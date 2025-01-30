import employeesAPI from "@/api/employeeAPI";
import { CustomAlertDialog } from "@/components/common/CustomAlertDialog";
import { DeleteIcon, EditIcon, ExpandIcon } from "@/components/common/icons";
import Loader from "@/components/common/Loader";
import EmployeeAddEditForm from "@/components/employee-components/EmployeeAddEditForm";
import SalariesDialog from "@/components/employee-components/SalariesDialog";
import { DataTable } from "@/components/generic-components/DataTable";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { employeeFormSchema } from "@/constants/schemas/formSchemas";
import { IEmployee } from "@/interfaces/IEmployee";
import { IRootState } from "@/store/store";
import { CaretSortIcon } from "@radix-ui/react-icons";
import { ColumnDef } from "@tanstack/react-table";
import { useEffect, useState } from "react";
import { FaPlus } from "react-icons/fa";
import { useSelector } from "react-redux";
import { z } from "zod";
import { toast, Toaster } from "sonner";

const Employees = () => {
  const [isEmployeeAddSlideoutOpen, setIsEmployeeAddSlideoutOpen] =
    useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [isFetching, setIsFetching] = useState(true);
  const [employees, setEmployees] = useState<IEmployee[]>([]);
  const [editingEmployeeData, setEditingEmployeeData] = useState<IEmployee>();
  const userData = useSelector((state: IRootState) => state.user);
  const [showSalariesDialog, setShowSalariesDialog] = useState(false);
  const [selectedEmployeeID, setSelectedEmployeeID] = useState("");

  const getEmployees = async () => {
    setIsFetching(true);
    try {
      employeesAPI.getAllEmployees().then((res) => {
        setEmployees(res.data);
        setIsFetching(false);
      });
    } catch (error) {
      console.error(error);
      setIsFetching(false);
    }
  };

  useEffect(() => {
    getEmployees();
  }, []);

  const employeeColumns: ColumnDef<IEmployee>[] = [
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
            id: "employeeId",
            header: "Actions",
            cell: ({ row }: any) => {
              const handleDelete = () => {
                console.log(row);
                employeesAPI
                  .deleteEmployee(row.original.employeeId)
                  .then(() => {
                    getEmployees();
                  });
              };
              const handleEdit = () => {
                setIsEmployeeAddSlideoutOpen(true);
                setIsEditing(true);
                setEditingEmployeeData(row.original);
              };
              const handleView = () => {
                setSelectedEmployeeID(row.original.employeeId);
                setShowSalariesDialog(true);
              };
              return (
                <div className="flex space-x-2">
                  <Button variant="outline" size="sm" onClick={handleEdit}>
                    <EditIcon />
                  </Button>
                  <CustomAlertDialog
                    actionText="Delete"
                    cancelText="Cancel"
                    description="Are you sure you want to delete this employee?"
                    onAction={handleDelete}
                    title="Delete Employee"
                  >
                    <Button variant="destructive" size="sm">
                      <DeleteIcon />
                    </Button>
                  </CustomAlertDialog>
                  <Button variant="outline" size="sm" onClick={handleView}>
                    <ExpandIcon />
                  </Button>
                </div>
              );
            },
          },
        ]
      : []),
  ];

  const handleEmployeeAdd = (values: z.infer<typeof employeeFormSchema>) => {
    const body = {
      firstName: values.FirstName,
      lastName: values.LastName,
      email: values.Email,
      phoneNumber: values.PhoneNumber,
    };
    employeesAPI
      .createEmployee(body)
      .then(() => {
        getEmployees();
        setIsEmployeeAddSlideoutOpen(false);
      })
      .catch((error) => {
        if (error.status === 400) {
          toast.error(error.data);
        }
      });
  };

  const handleEmployeeEdit = (values: z.infer<typeof employeeFormSchema>) => {
    const body = {
      employeeId: editingEmployeeData?.employeeId,
      firstName: values.FirstName,
      lastName: values.LastName,
      email: values.Email,
      phoneNumber: values.PhoneNumber,
    };
    employeesAPI
      .updateEmployee(body)
      .then(() => {
        getEmployees();
        setIsEmployeeAddSlideoutOpen(false);
      })
      .catch((error) => {
        if (error.status === 400) {
          toast.error(error.data);
        }
      });
  };

  return (
    <Card className="h-full w-full max-w-full">
      <CardHeader>
        <CardTitle className="flex items-center justify-between">
          <h1 className="text-3xl text-primary">Employees</h1>
          {userData?.userType === "Admin" && (
            <div className="flex items-center gap-2">
              <Button
                variant="default"
                onClick={() => setIsEmployeeAddSlideoutOpen(true)}
              >
                Add Employee <FaPlus />
              </Button>
            </div>
          )}
        </CardTitle>
        <CardDescription>Manage your employees</CardDescription>
      </CardHeader>
      <CardContent>
        <div className="max-w-full">
          {isFetching ? (
            <Loader />
          ) : (
            <DataTable
              data={employees}
              columns={employeeColumns}
              filterPlaceholder="Search employee..."
              showPagination={true}
            />
          )}
        </div>
      </CardContent>
      {isEmployeeAddSlideoutOpen && (
        <EmployeeAddEditForm
          onClose={() => setIsEmployeeAddSlideoutOpen(false)}
          isEmployeeAddSlideoutOpen={isEmployeeAddSlideoutOpen}
          isSubmitting={false}
          onSuccess={(values: z.infer<typeof employeeFormSchema>) => {
            if (isEditing) {
              handleEmployeeEdit(values);
            } else {
              handleEmployeeAdd(values);
            }
          }}
          editingEmployeeData={editingEmployeeData}
          isEditing={isEditing}
        />
      )}

      {showSalariesDialog && (
        <SalariesDialog
          employeeID={selectedEmployeeID}
          showSalariesDialog={showSalariesDialog}
          onClose={() => {
            setShowSalariesDialog(false);
            setSelectedEmployeeID("");
          }}
        />
      )}
      <Toaster
        toastOptions={{
          className: "bg-primary text-primary-foreground",
        }}
      />
    </Card>
  );
};

export default Employees;
