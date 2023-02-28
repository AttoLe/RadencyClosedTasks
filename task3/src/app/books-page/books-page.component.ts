import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {BookService} from "../services/book.service";

@Component({
  selector: 'app-books-page',
  templateUrl: './books-page.component.html',
  styleUrls: ['./books-page.component.css']
})
export class BooksPageComponent{
  constructor(private bookService: BookService) {}
}

