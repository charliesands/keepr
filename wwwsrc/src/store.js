import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let auth = Axios.create({
  baseURL: "//localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "//localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    vaults: [],
    keeps: [],
    vaultKeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    logout(state, user) {
      state.user = {}
      router.push({ name: 'login' })
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setVaultKeeps(state, vaultKeeps) {
      state.vaultKeeps = vaultKeeps
    }
  },
  actions: {
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('logout')
          router.push({ name: 'login' })
        })
    },

    //VAULTS
    getUserVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          commit('setVaults', res.data)
        })
    },
    addVault({ commit, dispatch }, vaultData) {
      api.post('vaults', vaultData)
        .then(res => {
          dispatch('getUserVaults')
        })
    },

    //KEEPS
    getKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setKeeps', res.data)
        })
    },
    addKeep({ commit, dispatch }, keepData) {
      api.post('keeps', keepData)
        .then(res => {
          dispatch('getKeeps')
        })
    },

    //VAULTKEEPS
    getVaultKeeps({ commit, dispatch }, data) {
      api.get('vaultkeeps/' + data)
        .then(res => {
          commit('setVaultKeeps', res.data)
        })
    },
    addVaultKeep({ commit, dispatch }, vaultKeepData) {
      api.post('vaultkeeps', vaultKeepData)
        .then(res => {
          commit('setVaultKeeps')
        })
    },
    updateKeep({ commit, dispatch }, keep) {
      api.put('keeps', keep)
        .then(res => {
          dispatch('getKeeps')
        })
    }
  }
})