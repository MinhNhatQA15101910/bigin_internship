<script setup>
import { ref, computed } from 'vue'
import 'vue3-carousel/carousel.css'
import { Carousel, Slide } from 'vue3-carousel'

const props = defineProps({
  facility: {
    type: Object,
    required: true,
  },
})

const showFullDescription = ref(false)
const defaultImage =
  'https://blog.khelomore.com/wp-content/uploads/2022/02/MC44MjUxMzYwMCAxNDY4MjI1Njg3.jpeg'

const toggleFullDescription = () => {
  showFullDescription.value = !showFullDescription.value
}

const truncatedDescription = computed(() => {
  let description = props.facility.description
  if (!showFullDescription.value && description.length > 120) {
    description = description.substring(0, 120) + '...'
  }
  return description
})

const handleImageError = (e) => {
  e.target.src = defaultImage
}
</script>

<template>
  <div class="bg-white rounded-xl shadow-md overflow-hidden">
    <!-- Carousel -->
    <Carousel :items-to-show="1" :wrap-around="true" :autoplay="false" class="w-full h-64">
      <Slide v-for="(photo, index) in facility.photos" :key="photo.id || index">
        <img
          :src="photo.url"
          @error="handleImageError"
          alt="Facility Photo"
          class="object-cover w-full h-64"
        />
      </Slide>
    </Carousel>

    <!-- Info -->
    <div class="p-4">
      <div class="mb-4">
        <div class="text-gray-600 text-sm mb-1">Status: {{ facility.state }}</div>
        <h2 class="text-xl font-bold">{{ facility.facilityName }}</h2>
      </div>

      <div class="mb-4">
        <p>{{ truncatedDescription }}</p>
        <button
          @click="toggleFullDescription"
          class="text-green-500 hover:text-green-600 mt-1 text-sm"
        >
          {{ showFullDescription ? 'See less' : 'See more' }}
        </button>
      </div>

      <div class="text-green-600 font-semibold mb-2">
        Price:
        {{
          facility.minPrice && facility.maxPrice
            ? `${facility.minPrice} - ${facility.maxPrice} VND/h`
            : 'Not available'
        }}
      </div>

      <div class="border border-gray-100 mb-4"></div>

      <div class="flex flex-col lg:flex-row justify-between text-sm text-gray-600">
        <div class="flex items-center gap-1 text-orange-700">
          <i class="pi pi-map-marker"></i>
          <span>{{ facility.province }}</span>
        </div>

        <RouterLink
          :to="`/facilities/${facility.id}`"
          class="h-[36px] bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-lg text-center mt-3 lg:mt-0"
        >
          Read more
        </RouterLink>
      </div>
    </div>
  </div>
</template>
