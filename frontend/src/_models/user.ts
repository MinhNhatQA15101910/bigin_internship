import type { Photo } from './photo'

export interface User {
  id: string
  username: string
  email: string
  token: string
  photoUrl?: string
  photos: Photo[]
  roles: string[]
}
