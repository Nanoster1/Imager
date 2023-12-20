<template>
  <div>
    <header-bar v-model:is-account="isAccount" />

    <account-page v-if="isAccount" />
    <main-page v-else />
  </div>
</template>

<script>
import MainPage from "./pages/MainPage.vue";
import AccountPage from "./pages/AccountPage.vue";
import HeaderBar from "./components/HeaderBar.vue";

export default {
  name: "App",
  components: {
    MainPage,
    AccountPage,
    HeaderBar
  },

  data() {
    return {
      isAccount: false,
      imagesIds: []
    };
  },

  mounted() {
    this.isAccount = JSON.parse(window.localStorage.getItem("isAccount")) === true;
  }
};
</script>

<style lang="scss">
@use "assets/scss/main" as *;
</style>
