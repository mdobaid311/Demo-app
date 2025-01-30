import {
  FormControl,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { useForm } from "react-hook-form";
import PasswordField from "./PasswordField";

interface IPasswordInputField {
  label: string;
  id: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  form: ReturnType<typeof useForm> | any;
  className?: string;
  inputFieldClassName?: string;
  labelClassName?: string;
  required?: boolean;
  disabled?: boolean;
}

const PasswordInputField = ({
  label,
  id,
  form,
  className,
  required,
  disabled,
  inputFieldClassName,
  labelClassName,
}: IPasswordInputField) => (
  <FormItem className={`${className}`}>
    <FormLabel className={labelClassName} required={required}>
      {label}
    </FormLabel>
    <div>
      <FormControl>
        <PasswordField
          id={id}
          {...form.register(id)}
          className={`${inputFieldClassName}`}
          onChange={(e) => {
            e.preventDefault();
            form.setValue(id, e.target.value);
          }}
          disabled={disabled}
        />
      </FormControl>
      <FormMessage>
        {form.formState.errors[id]?.message?.toString()}
      </FormMessage>
    </div>
  </FormItem>
);

export default PasswordInputField;
