import "bootstrap/dist/js/bootstrap.js";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import store from "./store/index.js";
import { createApp } from "vue";
import router from "./router.js";
import App from "./App.vue";
import BaseCard from "./components/UI/BaseCard.vue";

createApp(App)
	.use(router)
	.use(store)
	.component("base-card", BaseCard)
	.mount("#app");
