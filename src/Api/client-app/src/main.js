import "bootstrap/dist/js/bootstrap.js";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import store from "./store";
import { createApp } from "vue";
import router from "./router";
import App from "./App.vue";

const app = createApp(App);
app.use(router).use(store).mount("#app");
