<template>
	<nav class="navbar navbar-expand-lg navbar-light fixed-top">
		<div class="container">
			<router-link :to="{ name: 'home' }"> Home </router-link>
			<button
				class="navbar-toggler"
				type="button"
				data-bs-toggle="collapse"
				data-bs-target="#navbarNavAltMarkup"
				aria-controls="navbarNavAltMarkup"
				aria-expanded="false"
				aria-label="Toggle navigation"
				@click="toggleNavbar"
			>
				<span class="navbar-toggler-icon"></span>
			</button>
			<div class="collapse navbar-collapse" id="navbarNavAltMarkup">
				<div class="navbar-nav ms-auto gap-3">
					<router-link :to="{ name: 'restaurants' }"> Restaurants </router-link>
					<router-link :to="{ name: 'dishes' }"> Dishes </router-link>
					<router-link :to="{ name: 'user' }"> Login </router-link>
				</div>
			</div>
		</div>
	</nav>
</template>

<script>
export default {
	methods: {
		toggleNavbar() {
			const nav = document.querySelector(".navbar-collapse");
			document.addEventListener("click", () => {
				if (nav.classList.contains("show")) {
					nav.classList.remove("show");
				}
			});
		},
	},
};
</script>

<style scoped>
.navbar-toggler-icon {
	font-size: 1.8rem;
	text-transform: uppercase;
	color: var(--color-heading-h1);
}

.navbar-toggler {
	padding: 1em 2em;
	color: var(--color-heading-h1);
	border-color: var(--color-heading-h1);
}
</style>
