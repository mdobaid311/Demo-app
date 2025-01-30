
import React from "react";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import { IoMdClose } from "react-icons/io";
import { cn } from "@/lib/utils";
import { Button } from "../ui/button";
import { FaSpinner } from "react-icons/fa";

interface ISlideoutProps {
  title: string;
  description?: string;
  isOpen: boolean;
  children?: React.ReactNode;
  contentClassName?: string;
  titleClassName?: string;
  showFooter?: boolean;
  submitText?: string;
  isSubmitting?: boolean;
  onClose?: () => void;
  onSubmit?: () => void;
}

const Slideout = ({
  title,
  children,
  isOpen,
  description,
  contentClassName,
  titleClassName,
  submitText = "Submit",
  isSubmitting,
  onClose,
  onSubmit,
}: ISlideoutProps) => {
  return (
    <Sheet
      open={isOpen}
      onOpenChange={(open) => {
        if (!open && onClose) {
          onClose();
        }
      }}
    >
      <SheetContent
        className={`overflow-y-auto flex flex-col m-2 rounded-md ${contentClassName}`}
        showCloseIcon={false}
      >
        <SheetHeader
          className={cn(
            titleClassName,
            "text-accent-foreground flex justify-between rounded-md mb-6"
          )}
        >
          <div className="flex items-center justify-between">
            <SheetTitle
              className={cn(titleClassName, "text-accent-foreground")}
            >
              {title}
            </SheetTitle>
            <SheetTrigger>
              <IoMdClose />
            </SheetTrigger>
          </div>
          {description && <SheetDescription>{description}</SheetDescription>}
        </SheetHeader>
        <div className="flex-1">{children}</div>
        <SheetFooter>
          <div className="flex items-center justify-between gap-4">
            <Button
              variant="outline"
              type="button"
              onClick={() => {
                if (onClose) onClose();
              }}
              disabled={isSubmitting}
            >
              Cancel
            </Button>
            <Button
              onClick={() => {
                if (onSubmit) onSubmit();
              }}
              disabled={isSubmitting}
            >
              {isSubmitting ? (
                <FaSpinner className="animate-spin" />
              ) : (
                submitText
              )}
            </Button>
          </div>
        </SheetFooter>
      </SheetContent>
    </Sheet>
  );
};

export default Slideout;
