import { BrowserRouter, Route, Routes } from "react-router-dom";
import AppLayout from "./layouts/AppLayout";
import AppRoutes from "./routes/AppRoutes";
import { Provider } from "react-redux";
import { store } from "./store/store";
import AuthLayout from "./layouts/AuthLayout";
import AuthRoutes from "./routes/AuthRoutes";

const App = () => {
  return (
    <BrowserRouter>
      <Provider store={store}>
        <Routes>
          <Route path="/" element={<AppLayout />}>
            <Route path="*" element={<AppRoutes />} />
          </Route>
          <Route path="/auth/*" element={<AuthLayout />}>
            <Route path="*" element={<AuthRoutes />} />
          </Route>
        </Routes>
      </Provider>
    </BrowserRouter>
  );
};

export default App;
