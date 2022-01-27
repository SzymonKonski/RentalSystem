import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRentalListComponent } from './admin-rental-list/admin-rental-list.component';
import { AdminRoutingModule } from "./admin-routing.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { ReturnComponent } from "./return/return.component";

@NgModule({
  declarations: [AdminRentalListComponent, ReturnComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatListModule
  ]
})
export class AdminModule { }
