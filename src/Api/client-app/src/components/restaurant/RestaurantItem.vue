<template>
	<h3>{{ restaurantData.name }}</h3>
	<p class="text-center">{{ restaurantData.description }}</p>
	<p>Category: {{ restaurantData.category }}</p>
	<router-link :to="restaurantDetails">Go to Restaurant</router-link>
</template>

<script>
export default {
	props: ["restaurantData"],
	computed: {
		restaurantDetails() {
			return this.$route.path + "/" + this.restaurantData.id;
		},
	},
};
</script>
