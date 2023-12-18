<template>
  <div class="d-flex flex-column gap-5 justify-content-center">
    <div>
      <div class="wrapper d-flex align-items-center">
        <input
          class="loader"
          type="file"
          accept="image/*"
          multiple
          @input="fileToBinary"
        />
        <span
          class="headline-lg"
          v-html="headline"
        >
        </span>
      </div>

      <div class="controls d-flex flex-column gap-5 justify-content-between">
        <div class="inputs d-flex flex-column">
          <v-text-field
            v-model="width"
            label="Ширина, в пикселях"
            variant="outlined"
          />

          <v-text-field
            v-model="height"
            label="Высота, в пикселях"
            variant="outlined"
          />
        </div>

        <v-btn
          class="button"
          size="x-large"
          variant="tonal"
          @click="sendFetch"
        >
          Изменить размер
        </v-btn>
      </div>
    </div>

    <div v-if="!!files">
      <h3 class="headline-sm">Список загруженных файлов</h3>
      <span
        v-for="file in files"
        :key="file"
        >{{ file }}
      </span>
    </div>
  </div>
</template>

<script>
// import { inject } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";

export default {
  name: "ResizeForm",
  setup() {
    const signalr = useSignalR();

    return {
      signalr
    };
  },

  data() {
    return {
      files: [],
      binaryFiles: null,
      width: 0,
      height: 0
    };
  },

  computed: {
    headline() {
      return !this.fileName
        ? "Нажмите, чтобы загрузить изображение"
        : `Файл(ы) успешно загружен(ы). <br/> <br/> Нажмите еще раз, если хотите загрузить другой файл`;
    }
  },

  methods: {
    fileToBinary(event) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files);
      this.files = event.target.files.name;
      reader.onload = () => {
        this.binaryFiles = reader.result;
      };
    },

    sendFetch() {
      const accessToken = window.localStorage.getItem("accessToken");
      console.log(accessToken);
      fetch("http://localhost:5000/resize", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      });
    }
  }
};
</script>

<style scoped lang="scss">
.wrapper {
  position: relative;

  width: 30vw;
  height: 50vh;
  padding: 24px;

  text-align: center;

  background-color: $secondary-brand-20;
  border-radius: 20px;
}

.loader {
  position: absolute;
  top: 0;
  left: 0;

  width: 100%;
  height: 100%;

  opacity: 0;
  border-radius: 20px;
}

.inputs {
  width: 250px;
}

.button {
  color: white;
  background-color: $secondary-brand-60;
}

.controls {
  padding: 16px 0;
}
</style>
