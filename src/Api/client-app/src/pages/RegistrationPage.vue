<template>
	<section>
		<div class="container mt-5">
			<form @submit.prevent="register" class="w-50 mx-auto">
				<h2 class="mb-4">Registration</h2>
				<div class="mb-3">
					<label for="email" class="form-label">E-mail</label>
					<input
						type="email"
						class="form-control"
						id="email"
						v-model="email"
						required
					/>
				</div>
				<div class="mb-3">
					<label for="firstName" class="form-label">First Name:</label>
					<input
						type="text"
						class="form-control"
						id="firstName"
						v-model="firstName"
						required
					/>
				</div>
				<div class="mb-3">
					<label for="lastName" class="form-label">Last Name:</label>
					<input
						type="text"
						class="form-control"
						id="lastName"
						v-model="lastName"
						required
					/>
				</div>
				<div class="mb-3">
					<label for="dateOfBirth" class="form-label">Date of birth</label>
					<input
						type="date"
						class="form-control"
						id="dateOfBirth"
						v-model="dateOfBirth"
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
				<button type="submit" class="btn btn-primary">Sign up</button>
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
			firstName: "",
			lastName: "",
			dateOfBirth: "",
			password: "",
		};
	},
	methods: {
		async register() {
			const userData = {
				email: this.email,
				firstName: this.firstName,
				lastName: this.lastName,
				dateOfBirth: this.dateOfBirth,
				password: this.password,
			};

			await axios
				.post("https://localhost:7236/api/user/register", userData)
				.then((response) => {
					console.log(userData);
					console.log(response);
					this.$router.push("/login");
				})
				.catch((error) => {
					console.error(
						"Registration error",
						error.response.data.errors
					);
				});
		},
	},
};
</script>
