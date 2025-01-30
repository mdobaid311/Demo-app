import Login from "@/containers/Login";
import { Route, Routes } from "react-router-dom";

const AuthRoutes = () => {
  const routes = [
    {
      label: "Login",
      path: "/login",
      Component: Login,
    },
  ];
  return (
    <Routes>
      {JSON.stringify(routes)}
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

export default AuthRoutes;
