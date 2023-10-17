import { createStore } from "vuex";
import restaurantModule from "./restaurant/index.js";
import loginModule from "./login/auth.js";

const store = createStore({
	modules: {
		restaurant: restaurantModule,
		login: loginModule,
	},
});

export default store;
