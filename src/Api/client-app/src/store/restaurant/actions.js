import axios from "axios";

export default {
	async loadRestaurants(context, payload) {
		const query = {
			searchPhrase: payload.searchPhrase,
			pageSize: payload.pageSize,
			pageNumber: payload.pageNumber,
		};
        
		try {
			const response = await axios.get(
				`https://localhost:7236/api/restaurant`,
				{ params: query }
			);
			context.commit("setRestaurants", response.data.items);
		} catch (error) {
			console.error("An error occurred:", error.response);
		}
	},
};
