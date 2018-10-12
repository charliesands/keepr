<template>
  <div class="dashboard container-fluid">
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
    <div>
      <h1>Dashboard</h1>
    </div>
    <div>
      <form @submit.prevent>
        <select v-model="chosen">
          <option diabled value="">Choose Vault</option>
          <option v-for="vault in vaults" v-bind:value="vault.id">{{vault.name}}</option>
        </select>
        <button type="submit" @click="getVaultKeeps(chosen)">View</button>
        <button type="submit" @click="deleteVault(chosen)">Delete</button>
      </form>
    </div>
    <div class="row">
      <div v-for="vk in vaultKeeps" :key="vk._id" class="col-3">
        <div @click="viewKeep(vk)" data-toggle="modal" :data-target="'#vk'+vk.id">
          <img :src="vk.img">
        </div>
        <div>
          <h3> {{vk.name}}</h3>
        </div>
        <div>
          <p> shares:{{vk.shares}} views:{{vk.views}} keeps:{{vk.keeps}} </p>
        </div>
        <button type="button" class="btn btn-primary" @click="addShare(vk)">Share</button>
        <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#vk'+vk.id" @click="viewKeep(vk)">View</button>
        <!-- <button type="button" class="btn btn-primary">Keeps</button> -->
        <button type="button" class="btn btn-danger" @click="deleteVaultKeep(vk.id)">Remove</button>


        <div class="modal fade" :id="'vk'+vk.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
          aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">{{vk.name}}</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                  <img :src="vk.img">
                </div>
                <div>
                  {{vk.description}}
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12">
        <h2>Your Keeps</h2>
      </div>
      <div v-for="uk in userKeeps" :key="uk._id" class="col-3">
        <div @click="viewKeep(uk)" data-toggle="modal" :data-target="'#uk'+uk.id">
          <img :src="uk.img">
        </div>
        <div>
          <h3> {{uk.name}}</h3>
        </div>
        <div>
          shares:{{uk.shares}}views:{{uk.views}}keeps:{{uk.keeps}}
        </div>
        <button type="button" class="btn btn-primary" @click="addShare(uk)">Share</button>
        <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#uk'+uk.id" @click="viewKeep(uk)">View</button>
        <!-- <button type="button" class="btn btn-primary">Keep</button> -->
        <!-- <button class="btn btn-danger" @click="deleteKeep(uk)">Delete</button> -->
        <div class="modal fade" :id="'uk'+uk.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
          aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">{{uk.name}}</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                  <img :src="uk.img">
                </div>
                <div>
                  {{uk.description}}
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>
      </div>


    </div>
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
    mounted() {
      this.$store.dispatch("getUserVaults");
      this.$store.dispatch("getUserKeeps");
      document.body.classList.remove('modal-open')
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
      userKeeps() {
        return this.$store.state.userKeeps;
      }
    },
    methods: {
      getVaultKeeps(vault) {
        this.$store.dispatch('getVaultKeeps', vault)
      },
      addShare(vk) {
        vk.shares++
        this.$store.dispatch("updateKeep", vk)
      },
      viewKeep(vk) {
        vk.views++
        this.$store.dispatch("updateKeep", vk)
      },
      deleteVault(vault) {
        this.$store.dispatch("deleteVault", vault)
      },
      logout() {
        this.$store.dispatch("logout")
      },
      deleteVaultKeep(keepId) {
        this.$store.dispatch("deleteVaultKeep", { keepId: keepId, vaultId: this.chosen })
      },
      addShare(keep) {
        keep.shares++
        this.$store.dispatch("updateKeep", keep)
      },
      viewKeep(keep) {
        keep.views++
        this.$store.dispatch("updateKeep", keep)
      },
      deleteKeep(uk) {
        // if (uk.userId == this.user.id) 
        this.$store.dispatch("deleteKeep", uk);
      }
    }
  }

</script>