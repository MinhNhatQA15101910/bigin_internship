import type { User } from '@/_models/user'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const user = ref<User | null>(null)

  // Load from localStorage on init
  const loadUser = () => {
    const userData = localStorage.getItem('user')
    if (userData) {
      user.value = JSON.parse(userData)
    }
  }

  const setUser = (data: User) => {
    user.value = data
    localStorage.setItem('user', JSON.stringify(data))
  }

  const logout = () => {
    user.value = null
    localStorage.removeItem('user')
  }

  const isLoggedIn = computed(() => !!user.value)

  return {
    user,
    setUser,
    logout,
    loadUser,
    isLoggedIn,
  }
})
