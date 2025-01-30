import usersAPI from "@/api/userAPI";
import { CustomAlertDialog } from "@/components/common/CustomAlertDialog";
import { DeleteIcon, EditIcon } from "@/components/common/icons";
import Loader from "@/components/common/Loader";
import { DataTable } from "@/components/generic-components/DataTable";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import UserAddEditForm from "@/components/users-components/UserAddEditForm";
import { userFormSchema } from "@/constants/schemas/formSchemas";
import { IUser } from "@/interfaces/IUser";
import { IRootState } from "@/store/store";
import { CaretSortIcon } from "@radix-ui/react-icons";
import { ColumnDef } from "@tanstack/react-table";
import { useEffect, useState } from "react";
import { FaPlus } from "react-icons/fa";
import { useSelector } from "react-redux";
import { z } from "zod";

const Users = () => {
  const [isUserAddSlideoutOpen, setIsUserAddSlideoutOpen] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [isFetching, setIsFetching] = useState(true);
  const [users, setUsers] = useState<IUser[]>([]);
  const [editingUserData, setEditingUserData] = useState<IUser>();

  const userData = useSelector((state: IRootState) => state.user);

  const getUsers = async () => {
    setIsFetching(true);
    try {
      usersAPI.getAllUsers().then((res) => {
        setUsers(res.data);
        setIsFetching(false);
      });
    } catch (error) {
      console.error(error);
      setIsFetching(false);
    }
  };

  useEffect(() => {
    getUsers();
  }, []);

  const userColumns: ColumnDef<IUser>[] = [
    {
      accessorKey: "firstName",
      header: ({ column }: any) => (
        <Button
          variant="ghost"
          className="font-bold text-primary"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          First Name
          <CaretSortIcon className="ml-2 h-4 w-4" />
        </Button>
      ),
      cell: ({ row }: any) => <div>{row.getValue("firstName")}</div>,
    },
    {
      accessorKey: "lastName",
      header: "Last Name",
      cell: ({ row }: any) => <div>{row.getValue("lastName")}</div>,
    },
    {
      accessorKey: "phoneNumber",
      header: "Phone Number",
      cell: ({ row }: any) => (
        <div className="">{row.getValue("phoneNumber")}</div>
      ),
    },
    {
      accessorKey: "email",
      header: "Email",
      cell: ({ row }: any) => <div>{row.getValue("email")}</div>,
    },
    ...(userData?.userType === "Admin"
      ? [
          {
            id: "userId",
            header: "Actions",
            cell: ({ row }: any) => {
              const handleDelete = () => {
                console.log(row);
                usersAPI.deleteUser(row.original.userId).then(() => {
                  getUsers();
                });
              };
              const handleEdit = () => {
                setIsUserAddSlideoutOpen(true);
                setIsEditing(true);
                setEditingUserData(row.original);
              };
              return (
                <div className="flex space-x-2">
                  <Button variant="outline" size="sm" onClick={handleEdit}>
                    <EditIcon />
                  </Button>
                  <CustomAlertDialog
                    actionText="Delete"
                    cancelText="Cancel"
                    description="Are you sure you want to delete this user?"
                    onAction={handleDelete}
                    title="Delete User"
                  >
                    <Button variant="destructive" size="sm">
                      <DeleteIcon />
                    </Button>
                  </CustomAlertDialog>
                </div>
              );
            },
          },
        ]
      : []),
  ];

  const handleUserAdd = (values: z.infer<typeof userFormSchema>) => {
    console.log("add", values);
    const body = {
      firstName: values.FirstName,
      lastName: values.LastName,
      email: values.Email,
      phoneNumber: values.PhoneNumber,
      password: values.Password,
      roleID: values.Role,
    };
    usersAPI.createUser(body).then(() => {
      getUsers();
      setIsUserAddSlideoutOpen(false);
    });
  };

  const handleUserEdit = (values: z.infer<typeof userFormSchema>) => {
    console.log("edit", values);
    const body = {
      userId: editingUserData?.userId,
      firstName: values.FirstName,
      lastName: values.LastName,
      email: values.Email,
      phoneNumber: values.PhoneNumber,
      roleID: values.Role,
    };
    usersAPI.updateUser(body).then(() => {
      getUsers();
      setIsUserAddSlideoutOpen(false);
    });
  };

  return (
    <Card className="h-full w-full max-w-full">
      <CardHeader>
        <CardTitle className="flex items-center justify-between">
          <h1 className="text-3xl text-primary">Users</h1>
          {userData?.userType === "Admin" && (
            <div className="flex items-center gap-2">
              <Button
                variant="default"
                onClick={() => setIsUserAddSlideoutOpen(true)}
              >
                Add User <FaPlus />
              </Button>
            </div>
          )}
        </CardTitle>
        <CardDescription>Manage your users</CardDescription>
      </CardHeader>
      <CardContent>
        <div className="max-w-full">
          {isFetching ? (
            <Loader />
          ) : (
            <DataTable
              data={users}
              columns={userColumns}
              filterPlaceholder="Search user..."
              showPagination={true}
            />
          )}
        </div>
      </CardContent>
      {isUserAddSlideoutOpen && (
        <UserAddEditForm
          onClose={() => setIsUserAddSlideoutOpen(false)}
          isUserAddSlideoutOpen={isUserAddSlideoutOpen}
          isSubmitting={false}
          onSuccess={(values: z.infer<typeof userFormSchema>) => {
            if (isEditing) {
              handleUserEdit(values);
            } else {
              handleUserAdd(values);
            }
          }}
          editingUserData={editingUserData}
          isEditing={isEditing}
        />
      )}
    </Card>
  );
};

export default Users;
