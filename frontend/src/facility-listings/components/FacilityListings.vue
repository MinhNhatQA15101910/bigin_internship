<script setup>
import { onMounted, reactive } from 'vue'
import FacilityListing from './FacilityListing.vue'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import { RouterLink } from 'vue-router'
import { getFacilities } from '@/_services/facilitiesService'

defineProps({
  limit: Number,
  showButton: {
    type: Boolean,
    default: false,
  },
})

const state = reactive({
  facilities: [],
  isLoading: true,
})

onMounted(async () => {
  state.facilities = await getFacilities()
  console.log(state.facilities)

  state.isLoading = false
})
</script>

<template>
  <section class="bg-blue-100 py-10 px-4">
    <div class="container-xl lg:container m-auto">
      <h2 class="text-3xl font-bold text-green-500 mb-6 text-center">Browse Facilities</h2>

      <!-- Show loading spinner while loading is true -->
      <div v-if="state.isLoading" class="text-center text-gray-500 py-6">
        <PulseLoader />
      </div>

      <!-- Show job listing when done loading -->
      <div v-else class="grid grid-cols-1 md:grid-cols-3 gap-6">
        <FacilityListing
          v-for="facility in state.facilities.slice(0, limit || state.facilities.length)"
          :key="facility.id"
          :facility="facility"
        />
      </div>
    </div>
  </section>

  <section v-if="showButton" class="m-auto max-w-lg my-10 px-6">
    <RouterLink
      to="/facilities"
      class="block bg-black text-white text-center py-4 px-6 rounded-xl hover:bg-gray-700"
      >View All Facilities</RouterLink
    >
  </section>
</template>
