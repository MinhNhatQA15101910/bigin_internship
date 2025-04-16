import type { Error } from '@/_models/error'
import { useToast } from 'vue-toastification'

const toast = useToast()

export const errorHandler = async (method: Function): Promise<any> => {
  try {
    return await method()
  } catch (err: any) {
    console.error('Error:', err)
    const errorResponse: Error = err.response.data
    const statusCode = errorResponse.status
    switch (statusCode) {
      case 400:
        toast.warning(errorResponse.detail)
        break
      case 422:
        if (errorResponse.errors) {
          let errorMessage = ''
          for (const error of Object.values(errorResponse.errors)) {
            errorMessage += `${error}\n`
          }

          toast.warning(errorMessage)
        } else {
          toast.warning(errorResponse.detail)
        }
        break
      case 401:
      case 500:
        toast.error(errorResponse.detail)
        break
    }
  }
}
