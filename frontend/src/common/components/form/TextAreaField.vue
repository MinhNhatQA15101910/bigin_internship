<template>
  <div class="mb-4">
    <label :for="label" class="block mb-2 text-sm font-semibold text-gray-700">
      {{ label }}
    </label>
    <textarea
      :id="label"
      :placeholder="placeholder"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
      rows="4"
      class="w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500 focus:border-green-500 transition"
      :class="{ 'border-red-500': error }"
    ></textarea>
    <p v-if="error" class="mt-1 text-sm text-red-600">{{ error }}</p>
  </div>
</template>

<script setup>
defineProps({
  label: String,
  placeholder: String,
  modelValue: String,
  error: String,
})

defineEmits(['update:modelValue'])
</script>
