<template>
	<h3>{{ name }}</h3>
	<p class="text-center">{{ description }}</p>
	<p>Category: {{ category }}</p>
	<p>Has Delivery: {{ hasDelivery }}</p>
	<p>Contact Email: {{ contactEmail }}</p>
	<p>Contact Number: {{ contactNumber }}</p>
	<div v-if="dishes">
		<h4>Dishes:</h4>
		<ul>
			<li v-for="dish in dishes" :key="dish.id">{{ dish.name }}</li>
		</ul>
	</div>
	<router-link :to="restaurantDetails">Go to Restaurant</router-link>
</template>

<script>
export default {
	props: [
		"id",
		"name",
		"description",
		"category",
		"hasDelivery",
		"contactEmail",
		"contactNumber",
		"dishes",
	],
	computed: {
		restaurantDetails() {
			return this.$route.path + "/" + this.id;
		},
	},
};
</script>
