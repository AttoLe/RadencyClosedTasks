import { Component } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../services/book.service";
import {BookDetailed, Book} from "../interfaces/interface";

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent {

  public IsAdding: boolean = true;
  public editedBook?: Book;
  constructor(private activatedR: ActivatedRoute, private bookService: BookService) {
    const id = activatedR.snapshot.params['id']
    if (id)
      this.bookService.getBookById(id).subscribe(b => this.editedBook = b);
  }

  OnAddBook(){
    if(this.editedBook)
      this.bookService.saveBook(this.editedBook);
  }
}
