import { errorHandler } from '@/_helpers/errorHandler'
import type { User } from '@/_models/user'
import axios from 'axios'
import { useToast } from 'vue-toastification'

const toast = useToast()

export const login = async (payload: any) => {
  return errorHandler(async () => {
    const response = await axios.post<User>('/api/auth/login', payload)

    const user = response.data
    localStorage.setItem('user', JSON.stringify(user))
    toast.success('Login successfully')

    return user
  })
}
