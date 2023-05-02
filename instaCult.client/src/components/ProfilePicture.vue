<template>
  <img class="img-fluid" :src="profile.picture" alt="">
</template>


<script>
import { Profile } from '../models/Account.js';

export default {
  props: {
    profile: { type: Profile, required: true }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>