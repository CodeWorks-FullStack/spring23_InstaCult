<template>
  <form @submit.prevent="createCult">
    <div class="fs-2">Lead a Cult</div>
    <div class="mb-3">
      <label for="" class="form-label">Name</label>
      <input v-model="editable.name" type="text" class="form-control" name="" id="" aria-describedby="helpId"
        placeholder="">
      <small id="helpId" class="form-text text-muted">Name your cult</small>
    </div>
    <div class="mb-3">
      <label for="" class="form-label">description</label>
      <textarea v-model="editable.description" class="w-100" name="" id="" rows="5"></textarea>
      <small id="helpId" class="form-text text-muted">Tell us about your cult</small>
    </div>
    <div class="mb-3">
      <label for="" class="form-label">Tags</label>
      <input v-model="editable.tags" type="text" class="form-control" name="" id="" aria-describedby="helpId"
        placeholder="what does your cult like">
      <small id="helpId" class="form-text text-muted">comma space separated</small>
    </div>
    <div class="mb-3 text-end">
      <button class="btn btn-danger">Submit</button>
    </div>
  </form>
</template>


<script setup>
import { ref } from 'vue';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { router } from '../router.js';
import { Modal } from 'bootstrap';


const editable = ref({})
async function createCult() {
  try {
    let newCult = await cultsService.createCult(editable.value)
    Modal.getOrCreateInstance('#create-cult').hide() // hide the modal before you router push or you will be sorry (your backdrop will be stuck open)
    router.push({ name: 'CultDetails', params: { cultId: newCult.id } })
  } catch (error) {
    Pop.error(error)
  }
}
</script>


<style lang="scss" scoped></style>
