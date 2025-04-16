<script setup>
import { onMounted, reactive, ref } from 'vue'
import { useAuthStore } from '@/stores/authStore'
import { getFacilities } from '@/_services/facilitiesService'
import { FacilityParams } from '@/_models/params/facilityParams'

const authStore = useAuthStore()
const user = ref(authStore.user)

const state = reactive({
  facilities: [],
  isLoadingFacilities: false,
})

const fetchUserFacilities = async () => {
  const facilityParams = new FacilityParams()
  facilityParams.userId = user.value.id

  state.isLoadingFacilities = true

  var { facilities } = await getFacilities(facilityParams)

  state.facilities = facilities
  state.isLoadingFacilities = false
}

onMounted(async () => {
  await fetchUserFacilities()
})

const defaultImage = 'https://placehold.co/600x400/EEE/31343C'
</script>

<template>
  <section class="bg-green-50 min-h-screen py-12">
    <div class="max-w-3xl mx-auto bg-white shadow-md rounded-lg p-8">
      <!-- Profile Header -->
      <div class="flex flex-col items-center text-center">
        <img
          :src="user.photoUrl || defaultAvatar"
          alt="User Avatar"
          class="w-28 h-28 rounded-full border-4 border-green-500 shadow-md object-cover"
        />
        <h2 class="text-2xl font-bold mt-4 text-green-700">{{ user.username }}</h2>
        <p class="text-gray-600 mt-1">{{ user.email }}</p>
      </div>

      <!-- User Info -->
      <div class="mt-8 space-y-4">
        <div>
          <h3 class="text-lg font-semibold text-gray-700 mb-1">Roles</h3>
          <div class="flex flex-wrap gap-2">
            <span
              v-for="role in user.roles"
              :key="role"
              class="bg-green-100 text-green-700 text-sm font-semibold px-3 py-1 rounded-full"
            >
              {{ role }}
            </span>
          </div>
        </div>
      </div>

      <!-- User's Facility List -->
      <div class="mt-12">
        <h3 class="text-2xl font-bold text-green-700 mb-4">Your Facilities</h3>

        <!-- Loading state -->
        <div v-if="state.isLoadingFacilities" class="text-gray-500 py-4">Loading facilities...</div>

        <!-- Empty state -->
        <div v-else-if="state.facilities.length === 0" class="text-gray-500">
          You haven't added any facilities yet.
        </div>

        <!-- Facility cards -->
        <div v-else class="grid md:grid-cols-2 gap-4">
          <div
            v-for="facility in state.facilities"
            :key="facility.id"
            class="bg-white border border-gray-200 rounded-lg shadow-md p-4 flex flex-col"
          >
            <img
              :src="facility.photos?.find((p) => p.isMain)?.url || defaultImage"
              alt="Facility photo"
              class="w-full h-48 object-cover rounded mb-4"
              @error="(e) => (e.target.src = defaultImage)"
            />

            <h4 class="text-xl font-semibold text-green-800">{{ facility.facilityName }}</h4>
            <p class="text-gray-600 text-sm mb-4">{{ facility.detailAddress }}</p>

            <RouterLink
              :to="`/facilities/${facility.id}`"
              class="mt-auto text-center bg-green-600 text-white py-2 px-4 rounded hover:bg-green-700"
            >
              View
            </RouterLink>
          </div>
        </div>
      </div>

      <!-- Edit Button -->
      <div class="mt-10 text-right">
        <button class="bg-green-600 hover:bg-green-700 text-white px-5 py-2 rounded-lg transition">
          Edit Profile
        </button>
      </div>
    </div>
  </section>
</template>
