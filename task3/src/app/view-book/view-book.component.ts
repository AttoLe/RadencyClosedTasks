import {Component, OnInit} from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {BookService} from "../services/book.service";
import {BookDetailed} from "../interfaces/interface";
import {Observable} from "rxjs";

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.css']
})

export class ViewBookComponent implements OnInit{
  constructor(public modal: NgbActiveModal, private bookService: BookService) {}

  id!:number
  book$!:Observable<BookDetailed>
  ngOnInit(): void {
    this.book$ = this.bookService.getBookById(this.id);
    console.log(this.id +"\t"+this.book$)
  }
}
