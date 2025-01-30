import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface userState {
  email: string;
  userType: string;
  userID: string;
  name: string;
}

const initialState: userState = {
  email: "",
  userType: "",
  userID: "",
  name: "",
};

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    login: (
      state,
      action: PayloadAction<{
        email: string;
        userType: string;
        userID: string;
        name: string;
      }>
    ) => {
      const { email, userType, userID, name } = action.payload;
      state.email = email;
      state.userType = userType;
      state.userID = userID;
      state.name = name;
      localStorage.setItem("email", email);
      localStorage.setItem("userType", userType);
      localStorage.setItem("userID", userID);
      localStorage.setItem("name", name);
    },
    logout: () => {
      localStorage.setItem("userType", "");
      localStorage.setItem("email", "");
      localStorage.setItem("userID", "");
      localStorage.setItem("name", "");
      localStorage.setItem("token", "");
      localStorage.setItem("refresh_token", "");
    },
  },
});

export const { login, logout } = userSlice.actions;
export default userSlice.reducer;
