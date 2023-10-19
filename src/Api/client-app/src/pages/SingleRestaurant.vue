<template>
	<section>
		<base-card>
			<div class="container">
				<div class="m-5 text-center">
					<div class="m-5">
						<h1>{{ restaurantDetails.name }}</h1>
						<p>{{ restaurantDetails.description }}</p>
						<p>{{ restaurantDetails.category }}</p>
						<p>Has Delivery: {{ restaurantDetails.hasDelivery }}</p>
					</div>
					<div class="m-5">
						<h2>Address</h2>
						<p>City: {{ restaurantDetails.city }}</p>
						<p>Street: {{ restaurantDetails.street }}</p>
						<p>Postal Code: {{ restaurantDetails.postalCode }}</p>
					</div>
				</div>
				<h2 v-if="hasDishes">
					<router-link :to="restaurantDishes">MENU</router-link>
				</h2>
				<div class="d-flex justify-content-center align-items-center m-3">
					<button @click="goBack" class="btn btn-primary">Previous page</button>
				</div>
				<router-view />
			</div>
		</base-card>
	</section>
</template>

<script>
export default {
	props: ["restaurantId"],
	data() {
		return {
			restaurant: null,
		};
	},
	methods: {
		goBack() {
			this.$router.back();
		},
	},
	computed: {
		restaurantDetails() {
			return this.restaurant;
		},
		hasDishes() {
			return this.restaurant.dishes.length > 0;
		},
		restaurantDishes() {
			return this.$route.path + "/" + "dishes";
		},
	},
	created() {
		this.restaurant = this.$store.getters["restaurant/restaurants"].find(
			(restaurant) => restaurant.id == this.restaurantId
		);
	},
};
</script>

<style></style>
