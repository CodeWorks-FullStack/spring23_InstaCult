<template>
  <div class="container-fluid text-light">
    <h2>These are the cults on offer</h2>
    <section class="row">
      <div v-for="c in cults" class="col-6 col-md-3">
        <CultCard :cult="c" />
      </div>
    </section>
  </div>
  <button data-bs-toggle="modal" data-bs-target="#create-cult" class="btn btn-primary position-fixed fab">Become A 🕴️
    today</button>
  <Modal id="create-cult" size="modal-lg">
    <template #header>Create A Cult</template>
    <template #body>
      <CultForm />
    </template>

  </Modal>
</template>

<script>
import { computed, onMounted } from 'vue';
import { cultsService } from '../services/CultsService.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';

export default {
  setup() {
    onMounted(() => {
      getCults()
    })
    async function getCults() {
      try {
        await cultsService.getCults()
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      cults: computed(() => AppState.cults)
    }
  }
}
</script>

<style scoped lang="scss">
.fab {
  right: 1em;
  bottom: 1em;
}
</style>
