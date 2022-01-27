import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RentalListComponent } from "./rental-list/rental-list.component";
import { ReturnComponent } from "./return/return.component";

const routes: Routes = [
  { path: 'account', component: RentalListComponent },
  { path: ':rentId/return', component: ReturnComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
