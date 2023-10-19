import axios from "axios";

export default {
	async loadRestaurants(context, payload) {
		await axios
			.get(`https://localhost:7236/api/restaurant`, { params: payload })
			.then((result) => {
				context.commit("setRestaurants", result.data.items);
			})
			.catch((error) => {
				console.error("An error occurred:", error.response);
			});
	},
	async loadRestaurant(context, payload) {
		const token = context.rootGetters.getToken;

		await axios
			.get(`https://localhost:7236/api/restaurant/${payload}`, {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			})
			.then((result) => {
				context.commit("setRestaurant", result.data);
			})
			.catch((error) => {
				console.error("An error occurred:", error.response);
			});
	},
};
