<template>
  <div class="container text-light">
    <section v-if="cult" class="row">
      <div class="col-8">
        <h1 class="text-secondary">{{ cult.name }}</h1>
      </div>
      <div class="col-12 col-md-6">
        <img class="img-fluid" :src="cult.leader.picture" alt="">
        <p>{{ cult.leader.name }}</p>
      </div>
      <div class="col-12 col-md-6">
        <p>{{ cult.description }}</p>
        <div class="bg-black p-2 rounded">
          things this cult likes
          <span v-for="t in cult.tags" class="border-bottom border-2 mx-2 border-primary selectable text-primary">{{ t
          }}</span>
        </div>
      </div>

    </section>
    <section v-else class="mdi fs-1"> 🕴️</section>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { useRoute } from 'vue-router';
export default {
  setup() {
    onMounted(() => {
      getCultById()
    })
    const route = useRoute()
    async function getCultById() {
      try {
        await cultsService.getCultById(route.params.cultId)
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      cult: computed(() => AppState.activeCult)
    }
  }
};
</script>


<style lang="scss" scoped></style>
