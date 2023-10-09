<template>
	<section>
		<div class="container mt-5">
			<form @submit.prevent="login" class="w-25 mx-auto">
				<h2 class="m-5">Login</h2>
				<div class="my-4">
					<label for="email" class="form-label">E-mail</label>
					<input
						type="text"
						class="form-control"
						id="email"
						v-model="email"
						required
					/>
				</div>
				<div class="my-4">
					<label for="password" class="form-label">Password</label>
					<input
						type="password"
						class="form-control"
						id="password"
						v-model="password"
						required
					/>
				</div>
				<div class="d-flex justify-content-center align-items-center m-5">
					<button type="submit" class="btn btn-primary">Login</button>
				</div>
			</form>
			<div class="text-center m-5">
				<p>
					Not a member?
					<router-link to="/registration"> Register now!</router-link>
				</p>
			</div>
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
				})
				.catch((error) => {
					console.error("Login error", error);
				});
		},
	},
};
</script>
