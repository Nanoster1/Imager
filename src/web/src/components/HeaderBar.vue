<template>
  <div class="d-flex align-items-center justify-content-between px-2 py-2">
    <h1 class="headline-lg">Imager</h1>
    <h1>{{ status.isAuthorized }}</h1>
    <v-btn @click="handler">{{ buttonTitle }}</v-btn>
  </div>
</template>

<script>
import { inject } from "vue";

export default {
  name: "HeaderBar",
  setup() {
    const status = inject("Vue3GoogleOauth");
    return {
      status
    };
  },

  data() {
    return {
      user: {}
    };
  },

  computed: {
    buttonTitle() {
      return this.status.isAuthorized ? "Выйти" : "Войти";
    }
  },

  methods: {
    async handler() {
      if (!this.status.isAuthorized) {
        this.user = await this.$gAuth.signIn();
        window.localStorage.setItem("user", JSON.stringify(this.user));
        window.localStorage.setItem("accessToken", this.user.Sc.access_token);
      } else {
        this.user = await this.$gAuth.signOut();
        window.localStorage.setItem("user", "");
        window.localStorage.setItem("accessToken", "");
      }
    }
  }
};
</script>

<!-- <style lang="scss"></style> -->
