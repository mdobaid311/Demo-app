import axiosClient from "./APIClient";

const usersAPI = {
  getAllUsers: async () => await axiosClient.get("/users"),
  getUserByID: async (id: string) => await axiosClient.get(`/users/${id}`),
  createUser: async (data: any) =>
    await axiosClient.post("/Auth/register", data),
  updateUser: async (data: any) => await axiosClient.put(`/users`, data),
  deleteUser: async (id: string) =>
    await axiosClient.delete(`/users/${id}`),
};

export default usersAPI;
