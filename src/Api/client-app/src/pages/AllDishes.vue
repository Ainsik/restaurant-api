<template>
	<section>
		<div class="container">
			<div class="m-5 text-center">
				<h2>All Dishes</h2>
			</div>
			<div>
				<ul class="d-flex flex-column align-items-center">
					<li><router-link :to="dishDetails">Pizza</router-link></li>
					<li><router-link :to="dishDetails">Burger</router-link></li>
				</ul>
			</div>
		</div>
	</section>
</template>

<script>
export default {
	computed: {
		dishDetails() {
			return this.$route.path + "/" + "1";
		},
	},
};
</script>
