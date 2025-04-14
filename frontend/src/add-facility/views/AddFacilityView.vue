<script setup>
import { reactive } from 'vue'
import InputField from '@/components/form/InputField.vue'
import TextAreaField from '@/components/form/TextAreaField.vue'
import ImageUploader from '@/components/form/ImageUploader.vue'

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
  <div class="p-6 max-w-4xl mx-auto">
    <h2 class="text-2xl font-bold mb-4">Thêm Sân Cầu Lông Mới</h2>
    <form @submit.prevent="handleSubmit">
      <!-- Facility Info -->
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <InputField v-model="facility.facilityName" label="Tên sân" />
        <InputField v-model="facility.facebookUrl" label="Link Facebook" />
        <InputField v-model="facility.detailAddress" label="Địa chỉ chi tiết" />
        <InputField v-model="facility.province" label="Tỉnh/Thành phố" />
        <InputField v-model.number="facility.courtsAmount" label="Số lượng sân" type="number" />
        <InputField v-model.number="facility.minPrice" label="Giá thấp nhất" type="number" />
        <InputField v-model.number="facility.maxPrice" label="Giá cao nhất" type="number" />
      </div>

      <TextAreaField v-model="facility.description" label="Mô tả chi tiết" />
      <TextAreaField v-model="facility.policy" label="Chính sách sân" />

      <!-- Location -->
      <div class="grid grid-cols-2 gap-4">
        <InputField
          v-model.number="facility.location.coordinates[0]"
          label="Kinh độ (Longitude)"
          type="number"
        />
        <InputField
          v-model.number="facility.location.coordinates[1]"
          label="Vĩ độ (Latitude)"
          type="number"
        />
      </div>

      <!-- Photos Upload -->
      <ImageUploader label="Ảnh sân" :images="facility.photos" @update="facility.photos = $event" />

      <!-- Manager Info -->
      <h3 class="text-xl font-semibold mt-6 mb-2">Thông tin người quản lý</h3>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <InputField v-model="facility.managerInfo.fullName" label="Họ và tên" />
        <InputField v-model="facility.managerInfo.email" label="Email" />
        <InputField v-model="facility.managerInfo.phoneNumber" label="Số điện thoại" />
        <InputField v-model="facility.managerInfo.citizenId" label="Số CCCD" />
      </div>

      <!-- Citizen ID images -->
      <ImageUploader
        label="Ảnh CCCD mặt trước"
        :images="[facility.managerInfo.citizenImageFront]"
        single
        @update="facility.managerInfo.citizenImageFront = $event[0]"
      />
      <ImageUploader
        label="Ảnh CCCD mặt sau"
        :images="[facility.managerInfo.citizenImageBack]"
        single
        @update="facility.managerInfo.citizenImageBack = $event[0]"
      />

      <!-- Bank Card -->
      <ImageUploader
        label="Thẻ ngân hàng mặt trước"
        :images="[facility.managerInfo.bankCardFront]"
        single
        @update="facility.managerInfo.bankCardFront = $event[0]"
      />
      <ImageUploader
        label="Thẻ ngân hàng mặt sau"
        :images="[facility.managerInfo.bankCardBack]"
        single
        @update="facility.managerInfo.bankCardBack = $event[0]"
      />

      <!-- Business Licenses -->
      <ImageUploader
        label="Ảnh giấy phép kinh doanh"
        :images="facility.managerInfo.businessLicenseImages"
        @update="facility.managerInfo.businessLicenseImages = $event"
      />

      <!-- Submit -->
      <div class="mt-6">
        <button class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">Lưu</button>
      </div>
    </form>
  </div>
</template>
