import authAPI from "@/api/authAPI";

const authUtils = {
  isAuthenticated: async () => {
    const accessToken = localStorage.getItem("token");
    const refreshToken = localStorage.getItem("refresh_token");
    if (
      !accessToken ||
      accessToken == null ||
      accessToken === undefined ||
      !refreshToken ||
      refreshToken == null ||
      refreshToken === undefined
    )
      return { data: { status: 404 } };
    try {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      const res: any = await authAPI.refreshToken({
        AccessToken: accessToken,
        RefreshToken: refreshToken,
      });

      return res;
    } catch {
      return;
    }
  },
};

export const clearUserDetailsLocalStorage = () => {
  localStorage.setItem("userType", "");
  localStorage.setItem("userID", "");
  localStorage.setItem("token", "");
  localStorage.setItem("refresh_token", "");
};

export default authUtils;