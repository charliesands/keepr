<template>
  <div class="home">
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <a class="navbar-brand" href="#">myKeepr</a>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown"
        aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNavDropdown">
        <ul class="navbar-nav">
          <li class="nav-item">
            <button class="btn-danger" @click="logout">Logout</button>
          </li>
          <!-- <li class="nav-item">
                      <a class="nav-link" href="#">Register</a>
                  </li> -->
        </ul>
      </div>
    </nav>
    <h1>Welcome Home</h1>
    <button class="btn-danger" @click="logout">Logout</button>
    <router-link :to="{name: 'dashboard'}"><button class="btn btn-info">Dashboard</button></router-link>

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
    <div class="container-fluid">
      <div class="row">
        <div v-for="keep in keeps" :key="keep._id" class="col-3">
          <!-- <router-link :to="{name: 'board', params: {boardId: board._id}}">{{board.title}}</router-link> -->
          <div @click="viewKeep(keep)" data-toggle="modal" :data-target="'#keep'+keep.id">
            <img :src="keep.img">
          </div>
          <div>
            <h3> {{keep.name}}</h3>
          </div>
          <div>
            shares:{{keep.shares}}views:{{keep.views}}keeps:{{keep.keeps}}
          </div>
          <button type="button" class="btn btn-primary" @click="addShare(keep)">Share</button>
          <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#keep'+keep.id" @click="viewKeep(keep)">View</button>
          <!-- <button type="button" class="btn btn-primary">Keep</button> -->
          <button v-if="user.id == keep.userId" class="btn btn-danger" @click="deleteKeep(keep)">Delete</button>

          <div class="modal fade" :id="'keep'+keep.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">{{keep.name}}</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div>
                    <img :src="keep.img">
                  </div>
                  <div>
                    {{keep.description}}
                  </div>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
              </div>
            </div>
          </div>



          <form @submit.prevent>
            <select v-model="chosen">
              <option diabled value="">Choose Vault</option>
              <option v-for="vault in vaults" v-bind:value="vault.id">{{vault.name}}</option>
            </select>
            <button class="btn btn-primary m-1" type="submit" @click="addVaultKeep(keep)">Add to Vault</button>
          </form>
          <!-- <button class="btn-danger" @click="deleteBoard(board._id)">Delete Board</button> -->
        </div>
      </div>
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
    mounted() {
      this.$store.dispatch("getVaultKeeps");
    },
    mounted() {
      this.$store.dispatch("getUserVaults");
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
        },
        newVaultKeep: {
          vaultId: "",
          keepId: "",
          userId: ""
        },
        chosen: ""
      };
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
      },
      addVaultKeep(keep) {
        debugger
        keep.keeps++
        this.$store.dispatch("updateKeep", keep)
        this.newVaultKeep.userId = this.user.id
        this.newVaultKeep.keepId = keep.id
        this.newVaultKeep.vaultId = this.chosen
        this.$store.dispatch("addVaultKeep", this.newVaultKeep);
        // this.newKeep = { name: "", description: "", img: "", userId: "" };
        debugger
      },
      addShare(keep) {
        keep.shares++
        this.$store.dispatch("updateKeep", keep)
      },
      viewKeep(keep) {
        keep.views++
        this.$store.dispatch("updateKeep", keep)
      },
      deleteKeep(keep) {
        debugger
        if (keep.userId == this.user.id) {
          this.$store.dispatch("deleteKeep", keep);
        }
      }
    }
  };
</script>