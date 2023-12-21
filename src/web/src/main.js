import { createApp } from "vue";
import App from "./App.vue";
import gAuth from "vue3-google-oauth2";
import * as signalR from "@microsoft/signalr";

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

const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5000/resize-hub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
  })
  .build();

console.log(connection);

connection.on("send", (data) => {
  console.log(data);
});

const start = () => {
  connection.start();
};

start();

console.log(connection);

app.use(vuetify);

app.mount("#app");
