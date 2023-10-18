<template>
	<section>
		<div class="container">
			<form @submit.prevent="loadRestaurants">
				<label for="searchPhrase">Search Phrase:</label>
				<input type="text" id="searchPhrase" v-model="query.searchPhrase" />

				<label for="pageSize">Page Size:</label>
				<input type="number" id="pageSize" v-model="query.pageSize" />

				<label for="pageNumber">Page Number:</label>
				<input type="number" id="pageNumber" v-model="query.pageNumber" />

				<label for="sortBy">Sort By:</label>
				<select id="sortBy" v-model="query.sortBy">
					<option value="Name">Name</option>
					<option value="Description">Description</option>
					<option value="Category">Category</option>
				</select>

				<label for="sortDirection">Sort Direction:</label>
				<select id="sortDirection" v-model="query.sortDirection">
					<option value="0">Ascending</option>
					<option value="1">Descending</option>
				</select>

				<button type="submit">Search</button>
			</form>

			<div v-if="hasRestaurants">
				<restaurant-item
					v-for="restaurant in restaurants"
					:key="restaurant.id"
					:id="restaurant.id"
					:name="restaurant.name"
					:description="restaurant.description"
					:category="restaurant.category"
					:dishes="restaurant.dishes"
					:has-delivery="restaurant.hasDelivery"
					:contact-email="restaurant.contactEmail"
					:contact-number="restaurant.contactNumber"
				/>
			</div>
			<h3 v-else>No restaurants found.</h3>

			<div>
				<h2>CURRENT ID: {{ restaurantId }}</h2>
				<input type="text" v-model="restaurantId" />
				<button @click="getRestaurant">getRestaurant</button>
				<p>
					ID: {{ restaurant.id }} <br />
					Name: {{ restaurant.name }}
				</p>
			</div>
		</div>
	</section>
</template>

<script>
import axios from "axios";
import RestaurantItem from "../components/restaurant/RestaurantItem.vue";

const API = "https://localhost:7236/api/restaurant";

export default {
	data() {
		return {
			restaurant: {},
			restaurantId: null,
			query: {
				searchPhrase: "",
				pageSize: 5,
				pageNumber: 1,
				sortBy: "",
				sortDirection: 0,
			},
		};
	},
	components: {
		RestaurantItem,
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
		async loadRestaurants() {
			this.$store.dispatch("restaurant/loadRestaurants", this.query);
		},
	},
	computed: {
		restaurants() {
			return this.$store.getters["restaurant/restaurants"];
		},
		hasRestaurants() {
			return this.$store.getters["restaurant/hasRestaurants"];
		},
	},
};
</script>
