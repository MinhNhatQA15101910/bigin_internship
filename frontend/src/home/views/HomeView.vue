<script setup>
import Hero from '@/home/components/Hero.vue'
import HomeCards from '@/home/components/HomeCards.vue'

import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import FacilityListings from '@/facility-listings/components/FacilityListings.vue'

const authStore = useAuthStore()
const router = useRouter()

onMounted(() => {
  if (!authStore.isLoggedIn) {
    router.push('/login')
  }
})
</script>

<template>
  <Hero />
  <HomeCards />
  <FacilityListings :limit="3" showButton />
</template>
