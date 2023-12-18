<template>
  <div>
    <main-page />
  </div>
</template>

<script>
import MainPage from "./pages/MainPage.vue";

export default {
  name: "App",
  components: {
    MainPage
  }
};
</script>

<style lang="scss">
@use "assets/scss/main" as *;
</style>
