const state = {
	token: null,
};

const mutations = {
	setToken(state, token) {
		state.token = token;
	},
	clearToken(state) {
		state.token = null;
	},
};

const actions = {
	setToken({ commit }, token) {
		commit("setToken", token);
	},
	clearToken({ commit }) {
		commit("clearToken");
	},
};

const getters = {
	isAuthenticated: (state) => !!state.token,
	getToken: (state) => state.token,
};

export default {
	state,
	mutations,
	actions,
	getters,
};
