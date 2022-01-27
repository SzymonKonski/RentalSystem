import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RentformComponent } from "./rentform/rentform.component";

const routes: Routes = [
  { path: ':carId/rentform', component: RentformComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RentmeRoutingModule { }
