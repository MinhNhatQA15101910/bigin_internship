import { errorHandler } from '@/_helpers/errorHandler'
import type { User } from '@/_models/user'
import axios from 'axios'

export const login = async (payload: any): Promise<User> => {
  return errorHandler(async () => {
    const response = await axios.post<User>('/api/auth/login', payload)
    return response.data
  })
}

export const getCurrentUser = async (token: string): Promise<User> => {
  return errorHandler(async () => {
    const response = await axios.get<User>('/api/auth/me', {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    return response.data
  })
}
