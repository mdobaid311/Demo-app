import { axiosAuthClient } from "./APIClient";

const authAPI = {
  login(email: string, password: string) {
    return axiosAuthClient.post("/auth/login", {
      Email: email,
      Password: password,
    });
  },
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  refreshToken: (params: any) =>
    axiosAuthClient.post("account/refreshToken", params),
};

export default authAPI;