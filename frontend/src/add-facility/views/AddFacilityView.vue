<script setup>
import { reactive } from 'vue'
import InputField from '@/common/components/form/InputField.vue'
import TextAreaField from '@/common/components/form/TextAreaField.vue'
import ImageUploader from '@/common/components/form/ImageUploader.vue'

const facility = reactive({
  facilityName: '',
  description: '',
  facebookUrl: '',
  policy: '',
  courtsAmount: 0,
  minPrice: 0,
  maxPrice: 0,
  detailAddress: '',
  province: '',
  location: {
    type: 'Point',
    coordinates: [0, 0],
  },
  photos: [],
  managerInfo: {
    fullName: '',
    email: '',
    phoneNumber: '',
    citizenId: '',
    citizenImageFront: { id: null, url: '', isMain: true },
    citizenImageBack: { id: null, url: '', isMain: true },
    bankCardFront: { id: null, url: '', isMain: true },
    bankCardBack: { id: null, url: '', isMain: true },
    businessLicenseImages: [],
  },
})

const handleSubmit = () => {
  // Call API here or emit event
  console.log('Submitting facility:', JSON.stringify(facility, null, 2))
}
</script>

<template>
  <section class="bg-green-50 min-h-screen py-10">
    <div class="container mx-auto px-4">
      <div class="max-w-3xl mx-auto bg-white rounded-lg shadow-lg p-8">
        <h1 class="text-3xl font-bold text-green-700 mb-6">Add New Facility</h1>

        <!-- Your form starts here -->
        <form @submit.prevent="handleSubmit">
          <!-- Facility Info -->
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <InputField v-model="facility.facilityName" label="Facility name" />
            <InputField v-model="facility.facebookUrl" label="Facebook Link" />
            <InputField v-model="facility.detailAddress" label="Detail Address" />
            <InputField v-model="facility.province" label="Province" />
            <InputField
              v-model.number="facility.courtsAmount"
              label="Courts amount"
              type="number"
            />
            <InputField v-model.number="facility.minPrice" label="Min price" type="number" />
            <InputField v-model.number="facility.maxPrice" label="Max price" type="number" />
          </div>

          <TextAreaField v-model="facility.description" label="Description" />
          <TextAreaField v-model="facility.policy" label="Policy" />

          <!-- Location -->
          <div class="grid grid-cols-2 gap-4">
            <InputField
              v-model.number="facility.location.coordinates[0]"
              label="Longitude"
              type="number"
            />
            <InputField
              v-model.number="facility.location.coordinates[1]"
              label="Latitude"
              type="number"
            />
          </div>

          <!-- Photos Upload -->
          <ImageUploader
            label="Facility Images"
            :images="facility.photos"
            @update="facility.photos = $event"
          />

          <!-- Manager Info -->
          <h3 class="text-xl font-semibold mt-6 mb-2">Manager Information</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <InputField v-model="facility.managerInfo.fullName" label="Full name" />
            <InputField v-model="facility.managerInfo.email" label="Email" />
            <InputField v-model="facility.managerInfo.phoneNumber" label="Phone number" />
            <InputField v-model="facility.managerInfo.citizenId" label="Citizen ID" />
          </div>

          <!-- Citizen ID images -->
          <ImageUploader
            label="Citizen Image Front"
            :images="[facility.managerInfo.citizenImageFront]"
            single
            @update="facility.managerInfo.citizenImageFront = $event[0]"
          />
          <ImageUploader
            label="Citizen Image Back"
            :images="[facility.managerInfo.citizenImageBack]"
            single
            @update="facility.managerInfo.citizenImageBack = $event[0]"
          />

          <!-- Bank Card -->
          <ImageUploader
            label="Bank Card Front"
            :images="[facility.managerInfo.bankCardFront]"
            single
            @update="facility.managerInfo.bankCardFront = $event[0]"
          />
          <ImageUploader
            label="Bank Card Back"
            :images="[facility.managerInfo.bankCardBack]"
            single
            @update="facility.managerInfo.bankCardBack = $event[0]"
          />

          <!-- Business Licenses -->
          <ImageUploader
            label="Business License Images"
            :images="facility.managerInfo.businessLicenseImages"
            @update="facility.managerInfo.businessLicenseImages = $event"
          />

          <!-- Submit -->
          <div class="mt-6">
            <button class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600">Add</button>
          </div>
        </form>
        <!-- Your form ends here -->
      </div>
    </div>
  </section>
</template>
