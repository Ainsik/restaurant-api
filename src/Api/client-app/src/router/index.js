import { createRouter, createWebHistory } from "vue-router";
import DefaultLayout from "../components/DefaultLayout.vue";
import MainPage from "../pages/MainPage.vue";
import AllRestaurants from "../pages/AllRestaurants.vue";
import SingleRestaurant from "../pages/SingleRestaurant.vue";
import AllDishes from "../pages/AllDishes.vue";
import SingleDish from "../pages/SingleDish.vue";
import LoginPage from "../pages/LoginPage.vue";
import RegistrationPage from "../pages/RegistrationPage.vue";

const routes = [
	{
		path: "/",
		component: DefaultLayout,
		children: [
			{
				path: "/",
				name: "home",
				component: MainPage,
			},
			{
				path: "/restaurants",
				name: "restaurants",
				component: AllRestaurants,
			},
			{
				path: "/restaurant/:restaurantId?",
				name: "restaurant",
				component: SingleRestaurant,
			},
			{
				path: "/restaurant/:restaurantId?/dish",
				name: "dishes",
				component: AllDishes,
			},
			{
				path: "/restaurant/:restaurantId?/dish/:dishId?",
				name: "dish",
				component: SingleDish,
			},
			{
				path: "/login",
				name: "login",
				component: LoginPage,
			},
			{
				path: "/registration",
				name: "registration",
				component: RegistrationPage,
			},
		],
	},
];

const router = createRouter({
	history: createWebHistory(),
	routes,
});

export default router;
