<template>
	<header>
		<nav class="navbar navbar-expand-lg navbar-light">
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
						<router-link :to="{ name: 'restaurants' }">
							Restaurants
						</router-link>
						<router-link v-if="!token" to="/login">Login</router-link>
						<button class="logout" v-if="token" @click="logout">
							<i class="bi bi-box-arrow-right"></i> Logout
						</button>
					</div>
				</div>
			</div>
		</nav>
	</header>
</template>

<script>
export default {
	computed: {
		token() {
			return this.$store.getters.getToken;
		},
	},
	methods: {
		toggleNavbar() {
			const nav = document.querySelector(".navbar-collapse");
			document.addEventListener("click", () => {
				if (nav.classList.contains("show")) {
					nav.classList.remove("show");
				}
			});
		},
		logout() {
			this.$store.commit("clearToken");
			this.$router.push("/login");
		},
	},
};
</script>

<style scoped>
header {
	width: 100%;
	height: 5rem;
	background-color: #4e04b0;
	display: flex;
	justify-content: center;
	align-items: center;
}

header a {
	text-decoration: none;
	color: #f6ff00;
	display: inline-block;
	padding: 0.75rem 1.5rem;
	border: 1px solid transparent;
}

a:hover {
	border: 1px solid #f391e3;
}

header nav {
	width: 90%;
	margin: auto;
	display: flex;
	justify-content: space-between;
	align-items: center;
}

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

.logout {
	background-color: transparent;
	color: white;
	border: none;
}
</style>
