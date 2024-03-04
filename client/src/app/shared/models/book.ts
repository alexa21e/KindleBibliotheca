import { Genre } from "./genre"

export interface Book {
    id: string
    title: string
    authorId: string
    author: string
    publishingDate: string
    rating: number
    genre: Genre
    publishingHouse: string
    seriesId?:string
    series?: string
    seriesPlace?: number
    pagesNumber: number
    description: string
    coverUrl: string
    pdfUrl: string
  }