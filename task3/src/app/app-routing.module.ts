import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EditBookComponent} from "./edit-book/edit-book.component";

const routes: Routes = [
  { path: 'editing/:id', component: EditBookComponent},
  { path: '', component: EditBookComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
