import axiosClient from "./APIClient";

const employeesAPI = {
  getAllEmployees: async () => {
    return await axiosClient.get("/employee");
  },
  getEmployeeByID: async (id: string) =>
    await axiosClient.get(`/employee/${id}`),
  createEmployee: async (data: any) =>
    await axiosClient.post("/employee", data),
  updateEmployee: async (data: any) => await axiosClient.put(`/employee`, data),
  deleteEmployee: async (id: string) =>
    await axiosClient.delete(`/employee/${id}`),
  getSalariesbyEmployeeID: async (id: string) =>
    await axiosClient.get(`/salary/${id}`),
  createSalary: async (data: any) => await axiosClient.post("/salary", data),
  deleteSalary: async (id: string) => await axiosClient.delete(`/salary/${id}`),
};

export default employeesAPI;
