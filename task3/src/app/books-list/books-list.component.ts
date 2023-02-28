import {Component, OnInit} from '@angular/core';
import {BookService} from "../services/book.service";
import {Observable} from "rxjs";
import {BookListItem} from "../interfaces/interface";

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit{

  books!:Observable<BookListItem[]>
  isAllBook!: boolean;

  constructor(private bookService: BookService) {}

  ngOnInit(){
    this.onAllTabClick();
    this.bookService.BookSavedEvent.subscribe(() =>{
      if(this.isAllBook)
        this.onAllTabClick();
      else
        this.onRecommendClick();
    })
  }

  onAllTabClick(){
    this.books = this.bookService.getAllBooks();
    this.isAllBook = true;
  }

  onRecommendClick(){
    this.books = this.bookService.getRecommendedBooks();
    this.isAllBook = false;
  }
}
