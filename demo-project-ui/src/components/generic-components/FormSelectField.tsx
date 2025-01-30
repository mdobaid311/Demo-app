import { FieldValues, Path, UseFormReturn } from "react-hook-form";
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/select";
type OptionType = { label: string; value: string };

type FormSelectFieldProps<TSchema extends FieldValues> = {
  form: UseFormReturn<TSchema>;
  name: Path<TSchema>;
  label: string;
  placeholder: string;
  required?: boolean;
  options: OptionType[];
  onValueChange?: (value: string) => void;
};

const FormSelectField = <TSchema extends FieldValues>({
  form,
  name,
  label,
  placeholder,
  required = false,
  options,
  onValueChange,
}: FormSelectFieldProps<TSchema>) => (
  <FormField
    control={form.control}
    name={name}
    render={({ field }) => (
      <FormItem>
        <FormLabel required={required}>{label}</FormLabel>
        <FormControl>
          <Select
            onValueChange={(value) => {
              field.onChange(value);
              onValueChange?.(value);
            }}
            value={field.value}
          >
            <SelectTrigger>
              <SelectValue placeholder={placeholder} />
            </SelectTrigger>
            <SelectContent>
              <SelectGroup>
                {options.map((option) => (
                  <SelectItem key={option.value} value={option.value}>
                    {option.label}
                  </SelectItem>
                ))}
              </SelectGroup>
            </SelectContent>
          </Select>
        </FormControl>
        <FormMessage />
      </FormItem>
    )}
  />
);

export default FormSelectField;
