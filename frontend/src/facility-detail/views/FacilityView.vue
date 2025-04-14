<script setup>
import BackButton from '@/common/components/BackButton.vue'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import axios from 'axios'
import { nextTick, onMounted, reactive, watchEffect } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { Carousel, Slide } from 'vue3-carousel'
import L from 'leaflet'
import 'vue3-carousel/dist/carousel.css'
import 'leaflet/dist/leaflet.css'

// Fix Leaflet icon paths (optional but recommended)
import markerIcon2x from 'leaflet/dist/images/marker-icon-2x.png'
import markerIcon from 'leaflet/dist/images/marker-icon.png'
import markerShadow from 'leaflet/dist/images/marker-shadow.png'
delete L.Icon.Default.prototype._getIconUrl
L.Icon.Default.mergeOptions({
  iconRetinaUrl: markerIcon2x,
  iconUrl: markerIcon,
  shadowUrl: markerShadow,
})

const route = useRoute()
const router = useRouter()
const facilityId = route.params.id

const state = reactive({
  facility: null,
  isLoading: true,
})

const defaultImage = 'https://placehold.co/600x400/EEE/31343C'

const handleImageError = (e) => {
  e.target.src = defaultImage
}

const approveFacility = () => {}
const rejectFacility = () => {}
const editFacility = () => {}

onMounted(async () => {
  try {
    const { data } = await axios.get(`/api/facilities/${facilityId}`)
    state.facility = data
  } catch (error) {
    console.error('Error loading facility:', error)
  } finally {
    state.isLoading = false
  }
})

watchEffect(async () => {
  if (!state.facility?.location?.coordinates) return

  await nextTick()

  const mapContainer = document.getElementById('map')
  if (!mapContainer || mapContainer._leaflet_id) return // prevent double init

  const [lng, lat] = state.facility.location.coordinates

  const map = L.map(mapContainer).setView([lat, lng], 16)

  L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; OpenStreetMap contributors',
  }).addTo(map)

  L.marker([lat, lng]).addTo(map).bindPopup(state.facility.facilityName).openPopup()

  // Make sure the map adjusts to container size
  setTimeout(() => {
    map.invalidateSize()
  }, 0)
})
</script>

<template>
  <BackButton />

  <section v-if="!state.isLoading" class="bg-green-50 min-h-screen">
    <div class="container mx-auto py-10 px-4">
      <!-- Facility Header -->
      <div class="bg-white rounded-lg shadow-xl overflow-hidden">
        <Carousel
          v-if="state.facility.photos.length"
          :autoplay="3000"
          :interval="3000"
          :navigationEnabled="true"
          :wrapAround="true"
          class="rounded-t-lg"
        >
          <Slide v-for="photo in state.facility.photos" :key="photo.id">
            <img
              :src="photo.url"
              @error="handleImageError"
              class="object-cover w-full h-96"
              alt="Facility photo"
            />
          </Slide>
        </Carousel>

        <div class="p-6">
          <h1 class="text-3xl font-extrabold text-gray-800 mb-1">
            {{ state.facility.facilityName }}
          </h1>
          <p class="text-gray-600 mb-2">
            <i class="pi pi-map-marker text-orange-600 mr-2"></i>
            {{ state.facility.detailAddress }}
          </p>

          <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
            <!-- Description -->
            <div>
              <h3 class="text-lg font-semibold text-green-800 mb-2">Description</h3>
              <p class="text-gray-700">{{ state.facility.description }}</p>
            </div>

            <!-- Policy -->
            <div>
              <h3 class="text-lg font-semibold text-green-800 mb-2">Policy</h3>
              <pre class="whitespace-pre-wrap text-gray-700">{{ state.facility.policy }}</pre>
            </div>

            <!-- Price Range -->
            <div>
              <h3 class="text-lg font-semibold text-green-800 mb-2">Price Range</h3>
              <p class="text-gray-700">
                {{ state.facility.minPrice || 'Not Updated' }} -
                {{ state.facility.maxPrice || 'Not Updated' }} VND
              </p>
            </div>

            <!-- Facebook Link -->
            <div>
              <h3 class="text-lg font-semibold text-green-800 mb-2">Facebook</h3>
              <a
                v-if="state.facility.facebookUrl"
                :href="state.facility.facebookUrl"
                class="text-blue-600 hover:underline"
                target="_blank"
              >
                Visit Facebook Page
              </a>
              <span v-else class="text-gray-500 italic">Not updated</span>
            </div>
          </div>
        </div>
      </div>

      <!-- Manager Info & Map -->
      <div class="grid md:grid-cols-2 gap-6 mt-10">
        <!-- Manager Info -->
        <div class="bg-white p-6 rounded-lg shadow-md">
          <h3 class="text-xl font-bold mb-4">Manager Info</h3>
          <p class="font-semibold">{{ state.facility.managerInfo.fullName }}</p>
          <p>Email: {{ state.facility.managerInfo.email }}</p>
          <p>Phone: {{ state.facility.managerInfo.phoneNumber }}</p>

          <div class="grid grid-cols-2 gap-2 mt-4">
            <img
              :src="state.facility.managerInfo.citizenImageFront.url"
              @error="handleImageError"
            />
            <img :src="state.facility.managerInfo.citizenImageBack.url" @error="handleImageError" />
            <img :src="state.facility.managerInfo.bankCardFront.url" @error="handleImageError" />
            <img :src="state.facility.managerInfo.bankCardBack.url" @error="handleImageError" />
          </div>
        </div>

        <!-- Map -->
        <div class="bg-white p-6 rounded-lg shadow-md">
          <h3 class="text-xl font-bold mb-4">Map Location</h3>
          <div id="map" class="w-full h-[30vh] md:h-64 rounded-lg shadow-inner bg-gray-200"></div>
        </div>
      </div>

      <!-- Action Buttons -->
      <div class="flex justify-end gap-4 mt-10">
        <button
          class="bg-green-600 hover:bg-green-700 text-white py-2 px-5 rounded-lg font-semibold"
        >
          Approve
        </button>
        <button class="bg-red-600 hover:bg-red-700 text-white py-2 px-5 rounded-lg font-semibold">
          Reject
        </button>
        <button class="bg-blue-600 hover:bg-blue-700 text-white py-2 px-5 rounded-lg font-semibold">
          Edit
        </button>
      </div>
    </div>
  </section>

  <div v-else class="text-center py-20">
    <PulseLoader />
  </div>
</template>

<style scoped>
.carousel__prev,
.carousel__next {
  color: white !important;
}

#map {
  min-height: 256px;
  border-radius: 0.5rem;
}
</style>
