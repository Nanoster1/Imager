<template>
  <div class="d-flex gap-5 justify-content-center">
    <!-- <v-file-input
      v-model="photo"
      class="wrapper hedline-lg"
      label="Нажмите сюда, чтобы загрузить фото"
    >
    </v-file-input> -->

    <div class="wrapper d-flex align-items-center">
      <input
        class="loader"
        type="file"
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
          label="Ширина, в пикселях"
          variant="outlined"
        />

        <v-text-field
          label="Высота, в пикселях"
          variant="outlined"
        />
      </div>

      <v-btn
        class="button"
        size="x-large"
        variant="tonal"
      >
        Button
      </v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "ResizeForm",
  data() {
    return {
      fileName: "",
      binaryFile: null,
      width: 0,
      height: 0
    };
  },

  computed: {
    headline() {
      return !this.fileName
        ? "Нажмите, чтобы загрузить изображение"
        : `Файл <br/> <strong>${this.fileName}</strong> <br/> успешно загружен. <br/> <br/> Нажмите еще раз, если хотите загрузить другой файл`;
    }
  },

  methods: {
    fileToBinary(event) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      this.fileName = event.target.files[0].name;
      reader.onload = () => {
        this.binaryFile = reader.result;
      };
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
