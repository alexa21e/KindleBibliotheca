export interface Book {
    id: string
    title: string
    author: string
    publishingDate: string
    rating: number
    genre: number
    publishingHouse: string
    series?: string
    seriesPlace?: number
    pagesNumber: number
    description: string
    coverUrl: string
  }