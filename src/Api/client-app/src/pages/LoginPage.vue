<template>
	<section>
		<div class="container mt-5">
			<form @submit.prevent="login" class="w-50 mx-auto">
				<h2 class="mb-4">Login</h2>
				<div class="mb-3">
					<label for="email" class="form-label">Email</label>
					<input
						type="text"
						class="form-control"
						id="email"
						v-model="email"
						required
					/>
				</div>
				<div class="mb-3">
					<label for="password" class="form-label">Password</label>
					<input
						type="password"
						class="form-control"
						id="password"
						v-model="password"
						required
					/>
				</div>
				<button type="submit" class="btn btn-primary">Zaloguj się</button>
			</form>
		</div>
	</section>
</template>

<script>
import axios from "axios";

export default {
	data() {
		return {
			email: "",
			password: "",
		};
	},
	methods: {
		async login() {
			await axios
				.post("https://localhost:7236/api/user/login", {
					email: this.email,
					password: this.password,
				})
				.then((response) => {
					const token = response.data;
					this.$store.dispatch("setToken", token);
					console.log(token);
				})
				.catch((error) => {
					console.error("Login error", error);
				});
		},
	},
};
</script>
