<template>
	<section>
		<div class="container">
			<form @submit.prevent="loadRestaurants" class="container mt-4">
				<div class="row">
					<div class="col-sm-4 mb-3">
						<label for="searchPhrase" class="form-label">Search Phrase:</label>
						<input
							type="text"
							id="searchPhrase"
							class="form-control"
							v-model="query.searchPhrase"
						/>
					</div>
					<div class="col-sm-4 mb-3">
						<label for="pageSize" class="form-label">Page Size:</label>
						<select id="pageSize" class="form-select" v-model="query.pageSize">
							<option value="5">5</option>
							<option value="10">10</option>
							<option value="15">15</option>
						</select>
					</div>
					<div class="col-sm-4 mb-3">
						<label for="pageNumber" class="form-label">Page Number:</label>
						<input
							type="number"
							id="pageNumber"
							class="form-control"
							v-model="query.pageNumber"
						/>
					</div>
				</div>
				<div class="row">
					<div class="col-sm-4 mb-3">
						<label for="sortBy" class="form-label">Sort By:</label>
						<select id="sortBy" class="form-select" v-model="query.sortBy">
							<option value="Name">Name</option>
							<option value="Description">Description</option>
							<option value="Category">Category</option>
						</select>
					</div>
					<div class="col-sm-4 mb-3">
						<label for="sortDirection" class="form-label"
							>Sort Direction:</label
						>
						<select
							id="sortDirection"
							class="form-select"
							v-model="query.sortDirection"
						>
							<option value="0">Ascending</option>
							<option value="1">Descending</option>
						</select>
					</div>
				</div>
				<button type="submit" class="btn btn-primary">Search</button>
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
