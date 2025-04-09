<script setup>
import Navbar from '@/common/components/Navbar.vue'
import { RouterView } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'

const authStore = useAuthStore()

authStore.loadUser()
</script>

<template>
  <Navbar v-if="authStore.isLoggedIn" />
  <RouterView />
</template>
