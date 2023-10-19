<template>
	<section>
		<base-card>
			<div class="container">
				<div class="previous-page">
					<i class="bi bi-arrow-return-left" @click="goBack"></i>
				</div>
				<div class="m-3 text-center">
					<div class="my-3">
						<h1>{{ restaurant.name }}</h1>
						<p class="fst-italic">{{ restaurant.category }}</p>
						<p>{{ restaurant.description }}</p>
						<div class="fst-italic">
							<span v-if="restaurant.hasDelivery" class="text-success"
								>{{ restaurant.name }} has delivery options.</span
							><span v-else class="text-danger"
								>{{ restaurant.name }} doesn't have a delivery
								option.</span
							>
						</div>
					</div>
					<div class="my-3">
						<h2>Address</h2>
						<p>City: {{ restaurant.city }}</p>
						<p>Street: {{ restaurant.street }}</p>
						<p>Postal Code: {{ restaurant.postalCode }}</p>
					</div>
				</div>
				<h2 v-if="hasDishes">
					<router-link :to="restaurantDishes">MENU</router-link>
				</h2>
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

<style>
.previous-page {
	font-size: 2.5rem;
	cursor: pointer;
}
</style>
