import {Component, Input} from '@angular/core';
import {BookListItem} from "../interfaces/interface";
import {BookService} from "../services/book.service";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ViewBookComponent} from "../view-book/view-book.component";
import {EditBookComponent} from "../edit-book/edit-book.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-books-list-item',
  templateUrl: './books-list-item.component.html',
  styleUrls: ['./books-list-item.component.css']
})
export class BooksListItemComponent{
  @Input()book?:BookListItem;
  constructor(private bookService: BookService, private modal: NgbModal) {}

  OnViewClick(){
    const modal = this.modal.open(ViewBookComponent);
    modal.componentInstance.id = this.book?.id;
  }
}
