<script setup>
import logo from '@/assets/img/logo.png'
import { RouterLink, useRoute, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import { ref, onMounted, onBeforeUnmount } from 'vue'

const authStore = useAuthStore()
const user = authStore.user
const route = useRoute()
const router = useRouter()

const isActiveLink = (routePath) => route.path === routePath

// Dropdown state
const showDropdown = ref(false)

// Toggle dropdown visibility
const toggleDropdown = () => {
  showDropdown.value = !showDropdown.value
}

// Close dropdown when clicking outside
const dropdownRef = ref(null)
const handleClickOutside = (event) => {
  if (dropdownRef.value && !dropdownRef.value.contains(event.target)) {
    showDropdown.value = false
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
})

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside)
})

// Actions
const goToProfile = () => router.push('/profile')
const goToSettings = () => router.push('/settings')
const logout = () => authStore.logout()
</script>

<template>
  <nav class="bg-green-700 border-b border-green-500">
    <div class="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
      <div class="flex h-20 items-center justify-between">
        <!-- Left Section: Logo and Nav Links -->
        <div class="flex flex-1 items-center justify-start">
          <!-- Logo -->
          <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
            <img class="h-10 w-auto" :src="logo" alt="BadCourt Logo" />
            <span class="hidden md:block text-white text-2xl font-bold ml-2">BadCourt</span>
          </RouterLink>

          <!-- Nav Links -->
          <div class="ml-4 flex space-x-2">
            <RouterLink
              to="/"
              :class="[
                isActiveLink('/') ? 'bg-green-900' : 'hover:bg-green-900 hover:text-white',
                'text-white rounded-md px-3 py-2',
              ]"
              >Home</RouterLink
            >

            <RouterLink
              to="/jobs"
              :class="[
                isActiveLink('/jobs') ? 'bg-green-900' : 'hover:bg-green-900 hover:text-white',
                'text-white rounded-md px-3 py-2',
              ]"
              >Facilities</RouterLink
            >

            <RouterLink
              to="/jobs/add"
              :class="[
                isActiveLink('/jobs/add') ? 'bg-green-900' : 'hover:bg-green-900 hover:text-white',
                'text-white rounded-md px-3 py-2',
              ]"
              >Add Facility</RouterLink
            >
          </div>
        </div>

        <!-- Right Section: User Dropdown -->
        <div class="relative" ref="dropdownRef">
          <button
            class="flex items-center space-x-2 text-white focus:outline-none"
            @click="toggleDropdown"
          >
            <img
              :src="user.photoUrl"
              alt="User Avatar"
              class="w-10 h-10 rounded-full border-2 border-white"
            />
            <span class="hidden sm:block font-medium">{{ user.username }}</span>
            <svg
              class="w-4 h-4 ml-1"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M19 9l-7 7-7-7"
              />
            </svg>
          </button>

          <!-- Dropdown Menu -->
          <transition
            enter-active-class="transition ease-out duration-100"
            enter-from-class="transform opacity-0 scale-95"
            enter-to-class="transform opacity-100 scale-100"
            leave-active-class="transition ease-in duration-75"
            leave-from-class="transform opacity-100 scale-100"
            leave-to-class="transform opacity-0 scale-95"
          >
            <div
              v-if="showDropdown"
              class="absolute right-0 mt-2 w-56 bg-white rounded-xl shadow-xl ring-1 ring-black/10 z-50 overflow-hidden"
            >
              <div class="px-4 py-3 border-b border-gray-200">
                <p class="text-sm text-gray-700 font-medium">{{ user.username }}</p>
                <p class="text-xs text-gray-500 truncate">{{ user.email || 'user@email.com' }}</p>
              </div>

              <button
                @click="goToProfile"
                class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-green-50 hover:text-green-700 flex items-center gap-2"
              >
                <svg
                  class="w-4 h-4 text-gray-500"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="2"
                  viewBox="0 0 24 24"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    d="M5.121 17.804A9.001 9.001 0 1112 21a8.96 8.96 0 01-6.879-3.196z"
                  />
                </svg>
                User Profile
              </button>

              <button
                @click="goToSettings"
                class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-green-50 hover:text-green-700 flex items-center gap-2"
              >
                <svg
                  class="w-4 h-4 text-gray-500"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="2"
                  viewBox="0 0 24 24"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    d="M11.049 2.927c.3-.921 1.603-.921 1.902 0l.867 2.66a1.001 1.001 0 00.95.69h2.803c.969 0 1.371 1.24.588 1.81l-2.27 1.649a1 1 0 00-.364 1.118l.867 2.66c.3.921-.755 1.688-1.538 1.118l-2.27-1.648a1 1 0 00-1.176 0l-2.27 1.648c-.783.57-1.838-.197-1.538-1.118l.867-2.66a1 1 0 00-.364-1.118L2.98 8.087c-.783-.57-.38-1.81.588-1.81H6.37a1 1 0 00.95-.69l.867-2.66z"
                  />
                </svg>
                Settings
              </button>

              <div class="border-t border-gray-200"></div>

              <button
                @click="logout"
                class="w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-red-50 flex items-center gap-2"
              >
                <svg
                  class="w-4 h-4 text-red-500"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="2"
                  viewBox="0 0 24 24"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a2 2 0 01-2 2H5a2 2 0 01-2-2V7a2 2 0 012-2h6a2 2 0 012 2v1"
                  />
                </svg>
                Log out
              </button>
            </div>
          </transition>
        </div>
      </div>
    </div>
  </nav>
</template>
