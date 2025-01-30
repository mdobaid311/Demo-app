
import {
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { FieldValues, Path, UseFormReturn } from "react-hook-form";
import { CustomGroupMultiSelect } from "../common/CustomGroupMultiSelect";

type Option = Record<"value" | "label", string>;

interface OptionGroup {
  heading: string;
  options: Option[];
}
type FormMultiSelectFieldProps<TSchema extends FieldValues> = {
  form: UseFormReturn<TSchema>;
  name: Path<TSchema>;
  label: string;
  placeholder: string;
  required?: boolean;
  optionGroups: OptionGroup[];
  onSelect?: (selectedOptions: Option[]) => void;
  value?: Option[];
  CustomBadge?: React.ComponentType<{ option: Option }>;
};

const FormGroupMultiSelectField = <TSchema extends FieldValues>({
  form,
  name,
  label,
  placeholder,
  required = false,
  optionGroups,
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
            <CustomGroupMultiSelect
              {...field}
              optionGroups={optionGroups}
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

export default FormGroupMultiSelectField;
