<!-- eslint-disable vue/no-v-html -->
<template>
  <div class="d-flex gap-9 justify-content-center">
    <div class="d-flex gap-5">
      <div class="wrapper d-flex align-items-center justify-content-center">
        <input
          class="loader"
          type="file"
          accept="image/*"
          multiple
          @input="fileToBinary"
        />
        <span
          class="headline-sm"
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
          :disabled="!(width && height && fileNames.length)"
          @click="resize"
        >
          Изменить размер
        </v-btn>
      </div>
    </div>

    <div
      v-if="!!fileNames?.length"
      class="d-flex flex-column gap-5"
    >
      <h3 class="headline-md">Список загруженных файлов</h3>
      <span
        v-for="file in fileNames"
        :key="file"
      >
        {{ file }}
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

    signalr.on("SendResizedImageInfo", (id) => {
      const response = fetch(`http://localhost:5000/resize/${id}`, {
        method: "GET",
        Authorization: `Bearer ${this.accessToken}`
      });
      const imageRes = response.json().then((data) => {
        return {
          name: data.imageId,
          image: atob(data.image.imageInBytes)
        };
      });
      this.gettingImages.push(imageRes);
    });

    return {
      signalr
    };
  },

  data() {
    return {
      fileNames: [],
      binaryFiles: [],
      width: 0,
      height: 0,
      imageIds: [],
      accessToken: "",
      gettingImages: []
    };
  },

  computed: {
    headline() {
      return !this.fileNames.length
        ? "Нажмите, чтобы загрузить изображение"
        : `Файл(ы) успешно загружен(ы). <br/> <br/> Нажмите еще раз, если хотите загрузить другой файл`;
    }
  },

  methods: {
    async fileToBinary(event) {
      for (const file of event.target.files) {
        const result = await this.readFile(file);

        this.fileNames.push(file.name);
        this.binaryFiles.push({
          imageInBytes: btoa(result),
          format: file.type.split("/")[1]
        });
      }
    },

    readFile(file) {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => {
          resolve(reader.result);
        };
        reader.onerror = reject;
        reader.readAsArrayBuffer(file);
      });
    },

    async resize() {
      this.accessToken = window.localStorage.getItem("accessToken");
      const response = await fetch("http://localhost:5000/resize", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${this.accessToken}`,
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          width: this.width,
          height: this.height,
          imageModels: this.binaryFiles
        })
      });
      this.imageIds = await response.json().then((data) => data.imageIds);
    },

    convertBytesObjectInArray(obj) {
      const result = [];
      for (const value of obj) {
        result.push(+value);
      }
      console.log(result);
      return result;
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

  &:disabled {
    background-color: $secondary-content-64;
  }
}

.controls {
  padding: 16px 0;
}
</style>
