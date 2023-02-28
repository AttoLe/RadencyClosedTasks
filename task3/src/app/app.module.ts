import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EditBookComponent } from './edit-book/edit-book.component';
import { ViewBookComponent } from './view-book/view-book.component';
import { BooksPageComponent } from './books-page/books-page.component';
import { BooksListItemComponent } from './books-list-item/books-list-item.component';
import {HttpClientModule} from '@angular/common/http';
import { BooksListComponent } from './books-list/books-list.component';



@NgModule({
  declarations: [
    AppComponent,
    EditBookComponent,
    ViewBookComponent,
    BooksPageComponent,
    BooksListItemComponent,
    BooksListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
