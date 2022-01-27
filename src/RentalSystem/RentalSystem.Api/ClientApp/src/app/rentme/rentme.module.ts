import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentformComponent } from './rentform/rentform.component';
import { MatNativeDateModule, MatFormFieldModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { RentmeRoutingModule } from "./rentme-routing.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [RentformComponent],
  imports: [
    CommonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    RentmeRoutingModule
  ]
})
export class RentmeModule { }
