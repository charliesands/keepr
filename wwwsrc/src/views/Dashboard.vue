<template>
  <div class="dashboard">
    <h1>Hello?</h1>
    <form @submit.prevent>
      <select v-model="chosen">
        <option diabled value="">Choose Vault</option>
        <option v-for="vault in vaults" v-bind:value="vault.id">{{vault.name}}</option>
      </select>
      <input type="submit" @click="getVaultKeeps(chosen)">
    </form>

  </div>
</template>

<script>
  export default {
    name: "dashboard",
    data() {
      return {
        chosen: ""
      }
    },
    computed: {
      user() {
        return this.$store.state.user;
      },
      keeps() {
        return this.$store.state.keeps;
      },
      vaultKeeps() {
        return this.$store.state.vaultKeeps;
      },
      vaults() {
        return this.$store.state.vaults;
      }
    },
    methods: {
      getVaultKeeps(vault) {
        debugger
        this.$store.dispatch('getVaultKeeps', vault)
      }
    }
  }

</script>