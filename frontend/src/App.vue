<script setup>
import { RouterView } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'

const authStore = useAuthStore()

authStore.loadUser()
</script>

<template>
  <RouterView />
</template>
