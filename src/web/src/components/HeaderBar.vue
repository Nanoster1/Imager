<template>
  <div class="d-flex align-items-center justify-content-between px-2 py-2">
    <h1 class="headline-lg">Imager</h1>
    <div class="d-flex gap-3">
      <v-btn
        v-if="isAccount"
        @click="returnBack"
      >
        Назад на главную страницу
      </v-btn>
      <v-btn @click="handler">{{ buttonTitle }}</v-btn>
    </div>
  </div>
</template>

<script>
import { inject } from "vue";

export default {
  name: "HeaderBar",
  props: {
    isAccount: {
      type: Boolean,
      default: false
    }
  },

  emits: ["update:isAccount"],
  setup() {
    const status = inject("Vue3GoogleOauth");

    return {
      status
    };
  },

  computed: {
    buttonTitle() {
      return !this.status.isAuthorized ? "Войти" : this.isAccount ? "Выйти" : "Личный кабинет";
    }
  },

  methods: {
    returnBack() {
      window.localStorage.setItem("isAccount", false);
      this.$emit("update:isAccount", false);
    },

    async handler() {
      if (!this.status.isAuthorized) {
        this.user = await this.$gAuth.signIn();
        window.localStorage.setItem("user", JSON.stringify(this.user));
        window.localStorage.setItem("accessToken", this.user.Sc.access_token);
      } else {
        if (this.isAccount) {
          await this.$gAuth.signOut();
          this.returnBack();
          window.localStorage.setItem("user", "");
          window.localStorage.setItem("accessToken", "");
        } else {
          window.localStorage.setItem("isAccount", true);
          this.$emit("update:isAccount", true);
        }
      }
    }
  }
};
</script>
