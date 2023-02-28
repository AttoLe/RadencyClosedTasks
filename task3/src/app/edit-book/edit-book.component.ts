import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {BookService} from "../services/book.service";
import {Book} from "../interfaces/interface";

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent implements OnInit{

  public IsAdding: boolean = true;
  public editedBook?: Book;
  constructor(private activatedR: ActivatedRoute, private bookService: BookService, private router: Router) {
  }

  ngOnInit(): void {
    this.activatedR.params.subscribe(x => this.bookIdChange(x['id']));
  }

  OnAddBook(){
    if(this.editedBook){
      this.bookService.saveBook(this.editedBook).subscribe();
      this.router.navigate(['/']);
    }
  }

  onFilePathChange(Event: any){
    const reader = new FileReader();
    reader.readAsDataURL(Event.target.file[0]);
    reader.onload = () => {
      this.editedBook!.cover = reader.result!.toString();
    }
  }

  bookIdChange(bookId: number | null){
    if(bookId){
      this.bookService.getBookById(bookId).subscribe(b => this.editedBook = b);
      this.IsAdding = false;
    }
    else{
      this.editedBook = {} as Book;
      this.IsAdding = true;
    }
  }
}
