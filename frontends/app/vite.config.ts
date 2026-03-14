import { defineConfig, loadEnv } from "vite";

import react from "@vitejs/plugin-react";

const defaultPort = 5173;

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), "");
  const port = parseInt(env.VITE_PORT) || defaultPort;

  return {
    plugins: [react()],
    server: {
      port,
      open: true,
    },
  };
});
