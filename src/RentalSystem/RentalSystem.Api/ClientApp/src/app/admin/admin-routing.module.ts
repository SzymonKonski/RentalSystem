import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminRentalListComponent } from './admin-rental-list/admin-rental-list.component';
import { ReturnComponent } from "./return/return.component";

const routes: Routes = [
  { path: '', component: AdminRentalListComponent, pathMatch: 'full' },
  { path: 'rentals', component: AdminRentalListComponent },
  { path: ':rentId/return', component: ReturnComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
