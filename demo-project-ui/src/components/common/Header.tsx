import { IoMdLogOut } from "react-icons/io";
import logo from "../../assets/logo.png";
import user from "../../assets/user.png";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { Button } from "../ui/button";
import { Card, CardContent } from "../ui/card";
import { clearUserDetailsLocalStorage } from "@/utils/authUtils";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { IRootState } from "@/store/store";

const Header = () => {
  const navigate = useNavigate();
  const userData = useSelector((state: IRootState) => state.user);

  const logoutHandler = () => {
    clearUserDetailsLocalStorage();
    navigate("/auth/login");
  };

  return (
    <Card className="m-2 mb-0">
      <CardContent className="p-2">
        <div className="flex items-center justify-between">
          <div className="h-10 flex items-center gap-4">
            <img
              src={logo}
              alt="demo app logo"
              className="object-cover h-full"
            />
            <div className="uppercase font-semibold text-xl">Demo App </div>
          </div>
          <div className="flex items-center gap-8">
            <div className="flex items-center gap-2">
              <Avatar>
                <AvatarImage src={user} className="object-cover" />
                <AvatarFallback>MO</AvatarFallback>
              </Avatar>
              <div className="flex flex-col">
                <h3 className="font-semibold">
                  {userData?.name || "Demo User"}
                </h3>
                <p className="text-xs">{userData?.userType}</p>
              </div>
            </div>
            <div className="flex items-center gap-8">
              <Button variant="destructive" onClick={logoutHandler}>
                <IoMdLogOut />
              </Button>
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
};

export default Header;
