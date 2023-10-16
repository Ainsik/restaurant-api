<template>
	<section>
		<div class="container">
			<div class="m-5 text-center">
				<h2>Dish Details</h2>
				<p>Description</p>
			</div>
		</div>
		<div>
			<button @click="goBack" class="btn btn-primary">Previous page</button>
		</div>
	</section>
</template>

<script>
export default {
	methods: {
		goBack() {
			this.$router.back();
		},
	},
};
</script>
