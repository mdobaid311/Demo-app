import Footer from "@/components/common/Footer";
import Header from "@/components/common/Header";
import { AppSidebar } from "@/components/common/sidebar/app-sidebar";
import Topbar from "@/components/common/topbar";
import { SidebarProvider } from "@/components/ui/sidebar";
import { login } from "@/store/slices/userSlice";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { Outlet, useNavigate } from "react-router-dom";

const AppLayout = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const pathname = window.location.pathname;
  useEffect(() => {
    // get user data from local storage
    const userType = localStorage.getItem("userType");
    const userID = localStorage.getItem("userID");
    const email = localStorage.getItem("email");
    const accessToken = localStorage.getItem("token");
    const name = localStorage.getItem("name");

    if (!email || !userType || !userID || !accessToken) {
      navigate("/auth/login?redirect=" + pathname);
      return;
    }
    dispatch(
      login({
        name: name || "",
        email: email || "",
        userType: userType || "",
        userID: userID || "",
      })
    );
  }, []);

  return (
    <div className="flex flex-col" style={{ minHeight: "100vh" }}>
      <Header />
      <SidebarProvider defaultOpen={false} className="flex-1">
        <AppSidebar />
        <div className="flex-1 p-2 flex flex-col gap-2">
          <Topbar />
          <div className="flex-1">
            <Outlet />
          </div>
        </div>
      </SidebarProvider>
      <Footer />
    </div>
  );
};

export default AppLayout;
