<template>
	<router-view />
	<section>
		<div>
			<h2>CURRENT ID: {{ restaurantId }}</h2>
			<input type="text" v-model="restaurantId" />
			<button @click="getRestaurant">getRestaurant</button>
			<p>
				ID: {{ restaurant.id }} <br />
				Name: {{ restaurant.name }}
			</p>
		</div>
	</section>
</template>

<script>
import axios from "axios";
const API = "https://localhost:7236/api/restaurant";
export default {
	data() {
		return {
			restaurant: {},
			restaurantId: null,
		};
	},
	methods: {
		async getRestaurant() {
			const token = this.$store.getters.getToken;

			await axios
				.get(`${API}/${this.restaurantId}`, {
					headers: {
						Authorization: `Bearer ${token}`,
					},
				})
				.then((result) => {
					const { data } = result;
					this.restaurant = data;
				})
				.catch((error) => {
					console.error("Błąd żądania GET", error.response);
				});

			this.restaurantId = null;
		},
	},
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap");

* {
	box-sizing: border-box;
	margin: 0;
	padding: 0;
}

:root {
	--color-heading-h1: #ff0000;
	--color-heading-h3: rgb(255, 93, 18);
	--color-bg: rgb(44, 44, 44);
	--color-btn: rgba(254, 173, 0, 0.824);
	--text-color-primary: rgb(255, 255, 255);
	--text-color-secondary: rgb(255, 9, 9);
	--transition: all 500ms ease;
}

html {
	font-size: 62.5%;
	scroll-behavior: smooth;
}

body {
	font-family: "Roboto", sans-serif;
	font-size: 16px;
	position: relative;
	background-color: var(--color-bg);
	color: var(--text-color-primary);
}

section {
	margin-bottom: 5rem;
}

h1 {
	color: var(--color-heading-h1);
}

h2 {
	text-align: center;
	padding-bottom: 1em;
	color: var(--color-heading-h1);
	margin-bottom: 1rem;
	font-weight: 500;
	text-transform: capitalize;
}

h3 {
	font-weight: 500;
	text-align: center;
	color: var(--color-heading-h3);
}

.btn {
	font-size: 1.7rem;
	padding: 0.5em 1em;
	background-color: var(--color-btn);
	color: var(--text-color-primary);
	border: 2px solid var(--text-color-secondary);
}

.btn:hover {
	background-color: var(--text-color-secondary);
	color: var(--color-bg);
	transition: var(--transition);
	border-color: var(--color-btn);
}
</style>
