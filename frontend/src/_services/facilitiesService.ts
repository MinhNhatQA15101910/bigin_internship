import { errorHandler } from '@/_helpers/errorHandler'
import axios from 'axios'

export const getFacilities = async () => {
  return errorHandler(async () => {
    const response = await axios.get('/api/facilities')
    return response.data
  })
}
