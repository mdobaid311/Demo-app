import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { FaSpinner } from "react-icons/fa";
import { useDispatch } from "react-redux";
import { z } from "zod";

import authAPI from "@/api/authAPI";
import logo from "@/assets/logo.png";
import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { loginFormSchema } from "@/constants/schemas/formSchemas";
import { login } from "@/store/slices/userSlice";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [invalidCredentials, setInvalidCredentials] = useState(false);
  const [rememberMe, setRememberMe] = useState(false);
  const [loading, setLoading] = useState(false);
  const [activeBackgroundIndex, setActiveBackgroundIndex] = useState(1);

  useEffect(() => {
    const interval = setInterval(() => {
      setActiveBackgroundIndex((prev) => (prev === 3 ? 1 : prev + 1));
    }, 10000);
    return () => {
      clearInterval(interval);
    };
  }, []);

  const form = useForm<z.infer<typeof loginFormSchema>>({
    resolver: zodResolver(loginFormSchema),
  });

  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    const savedEmail = localStorage.getItem("email");
    const savedPassword = localStorage.getItem("password");
    const savedRememberMe = localStorage.getItem("rememberMe") === "true";

    if (savedRememberMe && savedEmail && savedPassword) {
      form.setValue("Email", savedEmail);
      form.setValue("Password", savedPassword);
      setRememberMe(true);
    } else {
      setRememberMe(false);
    }
  }, [form]);

  const submitHandler = form.handleSubmit(async (values) => {
    setLoading(true);
    try {
      const response = await authAPI.login(values.Email, values.Password);
      if (response?.status === 200) {
        localStorage.setItem("token", response?.data?.token);

        if (rememberMe) {
          localStorage.setItem("email", values.Email);
          localStorage.setItem("password", values.Password);
          localStorage.setItem("rememberMe", "true");
        } else {
          localStorage.removeItem("email");
          localStorage.removeItem("password");
          localStorage.removeItem("rememberMe");
        }
        dispatch(
          login({
            name: response?.data?.name,
            email: response?.data?.email,
            userType: response?.data?.roleName,
            userID: response?.data?.userId,
          })
        );

        setInvalidCredentials(false);

        const urlParams = new URLSearchParams(window.location.search);
        const redirect = urlParams.get("redirect");
        if (redirect) {
          navigate(redirect);
        } else {
          navigate("/");
        }
      } else {
        setInvalidCredentials(true);
      }
    } catch (error) {
      setInvalidCredentials(true);
      console.error("Login error:", error);
    } finally {
      setLoading(false);
    }
  });

  return (
    <>
      <div className="container relative flex h-screen flex-col items-center justify-center md:grid md:grid-cols-2 lg:max-w-none lg:px-0">
        <div className="relative hidden h-full flex-col bg-muted p-10 text-white dark:border-r md:flex">
          <div
            className={`absolute inset-0 bg-zinc-900 bg-cover bg-center bg-no-repeat login-bg-image-${activeBackgroundIndex}`}
          />
          <div className="relative z-20 flex items-center text-lg font-medium">
            <img src={logo} alt="Uptime Logo" width={120} height={120} />
          </div>
        </div>
        <div className="p-8 md:p-8">
          <div className="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]">
            <div className="flex flex-col space-y-2 text-center">
              <h1 className="text-2xl font-semibold tracking-tight">LOGIN</h1>
              <p className="text-sm text-muted-foreground">
                Enter your email and password to login
              </p>
            </div>
            <Form {...form}>
              <form onSubmit={submitHandler} className="flex flex-col gap-4">
                <FormField
                  control={form.control}
                  name="Email"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Email</FormLabel>
                      <FormControl>
                        <Input placeholder="Email" {...field} />
                      </FormControl>
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="Password"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Password</FormLabel>
                      <FormControl>
                        <Input
                          type="password"
                          placeholder="Password"
                          {...field}
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
                {invalidCredentials && (
                  <p className="text-red-500 text-sm">
                    Invalid email or password. Please try again.
                  </p>
                )}
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-2">
                    <input
                      type="checkbox"
                      checked={rememberMe}
                      onChange={(e) => setRememberMe(e.target.checked)}
                    />
                    <label>Remember Me</label>
                  </div>
                  <a href="#" className="text-sm text-primary font-semibold">
                    Forgot Password?
                  </a>
                </div>
                <Button type="submit">
                  {loading ? <FaSpinner className="animate-spin" /> : "Login"}
                </Button>
              </form>
            </Form>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;
