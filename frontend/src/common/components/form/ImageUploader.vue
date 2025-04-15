<template>
  <div class="mb-4">
    <label class="block text-sm font-semibold text-gray-700 mb-2">{{ label }}</label>
    <input
      type="file"
      :multiple="!single"
      @change="handleUpload"
      class="block w-full text-sm text-gray-500 file:mr-4 file:py-2 file:px-4 file:rounded-lg file:border-0 file:text-sm file:font-semibold file:bg-green-50 file:text-green-700 hover:file:bg-green-100"
    />

    <div v-if="previewUrls.length" class="grid grid-cols-2 md:grid-cols-4 gap-2 mt-4">
      <img
        v-for="(url, idx) in previewUrls"
        :key="idx"
        :src="url"
        alt="Preview"
        class="w-full h-32 object-cover rounded shadow"
      />
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const props = defineProps({
  label: String,
  single: Boolean,
  images: Array,
})
const emit = defineEmits(['update'])

const previewUrls = ref([])

const handleUpload = (e) => {
  const files = Array.from(e.target.files)
  previewUrls.value = files.map((file) => URL.createObjectURL(file))
  emit('update', props.single ? [files[0]] : files)
}
</script>
