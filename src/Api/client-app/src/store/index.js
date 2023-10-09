import { createStore } from "vuex";
import loginModule from "./login/auth.js";

const store = createStore({
	modules: {
		login: loginModule,
	},
});

export default store;
