import path from "path";
import react from "@vitejs/plugin-react-swc";
import { defineConfig } from "vite";

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      "components": path.resolve(__dirname, "./src/components"),
      "@": path.resolve(__dirname, "./src"),
      "@/assets": path.resolve(__dirname, "./src/assets/"),
    },
  },
  server: {
    host: "localhost",
    port: 3000,
  },
});
