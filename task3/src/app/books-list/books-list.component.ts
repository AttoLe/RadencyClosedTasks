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
  constructor(private bookService: BookService) {}

  ngOnInit(){
    this.onAllTabClick();
  }

  onAllTabClick(){
    this.books = this.bookService.getAllBooks();
    console.log(this.books);
  }

  onRecommendClick(){
    this.books = this.bookService.getRecommendedBooks();
  }
}
