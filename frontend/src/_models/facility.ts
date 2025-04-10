import type { Location } from './location'
import type { Photo } from './photo'

export interface Facility {
  id: string
  userId: string
  facilityName: string
  description: string
  facebookUrl: string
  policy: string
  courtsAmount: number
  minPrice: number
  maxPrice: number
  detailAddress: string
  province: string
  location: Location
  ratingAvg: number
  totalRatings: number
  registeredAt: Date
  photos: Photo[]
}
