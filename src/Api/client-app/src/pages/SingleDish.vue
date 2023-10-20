<template>
	<div v-if="dish" class="text-center">
		<h3>{{ dish.name }}</h3>
		<p class="">{{ dish.description }}</p>
		<p>
			Price: <span class="text-success fw-bold">{{ dish.price }}$</span>
		</p>
	</div>
	<div v-else>
		<p>Something went wrong</p>
	</div>
</template>

<script>
export default {
	props: ["dishId"],
	computed: {
		dish() {
			return this.$store.getters["dish/dish"];
		},
	},
	created() {
		const route = this.$route.path;
		this.$store.dispatch("dish/loadDish", route);
	},
};
</script>
