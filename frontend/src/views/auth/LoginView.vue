<script setup>
import { reactive } from 'vue'
import { login } from '@/_services/authService'

const form = reactive({
  email: '',
  password: '',
})

const handleSubmit = async () => {
  const loginDto = {
    ...form,
  }

  const result = await login(loginDto)
  console.log(result)
}
</script>

<template>
  <section class="bg-green-50 min-h-screen flex items-center justify-center">
    <div class="bg-white px-6 py-8 shadow-md rounded-md border w-full max-w-2xl">
      <form @submit.prevent="handleSubmit">
        <h2 class="text-3xl text-center font-semibold mb-6">Login</h2>

        <div class="mb-4">
          <label class="block text-gray-700 font-bold mb-2">Email</label>
          <input
            type="email"
            v-model="form.email"
            id="email"
            name="email"
            class="border rounded w-full py-2 px-3 mb-2"
            placeholder="eg. example@gmail.com"
            required
          />
        </div>

        <div class="mb-4">
          <label class="block text-gray-700 font-bold mb-2">Password</label>
          <input
            type="password"
            v-model="form.password"
            id="password"
            name="password"
            class="border rounded w-full py-2 px-3 mb-2"
            placeholder="Password"
            required
          />
        </div>

        <div>
          <button
            class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline"
            type="submit"
          >
            Login
          </button>
        </div>
      </form>
    </div>
  </section>
</template>
