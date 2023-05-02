<template>
  <div class="container text-light">
    <section v-if="cult" class="row">
      <div class="col-8 d-flex align-items-center">
        <h1 class="text-secondary">{{ cult.name }}</h1>
        <span v-if="account.id && !isCultist">
          <button @click="createCultMember()" class="btn btn-danger ms-3">Join us, human</button>
        </span>
        <span v-else-if="account.id">
          <button @click="removeCultMember(isCultist?.cultMemberId)" class="btn btn-success ms-3">Leave us, human</button>
        </span>
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
        <div class="container-fluid">
          <div class="row">
            <div v-for="cultist in cultists" :key="cultist.id" class="col-3">
              <ProfilePicture :profile="cultist" class="cultist-picture" :title="cultist.name" />
            </div>
          </div>
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
import { cultMembersService } from '../services/CultMembersService.js'
import { useRoute } from 'vue-router';
import ProfilePicture from '../components/ProfilePicture.vue';
import { logger } from '../utils/Logger.js';
export default {
  setup() {
    onMounted(() => {
      getCultById();
      getCultMembers();
    });
    const route = useRoute();
    async function getCultById() {
      try {
        await cultsService.getCultById(route.params.cultId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getCultMembers() {
      try {
        await cultMembersService.getCultMembers(route.params.cultId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      cult: computed(() => AppState.activeCult),
      cultists: computed(() => AppState.cultists),
      account: computed(() => AppState.account),
      isCultist: computed(() => AppState.cultists.find(c => c.id == AppState.account.id)),
      async createCultMember() {
        try {
          const cultId = route.params.cultId
          await cultMembersService.createCultMember(cultId)
        } catch (error) {
          Pop.error(error)
        }
      },
      async removeCultMember(cultMemberId) {

        try {
          const wantsToLeave = await Pop.confirm('Why would you ever click this button?', 'You probably clicked that button an accident; right?', 'No, I really clicked this button')

          if (!wantsToLeave) {
            Pop.success("I'm glad you're staying")
            return
          }

          const reallyWantsToLeave = await Pop.confirm("No please don't go", "we are nothing without you", "Please I have a family")

          if (!reallyWantsToLeave) {
            Pop.success('Welcome home')
            return
          }

          Pop.toast("I'll give you 2 seconds to think about it", 'info', 'center', 2000)
          setTimeout(async () => {
            const id = cultMemberId
            if (await Pop.confirm('this is your last chance', '', "I'm ready for this to be over")) {
              cultMembersService.removeCultMember(id)
            }
          }, 2000)


        } catch (error) {
          Pop.error(error)
        }
      },

      async theRemoveYouShouldReference(cultMemberId) {
        if (await Pop.confirm()) {
          cultMembersService.removeCultMember(cultMemberId)
        }
      }
    };
  },
  components: { ProfilePicture }
};
</script>


<style lang="scss">
.cultist-picture {
  height: 20vh;
  width: 20vh;
  border-radius: 50%;
  box-shadow: 0 0 10px rgb(226, 92, 92);
}
</style>
