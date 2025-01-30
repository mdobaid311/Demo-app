import { type LucideIcon } from "lucide-react";

import {
  SidebarGroup,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  useSidebar,
} from "@/components/ui/sidebar";
import { Link, useLocation } from "react-router-dom";

export function NavMain({
  items,
}: {
  items: {
    title: string;
    url: string;
    icon?: LucideIcon | React.ElementType;
    isActive?: boolean;
    items?: {
      title: string;
      url: string;
    }[];
  }[];
}) {
  const { state } = useSidebar();

  const { pathname } = useLocation();

  const isActive = (url: string) => {
    if (url === "/") {
      return url === window.location.pathname;
    }
    return pathname.startsWith(url);
  };
  return (
    <SidebarGroup>
      <SidebarMenu>
        {items.map((item) => (
          <SidebarMenuItem
            key={item.title}
            className={
              isActive(item.url)
                ? "bg-sidebar-accent text-sidebar-accent-foreground rounded-md"
                : ""
            }
          >
            <Link to={item.url} className="w-full justify-start">
              <SidebarMenuButton tooltip={item.title}>
                {item.icon && state == "collapsed" && (
                  <span className="w-full justify-start">
                    <item.icon />
                  </span>
                )}
                {item.icon && state !== "collapsed" && <item.icon />}
                {state !== "collapsed" && (
                  <span className="w-full justify-start">
                    <span>{item.title}</span>
                  </span>
                )}
              </SidebarMenuButton>
            </Link>
          </SidebarMenuItem>
        ))}
      </SidebarMenu>
    </SidebarGroup>
  );
}
