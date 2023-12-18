<template>
  <div class="d-flex flex-column gap-9 align-items-center">
    <resize-form class="w-100" />
  </div>
</template>

<script>
import ResizeForm from "./ResizeForm.vue";

export default {
  name: "PageBody",
  components: {
    ResizeForm
  }
};
</script>
