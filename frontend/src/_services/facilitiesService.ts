import { errorHandler } from '@/_helpers/errorHandler'
import type { FacilityParams } from '@/_models/params/facilityParams'
import axios from 'axios'

export const getFacilities = async (facilityParams: FacilityParams) => {
  console.log('facilityParams', facilityParams)

  return errorHandler(async () => {
    var url = `/api/facilities?pageNumber=${facilityParams.pageNumber}&pageSize=${facilityParams.pageSize}&orderBy=${facilityParams.orderBy}&sortBy=${facilityParams.sortBy}`
    if (facilityParams.userId) {
      url += `&userId=${facilityParams.userId}`
    }

    const response = await axios.get(url)
    console.log('response', response)
    const pagination = JSON.parse(response.headers['pagination'])
    return { facilities: response.data, pagination }
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
