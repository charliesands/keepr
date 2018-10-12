<template>
  <div class="dashboard container-fluid">
    <div>
      <h1>Dashboard</h1>
    </div>
    <div>
      <form @submit.prevent>
        <select v-model="chosen">
          <option diabled value="">Choose Vault</option>
          <option v-for="vault in vaults" v-bind:value="vault.id">{{vault.name}}</option>
        </select>
        <input type="submit" @click="getVaultKeeps(chosen)">
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
        this.$store.dispatch('getVaultKeeps', vault)
      },
      addShare(vk) {
        vk.shares++
        this.$store.dispatch("updateKeep", vk)
      },
      viewKeep(vk) {
        vk.views++
        this.$store.dispatch("updateKeep", vk)
      }
    }
  }

</script>