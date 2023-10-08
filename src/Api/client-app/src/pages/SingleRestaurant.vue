<template>
	<section>
		<div>
			<h2>CURRENT ID: {{ restaurantId }}</h2>
			<input type="text" v-model="restaurantId" />
			<button @click="getRestaurant">getRestaurant</button>
			<p v-if="isValid">
				ID: {{ restaurant.id }} <br />
				Name: {{ restaurant.name }}
			</p>
			<p v-else>Invalid id!</p>
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
			this.isValid = true;
			try {
				await axios.get(`${API}/${this.restaurantId}`).then((result) => {
					const { data } = result;
					this.restaurant = data;
				});
			} catch (error) {
				this.isValid = false;
			}
			this.restaurantId = null;
		},
	},
};
</script>

<style></style>
