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
};
