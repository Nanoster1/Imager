import { createApp } from "vue";
import App from "./App.vue";
import gAuth from "vue3-google-oauth2";
import { VueSignalR } from "@quangdao/vue-signalr";

// Vuetify
import "vuetify/styles";
import { createVuetify } from "vuetify";
import * as components from "vuetify/components";
import * as directives from "vuetify/directives";

const vuetify = createVuetify({
  components,
  directives
});

const clientId = "560104569923-22dq24omph7h922ifhb79o13v8ahnj2p.apps.googleusercontent.com";
const app = createApp(App);

app.use(gAuth, {
  clientId,
  plugin_name: "resizer"
});
app.use(vuetify);
app.use(VueSignalR, {
  url: "http://localhost:5000/resize-hub",
  accessTokenFactory: () => clientId
});

app.mount("#app");
