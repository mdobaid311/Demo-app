import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
  } from "@/components/ui/tooltip";
  import { IconType } from "react-icons";
  
  interface IconsProps {
    icon: IconType;
    label: string;
  }
  
  const CustomIcon: React.FC<IconsProps> = ({ icon: Icon, label }) => {
    return (
      <TooltipProvider>
        <Tooltip>
          <TooltipTrigger>
            <Icon />
          </TooltipTrigger>
          <TooltipContent>{label}</TooltipContent>
        </Tooltip>
      </TooltipProvider>
    );
  };
  
  export default CustomIcon;
  