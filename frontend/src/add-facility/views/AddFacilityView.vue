<script setup>
import { reactive, computed, ref } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required, minLength, email } from '@vuelidate/validators'

import InputField from '@/common/components/form/InputField.vue'
import TextAreaField from '@/common/components/form/TextAreaField.vue'
import ImageUploader from '@/common/components/form/ImageUploader.vue'
import { registerFacility } from '@/_services/facilitiesService'
import { useAuthStore } from '@/stores/authStore'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import { useToast } from 'vue-toastification'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()
const toast = useToast()

// State
const isLoading = ref(false)

const facility = reactive({
  facilityName: '',
  description: '',
  facebookUrl: '',
  policy: '',
  detailAddress: '',
  province: '',
  lon: 0,
  lat: 0,
  facilityImages: [],
  fullName: '',
  email: '',
  phoneNumber: '',
  citizenId: '',
  citizenImageFront: null,
  citizenImageBack: null,
  bankCardFront: null,
  bankCardBack: null,
  businessLicenseImages: [],
})

// Vuelidate Rules
const rules = computed(() => ({
  facilityName: { required },
  description: { required, minLength: minLength(10) },
  facebookUrl: {},
  policy: { required },
  detailAddress: { required },
  province: { required },
  lon: { required },
  lat: { required },
  fullName: { required },
  email: { required, email },
  phoneNumber: { required },
  citizenId: { required },
}))

const v$ = useVuelidate(rules, facility)

const handleSubmit = async () => {
  v$.value.$touch()

  if (!v$.value.$invalid) {
    isLoading.value = true
    
    const formData = getFormData(facility)

    var newFacility = await registerFacility(formData, authStore.user.token)

    isLoading.value = false

    router.push(`/facilities/${newFacility.id}`)
    toast.success('Facility added successfully!')
  } else {
    console.error('Form validation failed:', v$.value.$errors)
  }
}

const getFormData = (facility) => {
  const formData = new FormData()
  formData.append('facilityName', facility.facilityName)
  formData.append('description', facility.description)
  formData.append('facebookUrl', facility.facebookUrl)
  formData.append('policy', facility.policy)
  formData.append('detailAddress', facility.detailAddress)
  formData.append('province', facility.province)
  formData.append('lon', facility.lon)
  formData.append('lat', facility.lat)
  formData.append('fullName', facility.fullName)
  formData.append('email', facility.email)
  formData.append('phoneNumber', facility.phoneNumber)
  formData.append('citizenId', facility.citizenId)
  formData.append('citizenImageFront', facility.citizenImageFront)
  formData.append('citizenImageBack', facility.citizenImageBack)
  formData.append('bankCardFront', facility.bankCardFront)
  formData.append('bankCardBack', facility.bankCardBack)

  facility.facilityImages.forEach((file) => {
    formData.append('facilityImages', file)
  })

  facility.businessLicenseImages.forEach((file) => {
    formData.append('businessLicenseImages', file)
  })

  return formData
}
</script>

<template>
  <div
    v-if="isLoading"
    class="fixed inset-0 z-50 bg-white bg-opacity-70 flex items-center justify-center"
  >
    <PulseLoader />
  </div>

  <section class="bg-green-50 min-h-screen py-10">
    <div class="container mx-auto px-4">
      <div class="max-w-3xl mx-auto bg-white rounded-lg shadow-lg p-8">
        <h1 class="text-3xl font-bold text-green-700 mb-6">Add New Facility</h1>

        <form @submit.prevent="handleSubmit">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <InputField
              v-model="facility.facilityName"
              label="Facility name"
              :error="
                v$.facilityName.$dirty && v$.facilityName.$error ? 'Facility name is required' : ''
              "
            />
            <InputField v-model="facility.facebookUrl" label="Facebook Link" />
            <InputField
              v-model="facility.detailAddress"
              label="Detail Address"
              :error="
                v$.detailAddress.$dirty && v$.detailAddress.$error
                  ? 'Detail address is required'
                  : ''
              "
            />
            <InputField
              v-model="facility.province"
              label="Province"
              :error="v$.province.$dirty && v$.province.$error ? 'Province is required' : ''"
            />
          </div>

          <TextAreaField
            v-model="facility.description"
            label="Description"
            :error="
              v$.description.$dirty && v$.description.$error
                ? 'Description must be at least 10 characters'
                : ''
            "
          />

          <TextAreaField
            v-model="facility.policy"
            label="Policy"
            :error="v$.policy.$dirty && v$.policy.$error ? 'Policy is required' : ''"
          />

          <div class="grid grid-cols-2 gap-4">
            <InputField
              v-model.number="facility.lon"
              label="Longitude"
              type="number"
              :error="v$.lon.$dirty && v$.lon.$error ? 'Longitude is required' : ''"
            />
            <InputField
              v-model.number="facility.lat"
              label="Latitude"
              type="number"
              :error="v$.lat.$dirty && v$.lat.$error ? 'Latitude is required' : ''"
            />
          </div>

          <ImageUploader
            label="Facility Images"
            :images="facility.facilityImages"
            @update="facility.facilityImages = $event"
          />

          <h3 class="text-xl font-semibold mt-6 mb-2">Manager Information</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <InputField
              v-model="facility.fullName"
              label="Full name"
              :error="v$.fullName.$dirty && v$.fullName.$error ? 'Full name is required' : ''"
            />
            <InputField
              v-model="facility.email"
              label="Email"
              :error="v$.email.$dirty && v$.email.$error ? 'Valid email is required' : ''"
            />
            <InputField
              v-model="facility.phoneNumber"
              label="Phone number"
              :error="
                v$.phoneNumber.$dirty && v$.phoneNumber.$error ? 'Phone number is required' : ''
              "
            />
            <InputField
              v-model="facility.citizenId"
              label="Citizen ID"
              :error="v$.citizenId.$dirty && v$.citizenId.$error ? 'Citizen ID is required' : ''"
            />
          </div>

          <ImageUploader
            label="Citizen Image Front"
            :images="[facility.citizenImageFront]"
            single
            @update="facility.citizenImageFront = $event[0]"
          />
          <ImageUploader
            label="Citizen Image Back"
            :images="[facility.citizenImageBack]"
            single
            @update="facility.citizenImageBack = $event[0]"
          />

          <ImageUploader
            label="Bank Card Front"
            :images="[facility.bankCardFront]"
            single
            @update="facility.bankCardFront = $event[0]"
          />
          <ImageUploader
            label="Bank Card Back"
            :images="[facility.bankCardBack]"
            single
            @update="facility.bankCardBack = $event[0]"
          />

          <ImageUploader
            label="Business License Images"
            :images="facility.businessLicenseImages"
            @update="facility.businessLicenseImages = $event"
          />

          <div class="mt-6">
            <button class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600">
              Add
            </button>
          </div>
        </form>
      </div>
    </div>
  </section>
</template>
