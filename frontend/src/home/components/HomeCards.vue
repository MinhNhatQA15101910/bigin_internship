<script setup>
import Card from '@/home/components/Card.vue'
import { RouterLink } from 'vue-router'
</script>

<template>
  <section class="py-4">
    <div class="container-xl lg:container m-auto">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4 p-4 rounded-lg">
        <Card>
          <h2 class="text-2xl font-bold">For Players</h2>
          <p class="mt-2 mb-4">Find and book badminton courts near you in seconds</p>
          <RouterLink
            to="/jobs"
            class="inline-block bg-black text-white rounded-lg px-4 py-2 hover:bg-gray-700"
          >
            Find Courts
          </RouterLink>
        </Card>
        <Card bg="bg-green-100">
          <h2 class="text-2xl font-bold">For Court Owners</h2>
          <p class="mt-2 mb-4">Add your facility and start managing bookings with ease</p>
          <RouterLink
            to="/jobs/add"
            class="inline-block bg-green-500 text-white rounded-lg px-4 py-2 hover:bg-green-600"
          >
            Add Your Court
          </RouterLink>
        </Card>
      </div>
    </div>
  </section>
</template>
