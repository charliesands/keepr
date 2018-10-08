<template>
  <div class="home">
    <h1>Welcome Home</h1>
    <button class="btn-danger" @click="logout">Logout</button>

    <form @submit.prevent="addVault">
      <input type="text" placeholder="Name" v-model="newVault.name" required>
      <input type="text" placeholder="Description" v-model="newVault.description">
      <button class="btn-success" type="submit">Create Vault</button>
    </form>
    <form @submit.prevent="addKeep">
      <input type="text" placeholder="Name" v-model="newKeep.name" required>
      <input type="text" placeholder="Description" v-model="newKeep.description">
      <input type="text" placeholder="Image URL" v-model="newKeep.img">

      <button class="btn-success" type="submit">Create Keep</button>
    </form>
    <div v-for="keep in keeps" :key="keep._id">
      <!-- <router-link :to="{name: 'board', params: {boardId: board._id}}">{{board.title}}</router-link> -->
      <img :src="keep.img">
      {{keep.name}}- {{keep.description}}
      <!-- <button class="btn-danger" @click="deleteBoard(board._id)">Delete Board</button> -->
    </div>
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
    mounted() {
      this.$store.dispatch("getKeeps");
    },
    data() {
      return {
        newVault: {
          name: "",
          description: "",
          userId: ""
        },
        newKeep: {
          name: "",
          description: "",
          img: "",
          userId: ""
        }
      };
    },
    computed: {
      user() {
        return this.$store.state.user;
      },
      keeps() {
        return this.$store.state.keeps;
      },
    },
    methods: {
      logout() {
        this.$store.dispatch("logout")
      },
      addVault() {
        this.newVault.userId = this.user.id
        this.$store.dispatch("addVault", this.newVault);
        this.newVault = { name: "", description: "", userId: "" };
      },
      addKeep() {
        this.newKeep.userId = this.user.id
        this.$store.dispatch("addKeep", this.newKeep);
        this.newKeep = { name: "", description: "", img: "", userId: "" };
      }
    }
  };
</script>