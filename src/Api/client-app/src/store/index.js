import { createStore } from "vuex";
import restaurantModule from "./restaurant/index.js";
import dishModule from "./dish/index.js";
import loginModule from "./login/auth.js";

const store = createStore({
	modules: {
		restaurant: restaurantModule,
		dish: dishModule,
		login: loginModule,
	},
});

export default store;
