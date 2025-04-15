<template>
  <div class="mb-4">
    <label :for="label" class="block mb-2 text-sm font-semibold text-gray-700">
      {{ label }}
    </label>
    <input
      :id="label"
      :type="type"
      :placeholder="placeholder"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
      class="w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500 focus:border-green-500 transition"
      :class="{ 'border-red-500': error }"
    />
    <p v-if="error" class="mt-1 text-sm text-red-600">{{ error }}</p>
  </div>
</template>

<script setup>
defineProps({
  label: String,
  type: {
    type: String,
    default: 'text',
  },
  placeholder: String,
  modelValue: [String, Number],
  error: String,
})

defineEmits(['update:modelValue'])
</script>
