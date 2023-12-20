<template>
  <div class="body d-flex flex-column gap-9 align-items-center">
    <resize-form class="w-100" />
  </div>
</template>

<script>
import ResizeForm from "../components/ResizeForm.vue";

export default {
  name: "MainPage",
  components: {
    ResizeForm
  }
};
</script>

<style scoped>
.body {
  margin-top: 20vh;
}
</style>
