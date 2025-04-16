<script setup>
import { onMounted, reactive } from 'vue'
import FacilityListing from './FacilityListing.vue'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import { RouterLink } from 'vue-router'
import { getFacilities } from '@/_services/facilitiesService'
import { FacilityParams } from '@/_models/params/facilityParams'

defineProps({
  limit: Number,
  showButton: {
    type: Boolean,
    default: false,
  },
  showPagination: {
    type: Boolean,
    default: false,
  },
})

var facilityParams = new FacilityParams()

const state = reactive({
  facilities: [],
  pagination: {
    currentPage: 1,
    itemsPerPage: 9,
    totalItems: 0,
    totalPages: 0,
  },
  isLoading: true,
})

const goToPrev = () => {
  if (state.pagination.currentPage > 1) {
    facilityParams.pageNumber--

    getFacilitiesCall()
  }
}

const goToNext = async () => {
  if (state.pagination.currentPage < state.pagination.totalPages) {
    facilityParams.pageNumber++

    getFacilitiesCall()
  }
}

onMounted(async () => {
  getFacilitiesCall()
})

const getFacilitiesCall = () => {
  state.isLoading = true

  getFacilities(facilityParams).then(({ facilities, pagination }) => {
    state.facilities = facilities
    state.pagination = pagination

    state.isLoading = false
  })
}
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

      <div
        v-if="showPagination && state.pagination.totalPages > 1"
        class="flex justify-center mt-8 gap-4 items-center"
      >
        <button
          @click="goToPrev"
          :disabled="state.pagination.currentPage === 1"
          class="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600 disabled:bg-gray-300"
        >
          Previous
        </button>

        <span class="font-semibold text-gray-700">
          Page {{ state.pagination.currentPage }} of {{ state.pagination.totalPages }}
        </span>

        <button
          @click="goToNext"
          :disabled="state.pagination.currentPage === state.pagination.totalPages"
          class="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600 disabled:bg-gray-300"
        >
          Next
        </button>
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
