import { useForm } from "react-hook-form";
import Slideout from "../generic-components/Slideout";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { userFormSchema } from "@/constants/schemas/formSchemas";
import FormInputField from "../generic-components/FormInputField";
import { Form } from "../ui/form";
import { useEffect } from "react";
import { IUser } from "@/interfaces/IUser";

interface IUserFormProps {
  onClose: () => void;
  onSuccess: (values: z.infer<typeof userFormSchema>) => void;
  isEditing?: boolean;
  isUserAddSlideoutOpen: boolean;
  isSubmitting: boolean;
  editingUserData?: IUser;
}

const UserAddEditForm = ({
  onClose,
  onSuccess,
  isEditing,
  isUserAddSlideoutOpen,
  isSubmitting,
  editingUserData,
}: IUserFormProps) => {
  const form = useForm<z.infer<typeof userFormSchema>>({
    resolver: zodResolver(userFormSchema),
  });

  const submitFormHandler = form.handleSubmit((values) => {
    onSuccess(values);
  });

  useEffect(() => {
    if (isEditing) {
      form.setValue("FirstName", editingUserData?.firstName || "");
      form.setValue("LastName", editingUserData?.lastName || "");
      form.setValue("Email", editingUserData?.email || "");
      form.setValue("PhoneNumber", editingUserData?.phoneNumber || "");
      form.setValue("Password", "Password");
    }
  }, [editingUserData, form, isEditing]);

  return (
    <Slideout
      isOpen={isUserAddSlideoutOpen}
      onClose={() => onClose()}
      title={isEditing ? "Edit User" : "Add User"}
      isSubmitting={isSubmitting}
      onSubmit={submitFormHandler}
    >
      <Form {...form}>
        <form onSubmit={submitFormHandler} className="space-y-4">
          <FormInputField
            form={form}
            name="FirstName"
            label="First Name"
            placeholder="Enter first name"
            required
          />
          <FormInputField
            form={form}
            name="LastName"
            label="Last Name"
            placeholder="Enter last name"
            required
          />
          <FormInputField
            form={form}
            name="Email"
            label="Email"
            placeholder="Enter email"
            required
          />
          <FormInputField
            form={form}
            name="PhoneNumber"
            label="Phone Number"
            placeholder="Enter phone number"
            required
          />
          <FormInputField
            form={form}
            name="Role"
            label="Role"
            placeholder="Enter role"
            required
          />
          {!isEditing && (
            <FormInputField
              form={form}
              name="Password"
              label="Password"
              placeholder="Enter password"
              required
            />
          )}
        </form>
      </Form>
    </Slideout>
  );
};

export default UserAddEditForm;
