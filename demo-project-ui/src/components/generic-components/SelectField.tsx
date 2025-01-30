import { useEffect, useState } from "react";
import { Label } from "../ui/label";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/select";

type Option = {
  value: string;
  label: string;
};

type Props = {
  label: string;
  options: Option[];
  selectedValue: string;
  onValueChange: (value: string) => void;
};

const SelectField = ({
  label,
  options,
  selectedValue,
  onValueChange,
}: Props) => {
  const [internalValue, setInternalValue] = useState(selectedValue);

  useEffect(() => {
    setInternalValue(selectedValue);
  }, [selectedValue]);

  const handleValueChange = (value: string) => {
    setInternalValue(value);
    onValueChange(value);
  };

  return (
    <div className="flex items-center gap-2">
      <Label className="font-normal text-sm text-muted-foreground min-w-[100px]">
        {label}
      </Label>
      <Select onValueChange={handleValueChange} value={internalValue}>
        <SelectTrigger>
          <SelectValue placeholder="Choose an option" />
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
    </div>
  );
};

export default SelectField;
