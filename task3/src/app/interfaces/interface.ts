export interface BookListItem{
  id: number,
  title: string,
  reviewsNumber: number,
  cover: string,
  avgRating: number
}

export interface BookDetailed{
  id: number,
  title: string,
  author:string,
  cover: string,
  content: string,
  genre: string,
  avgRating: number,
  reviews: Review[],
}

export interface Review{
  id: number,
  message: string,
  reviewer: string
}

export interface Book{
  id: number,
  title: string,
  author:string,
  cover: string,
  content: string,
  genre: string,
}
