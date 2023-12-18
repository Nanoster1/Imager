<template>
  <div>
    <header-bar />
    <page-body />
  </div>
</template>

<script>
import HeaderBar from "../components/HeaderBar.vue";
import PageBody from "../components/PageBody.vue";

export default {
  name: "MainPage",
  components: {
    HeaderBar,
    PageBody
  }
};
</script>
