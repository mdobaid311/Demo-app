
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { FieldValues, Path, UseFormReturn } from "react-hook-form";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";

type FormInputFieldProps<TSchema extends FieldValues> = {
  form: UseFormReturn<TSchema>;
  name: Path<TSchema>;
  label: string;
  placeholder: string;
  required?: boolean;
  type?: string;
};

const FormInputField = <TSchema extends FieldValues>({
  form,
  name,
  label,
  placeholder,
  required = false,
  type = "text",
}: FormInputFieldProps<TSchema>) => (
  <FormField
    control={form.control}
    name={name}
    render={({ field }) => (
      <FormItem>
        <FormLabel required={required}>{label}</FormLabel>
        <FormControl>
          {type !== "textarea" ? (
            <Input placeholder={placeholder} {...field} type={type} />
          ) : (
            <Textarea placeholder={placeholder} {...field} />
          )}
        </FormControl>
        <FormMessage />
      </FormItem>
    )}
  />
);

export default FormInputField;
