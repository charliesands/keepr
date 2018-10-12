<template>
    <div class="login container-fluid">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="#">myKeepr</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown"
                aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavDropdown">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#loginModal"
                            data-backdrop="false">Login/Register</button>
                    </li>
                    <!-- <li class="nav-item">
                        <a class="nav-link" href="#">Register</a>
                    </li> -->
                </ul>
            </div>
        </nav>

        <div class="modal fade" id="loginModal" tabindex="-1" role="dialog" aria-labelledby="loginModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="loginModalLabel">Login</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form v-if="loginForm" @submit.prevent="loginUser">
                            <input type="email" v-model="creds.email" placeholder="email">
                            <input type="password" v-model="creds.password" placeholder="password">
                            <button type="submit">Login</button>
                        </form>
                        <form v-else @submit.prevent="register">
                            <input type="text" v-model="newUser.username" placeholder="name">
                            <input type="email" v-model="newUser.email" placeholder="email">
                            <input type="password" v-model="newUser.password" placeholder="password">
                            <button type="submit">Create Account</button>
                        </form>
                        <div @click="loginForm = !loginForm">
                            <p v-if="loginForm">No account Click to Register</p>
                            <p v-else>Already have an account click to Login</p>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <!-- <button type="button" class="btn btn-primary">Register</button> -->
                    </div>
                </div>
            </div>
        </div>

        <!-- <form v-if="loginForm" @submit.prevent="loginUser">
            <input type="email" v-model="creds.email" placeholder="email">
            <input type="password" v-model="creds.password" placeholder="password">
            <button type="submit">Login</button>
        </form>
        <form v-else @submit.prevent="register">
            <input type="text" v-model="newUser.username" placeholder="name">
            <input type="email" v-model="newUser.email" placeholder="email">
            <input type="password" v-model="newUser.password" placeholder="password">
            <button type="submit">Create Account</button>
        </form>
        <div @click="loginForm = !loginForm">
            <p v-if="loginForm">No account Click to Register</p>
            <p v-else>Already have an account click to Login</p>
        </div> -->
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
                    <button type="button" class="btn btn-primary" data-toggle="modal" :data-target="'#keep'+keep.id"
                        @click="viewKeep(keep)">View</button>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#loginModal"
                        data-backdrop="false">Keep</button>
                    <!-- <button class="btn-danger" @click="deleteBoard(board._id)">Delete Board</button> -->
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
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "login",
        mounted() {
            //checks for valid session
            this.$store.dispatch("authenticate");
        },
        mounted() {
            this.$store.dispatch("getKeeps");
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                }
            };
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
        computed: {
            keeps() {
                return this.$store.state.keeps;
            },
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            },
            addShare(keep) {
                keep.shares++
                this.$store.dispatch("updateKeep", keep)
            },
            viewKeep(keep) {
                keep.views++
                this.$store.dispatch("updateKeep", keep)
            }
        }
    };
</script>