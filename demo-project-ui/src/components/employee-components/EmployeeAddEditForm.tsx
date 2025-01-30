import { useForm } from "react-hook-form";
import Slideout from "../generic-components/Slideout";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { employeeFormSchema } from "@/constants/schemas/formSchemas";
import FormInputField from "../generic-components/FormInputField";
import { Form } from "../ui/form";
import { useEffect } from "react";
import { IEmployee } from "@/interfaces/IEmployee";

interface IEmployeeFormProps {
  onClose: () => void;
  onSuccess: (values: z.infer<typeof employeeFormSchema>) => void;
  isEditing?: boolean;
  isEmployeeAddSlideoutOpen: boolean;
  isSubmitting: boolean;
  editingEmployeeData?: IEmployee;
}

const EmployeeAddEditForm = ({
  onClose,
  onSuccess,
  isEditing,
  isEmployeeAddSlideoutOpen,
  isSubmitting,
  editingEmployeeData,
}: IEmployeeFormProps) => {
  const form = useForm<z.infer<typeof employeeFormSchema>>({
    resolver: zodResolver(employeeFormSchema),
  });

  const submitFormHandler = form.handleSubmit((values) => {
    onSuccess(values);
  });

  useEffect(() => {
    if (isEditing) {
      form.setValue("FirstName", editingEmployeeData?.firstName || "");
      form.setValue("LastName", editingEmployeeData?.lastName || "");
      form.setValue("Email", editingEmployeeData?.email || "");
      form.setValue("PhoneNumber", editingEmployeeData?.phoneNumber || "");
    }
  }, [editingEmployeeData, form, isEditing]);

  return (
    <Slideout
      isOpen={isEmployeeAddSlideoutOpen}
      onClose={() => onClose()}
      title={isEditing ? "Edit Employee" : "Add Employee"}
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
            placeholder="Enter Phone Number"
            required
          />
        </form>
      </Form>
    </Slideout>
  );
};

export default EmployeeAddEditForm;
