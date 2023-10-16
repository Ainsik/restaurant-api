<template>
	<section>
		<div class="container">
			<div class="m-5 text-center">
				<h1>Restaurant Details</h1>
			</div>
			<router-link :to="restaurantDishes">Go to Dishes</router-link>
			<router-view />
		</div>
	</section>
</template>

<script>
export default {
	computed: {
		restaurantDishes() {
			return this.$route.path + "/" + "dishes";
		},
	},
};
</script>

<style></style>
