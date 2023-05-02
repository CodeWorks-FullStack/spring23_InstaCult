<template>
  <div class="card bg-dark text-center">
    <div>
      <ProfilePicture :profile="cult.leader" />
      <p>{{ cult.leader.name }}</p>
    </div>
    <div class="p-2">
      <router-link :to="{ name: 'CultDetails', params: { cultId: cult.id } }">
        <h3>{{ cult.name }}</h3>
      </router-link>
      <div class="d-flex">
        <span v-for="t in cult.tags" class="text-center bg-primary text-dark px-1 rounded-pill darken-20 selectable">{{ t
        }}</span>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Cult } from '../models/Cult.js';
import ProfilePicture from './ProfilePicture.vue';
export default {
  props: { cult: { type: Cult, required: true } },
  setup() {
    return {};
  },
  components: { ProfilePicture }
};
</script>


<style lang="scss" scoped></style>
