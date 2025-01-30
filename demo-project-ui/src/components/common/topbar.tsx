import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { sidebarlinks } from "@/constants/sidebarLinks";
import { useLocation } from "react-router-dom";
import { Card, CardContent } from "../ui/card";
import { SidebarTrigger, useSidebar } from "../ui/sidebar";
import MobileTopbar from "./MobileTopbar";

const Topbar = () => {
  const { pathname } = useLocation();
  const currentTabName = sidebarlinks.find(
    (link) => link.url === pathname
  )?.title;

  const { isMobile } = useSidebar();

  return (
    <div className="flex flex-col gap-4">
      <MobileTopbar />
      <Card className="p-4 flex items-center">
        <CardContent className="p-0 flex items-center gap-8">
          {!isMobile && <SidebarTrigger />}
          <Breadcrumb>
            <BreadcrumbList>
              <BreadcrumbItem className="bg-[--fq-green-accent] text-[--fq-green] px-2 py-1 rounded-sm font-medium ">
                <BreadcrumbLink href="/">Home</BreadcrumbLink>
              </BreadcrumbItem>
              {currentTabName && (
                <BreadcrumbSeparator className="text-[--fq-dark-blue]" />
              )}
              {currentTabName && (
                <BreadcrumbItem className="bg-[--fq-light-blue-accent] text-[--f1-light-blue]  px-2 py-1 rounded-sm font-medium">
                  <BreadcrumbLink href={pathname}>
                    {currentTabName}
                  </BreadcrumbLink>
                </BreadcrumbItem>
              )}
            </BreadcrumbList>
          </Breadcrumb>
        </CardContent>
      </Card>
    </div>
  );
};

export default Topbar;
