import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Book, BookDetailed, BookListItem} from "../interfaces/interface";
import {environment} from "../../environments/environment";
import {Subject, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  BookSavedEvent = new Subject();

  constructor(private http: HttpClient) { }

  getAllBooks(){
    return this.http.get<BookListItem[]>(environment.url + 'books?order=title')
  }

  getRecommendedBooks(){
    return this.http.get<BookListItem[]>(environment.url + 'recommended')
  }

  getBookById(id:number){
    return this.http.get<BookDetailed>(environment.url + `books/${id}`)
  }

  saveBook(book:Book){
    return this.http.post<Book>(environment.url + "books/save", book)
      .pipe(tap(() => {this.BookSavedEvent.next({})}));
  }
}
