import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";

import vue from "@vitejs/plugin-vue";
import eslint from "vite-plugin-eslint";
import stylelint from "vite-plugin-stylelint";
import zipPack from "vite-plugin-zip-pack";

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  return {
    plugins: [
      vue(),
      eslint(),
      stylelint({ fix: true }),
      zipPack({ outDir: "dist", outFileName: `${mode}.zip` })
    ],
    resolve: {
      alias: {
        "@": fileURLToPath(new URL("./src", import.meta.url))
      }
    },
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: `@use "@/assets/scss/abstracts" as *;`
        }
      }
    }
    // если необходимо пошарить dev server по сети
    // server: {
    //   host: true
    // }
  };
});
