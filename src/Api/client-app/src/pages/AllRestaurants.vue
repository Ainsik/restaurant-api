<template>
	<section>
		<div class="container">
			<h2>CURRENT ID: {{ restaurantId }}</h2>
			<input type="text" v-model="restaurantId" />
			<button @click="getRestaurant">getRestaurant</button>
			<p>
				ID: {{ restaurant.id }} <br />
				Name: {{ restaurant.name }}
			</p>
			<router-link :to="restaurantDetails">Go to Restaurant</router-link>
		</div>
	</section>
</template>

<script>
import axios from "axios";
const API = "https://localhost:7236/api/restaurant";
export default {
	data() {
		return {
			restaurant: {},
			restaurantId: null,
		};
	},
	methods: {
		async getRestaurant() {
			const token = this.$store.getters.getToken;

			await axios
				.get(`${API}/${this.restaurantId}`, {
					headers: {
						Authorization: `Bearer ${token}`,
					},
				})
				.then((result) => {
					const { data } = result;
					this.restaurant = data;
				})
				.catch((error) => {
					console.error("Błąd żądania GET", error.response);
				});
		},
	},
	computed: {
		restaurantDetails() {
			return this.$route.path + "/" + this.restaurantId;
		},
	},
};
</script>
