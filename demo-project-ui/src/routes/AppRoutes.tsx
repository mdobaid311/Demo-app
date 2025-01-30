import Employees from "@/containers/Employees";
import Users from "@/containers/Users";
import { Route, Routes } from "react-router-dom";

const AppRoutes = () => {
  const routes = [
    {
      label: "Users",
      path: "/users",
      Component: Users,
    },
    {
      label: "Employees",
      path: "/employees",
      Component: Employees,
    },
  ];
  return (
    <Routes>
      {routes?.map((route) => (
        <Route
          key={route.path}
          path={route.path}
          element={<route.Component />}
        />
      ))}
    </Routes>
  );
};

export default AppRoutes;
