import { Component, OnInit } from '@angular/core';
import { Reservation } from '../../user/reservation';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-admin-rental-list',
  templateUrl: './admin-rental-list.component.html',
  styleUrls: ['./admin-rental-list.component.css']
})
export class AdminRentalListComponent implements OnInit {

  rentals: Reservation[] = [];
  rentalsOld: Reservation[] = [];
  displayedColumns: string[] = ['model', 'brand', 'rentFrom', 'rentTo', 'buttons'];
  displayedColumnsOld: string[] = ['model', 'brand', 'rentFrom', 'rentTo'];

  constructor(public adminService: AdminService) { }

  ngOnInit(): void {

    this.adminService.getAllCurrentRents().subscribe(result => {
        this.rentals = result;
      },
      error => console.error(error));

    this.adminService.getAllOldRents().subscribe(result => {
      this.rentalsOld = result;
    },
      error => console.error(error));
  }
}
