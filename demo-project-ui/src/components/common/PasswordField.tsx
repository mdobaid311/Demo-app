import React, { useState } from "react";
import { Input } from "@/components/ui/input";
import { Eye, EyeOff } from "lucide-react";
import { cn } from "@/lib/utils";

interface PasswordFieldProps
  extends React.InputHTMLAttributes<HTMLInputElement> {
  className?: string;
}

const PasswordField = React.forwardRef<HTMLInputElement, PasswordFieldProps>(
  ({ className, ...props }, ref) => {
    const [isView, setIsView] = useState(false);

    return (
      <div
        className={cn(
          "flex items-center justify-between h-9 w-full rounded-md border border-input bg-transparent py-1 text-sm shadow-sm transition-colors file:border-0 file:bg-transparent file:text-sm file:font-medium file:text-foreground placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:cursor-not-allowed disabled:opacity-50",
          className
        )}
      >
        {isView ? (
          <Input
            type={"text"}
            className={cn(
              "border-none outline-none focus-visible:ring-0 flex-1s",
              className
            )}
            ref={ref}
            {...props}
          />
        ) : (
          <Input
            type={"password"}
            className={cn(
              "border-none outline-none focus-visible:ring-0",
              className
            )}
            ref={ref}
            {...props}
          />
        )}
        {isView ? (
          <Eye
            className="z-10 cursor-pointer text-gray-500 w-4 mx-3"
            onClick={() => setIsView((prev) => !prev)}
          />
        ) : (
          <EyeOff
            className="z-10 cursor-pointer text-gray-500 w-4 mx-3"
            onClick={() => setIsView((prev) => !prev)}
          />
        )}
      </div>
    );
  }
);

PasswordField.displayName = "PasswordField";

export default PasswordField;
