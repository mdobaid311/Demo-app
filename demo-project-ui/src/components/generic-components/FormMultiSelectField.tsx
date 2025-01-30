
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { FieldValues, Path, UseFormReturn } from "react-hook-form";
import { CustomMultiSelect } from "../common/CustomMultiSelect";

type OptionType = { label: string; value: string };

type FormMultiSelectFieldProps<TSchema extends FieldValues> = {
  form: UseFormReturn<TSchema>;
  name: Path<TSchema>;
  label: string;
  placeholder: string;
  required?: boolean;
  options: OptionType[];
  onSelect: (selectedOptions: OptionType[]) => void;
  value?: OptionType[];
  CustomBadge?: React.ComponentType<{ option: OptionType }>;
};

const FormMultiSelectField = <TSchema extends FieldValues>({
  form,
  name,
  label,
  placeholder,
  required = false,
  options,
  onSelect,
  value,
  CustomBadge,
}: FormMultiSelectFieldProps<TSchema>) => (
  <FormField
    control={form.control}
    name={name}
    render={({ field }) => (
      <FormItem>
        <FormLabel required={required}>{label}</FormLabel>
        <FormControl>
          <div>
            <CustomMultiSelect
              {...field}
              options={options}
              placeholder={placeholder}
              onSelect={onSelect}
              value={value}
              CustomBadge={CustomBadge}
            />
          </div>
        </FormControl>
        <FormMessage />
      </FormItem>
    )}
  />
);

export default FormMultiSelectField;
