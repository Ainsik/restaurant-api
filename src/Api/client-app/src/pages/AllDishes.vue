<template>
	<div v-if="hasDishes">
		<h3>MENU</h3>
		<ul class="d-flex flex-column align-items-center">
			<li v-for="dish in dishes" :key="dish.id">
				<router-link :to="dishDetails(dish.id)">{{ dish.name }}</router-link>
			</li>
		</ul>
	</div>
	<div v-else class="text-center">
		<p class="text-danger fw-bold">No dishes.</p>
	</div>
</template>

<script>
export default {
	methods: {
		dishDetails(id) {
			return this.$route.path + "/" + id;
		},
	},
	computed: {
		dishes() {
			return this.$store.getters["dish/dishes"];
		},
		hasDishes() {
			return this.$store.getters["dish/hasDishes"];
		},
	},
	created() {
		const route = this.$route.path;
		this.$store.dispatch("dish/loadDishes", route);
	},
};
</script>
