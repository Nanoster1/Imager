<template>
  <div class="d-flex flex-column gap-5">
    <div class="d-flex flex-column gap-4">
      <span class="title-lg">Вы авторизованы под именем: {{ user?.sy?.Zf }}</span>
      <v-text-field
        v-model="email"
        label="Почта для отправки изображений"
        variant="outlined"
      />
    </div>

    <div class="d-flex flex-column gap-3 align-items-center">
      <span class="headline-md">Список изображений</span>

      <span
        v-if="!images.length"
        class="title-md"
      >
        Вы еще ни разу не загружали фотографии для ресайза
      </span>
      <span
        v-if="!images.length"
        class="title-md"
      >
        Вы можете сделать это на главной странице
      </span>
      <div v-else>
        <!-- TODO:добавить список картинок -->
        <v-btn>Отправить на почту</v-btn>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AccountPage",

  data() {
    return {
      user: {},
      images: [],
      email: ""
    };
  },

  async mounted() {
    this.user = JSON.parse(window.localStorage.getItem("user"));
    this.email = this.user?.sy?.jz;
    const accessToken = window.localStorage.getItem("accessToken");

    const response = await fetch("http://localhost:5000/images/all", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
    this.images = await response.json().then((data) => atob(data.images));
  }
};
</script>
