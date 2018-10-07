<template>
  <div class="home">
    <h1>Welcome Home</h1>
    <form @submit.prevent="addVault">
      <input type="text" placeholder="Name" v-model="newVault.name" required>
      <input type="text" placeholder="Description" v-model="newVault.description">
      <button class="btn-success" type="submit">Create Vault</button>
    </form>
  </div>
</template>

<script>
  export default {
    name: "home",
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    data() {
      return {
        newVault: {
          name: "",
          description: "",
          userId: ""
        }
      };
    },
    computed: {
      user() {
        return this.$store.state.user;
      }
    },
    methods: {
      addVault() {
        debugger
        let userId = this.user.id
        this.$store.dispatch("addVault", this.newVault);
        this.newVault = { name: "", description: "", userId: "" };
        debugger
      }
    }
  };
</script>