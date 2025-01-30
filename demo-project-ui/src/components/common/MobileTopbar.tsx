import { sidebarlinks } from "@/constants/sidebarLinks";
import { EllipsisVerticalIcon } from "lucide-react";
import { useState } from "react";
import { IoMdMenu } from "react-icons/io";
import { Link, useLocation } from "react-router-dom";
import Slideout from "../generic-components/Slideout.tsx";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar.tsx";
import { Button } from "../ui/button";
import { Card } from "../ui/card";

const MobileTopbar = () => {
  const [openSidebar, setOpenSidebar] = useState(false);
  const location = useLocation();

  return (
    <div className="lg:hidden">
      <Card className="lg:hidden p-4 flex items-center justify-between">
        <h2 className="text-lg font-semibold tracking-tight flex items-center gap-2">
          {/* <img src={logo} alt="" width={70} height={70} /> */}
        </h2>
        <IoMdMenu className="text-2xl" onClick={() => setOpenSidebar(true)} />
      </Card>
      <Slideout
        title="Menu"
        isOpen={openSidebar}
        onClose={() => setOpenSidebar(false)}
        contentClassName="h-full flex flex-col"
        titleClassName="text-primary text-2xl font-semibold"
      >
        <div className="flex flex-col justify-between flex-1">
          <div className="mt-4 flex flex-col gap-3">
            {sidebarlinks.map((link, index) => (
              <Link
                key={index}
                to={link.url}
                className="w-full justify-start"
                onClick={() => setOpenSidebar(false)}
              >
                <Button
                  key={index}
                  variant="ghost"
                  className={`w-full justify-start text-lg tracking-wider ${
                    link.url === location.pathname
                      ? "bg-[--f1-light-blue] text-primary-foreground"
                      : ""
                  }`}
                >
                  <link.icon className="mr-2 h-4 w-4 text-2xl" />
                  {link.title}
                </Button>
              </Link>
            ))}
          </div>
          <Card className="flex items-center justify-between m-2 p-2">
            <Avatar>
              <AvatarImage src="" />
              <AvatarFallback>MO</AvatarFallback>
            </Avatar>
            <div className="flex flex-col">
              <h3 className="text-sm font-semibold">Mohammed Obaid</h3>
              <p className="text-xs">Admin</p>
            </div>
            <div>
              <EllipsisVerticalIcon />
            </div>
          </Card>
        </div>
      </Slideout>
    </div>
  );
};

export default MobileTopbar;
