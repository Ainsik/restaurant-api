import axios from "axios";

export default {
	async loadDishes(context, payload) {
		const URL = `https://localhost:7236/api${payload}`;

		await axios
			.get(URL)
			.then((result) => {
				context.commit("setDishes", result.data);
			})
			.catch((error) => {
				console.error("An error occurred:", error.response);
			});
	},
	async loadDish(context, payload) {
		const URL = `https://localhost:7236/api${payload}`;
		await axios
			.get(URL)
			.then((result) => {
				context.commit("setDish", result.data);
			})
			.catch((error) => {
				console.error("An error occurred:", error.response);
			});
	},
};
