export default {
	dishes(state) {
		return state.dishes;
	},
	hasDishes(state) {
		return state.dishes && state.dishes.length > 0;
	},
	dish(state) {
		return state.dish;
	},
};
