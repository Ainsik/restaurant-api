<template>
	<base-card>
		<div class="text-center">
			<h3>
				<router-link :to="restaurantDetails">{{
					restaurantData.name
				}}</router-link>
			</h3>
			<p>{{ restaurantData.description }}</p>
			<p>Category: {{ restaurantData.category }}</p>
		</div>
	</base-card>
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
