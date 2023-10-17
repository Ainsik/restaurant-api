export default {
	restaurants(state) {
		return state.restaurants;
	},
	hasRestaurants(state) {
		return state.restaurants && state.restaurants.length > 0;
	},
};
