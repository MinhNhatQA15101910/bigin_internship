import { errorHandler } from '@/_helpers/errorHandler'
import { useAuthStore } from '@/stores/authStore'
import axios from 'axios'

export const getFacilities = async () => {
  return errorHandler(async () => {
    const response = await axios.get('/api/facilities')
    return response.data
  })
}

export const registerFacility = async (facility: FormData, token: string) => {
  return errorHandler(async () => {
    const response = await axios.post('/api/facilities', facility, {
      headers: {
        'Content-Type': 'multipart/form-data',
        Authorization: `Bearer ${token}`,
      },
    })
    return response.data
  })
}
