<template>
	<section>
		<base-card v-if="restaurant">
			<div class="container">
				<div class="previous-page">
					<i class="bi bi-arrow-return-left" @click="goBack"></i>
				</div>
				<div class="m-3 text-center">
					<div class="my-3">
						<h1 class="fw-bold">{{ restaurant.name }}</h1>
						<p class="fst-italic">{{ restaurant.category }}</p>
						<p>{{ restaurant.description }}</p>
						<div class="fst-italic">
							<span v-if="restaurant.hasDelivery" class="text-success"
								>{{ restaurant.name }} has delivery options.</span
							><span v-else class="text-danger"
								>{{ restaurant.name }} doesn't have a delivery option.</span
							>
						</div>
					</div>
					<div class="">
						<h3>Address:</h3>
						<p>
							<span>{{ restaurant.street }}, </span>
							<span>{{ restaurant.city }}&nbsp;</span>
							<span>{{ restaurant.postalCode }}</span>
						</p>
					</div>
				</div>
				<div>
					<h3 v-if="isActive">
						<router-link :to="restaurantDishes" @click="toogleActive"
							>SHOW MENU</router-link
						>
					</h3>
					<router-view v-else />
				</div>
			</div>
		</base-card>
		<div v-else class="text-center text-danger">
			<h5 class="display-2">
				You must
				<span>
					<router-link to="/login">log in</router-link>
				</span>
				to see the restaurant details.
			</h5>
		</div>
	</section>
</template>

<script>
export default {
	data() {
		return {
			isActive: true,
		};
	},
	props: ["restaurantId"],
	methods: {
		goBack() {
			if (this.$route.path === `/restaurants/${this.restaurantId}`) {
				this.$router.push("/restaurants");
			} else {
				this.$router.push(`/restaurants/${this.restaurantId}`);
			}
			this.isActive = true;
		},
		toogleActive() {
			this.isActive = !this.isActive;
		},
	},
	computed: {
		restaurantDishes() {
			return this.$route.path + "/" + "dishes";
		},
		restaurant() {
			return this.$store.getters["restaurant/restaurant"];
		},
	},
	created() {
		this.$store.dispatch("restaurant/loadRestaurant", this.restaurantId);
	},
};
</script>

<style>
.previous-page {
	font-size: 2.5rem;
	cursor: pointer;
}
</style>
