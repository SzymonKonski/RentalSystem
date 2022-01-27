import { Component, OnInit } from '@angular/core';
import { UserService } from "../user.service";
import { Reservation } from "../reservation";


@Component({
  selector: 'app-rental-list',
  templateUrl: './rental-list.component.html',
  styleUrls: ['./rental-list.component.css']
})
export class RentalListComponent implements OnInit {

  rentals: Reservation[] = [];
  rentalsOld: Reservation[] = [];
  displayedColumns: string[] = ['model', 'brand', 'rentFrom', 'rentTo', 'buttons'];
  displayedColumnsOld: string[] = ['model', 'brand', 'rentFrom', 'rentTo'];

  constructor(public userService: UserService) { }

  ngOnInit(): void {

    this.userService.getCurrentRents().subscribe(result => {
        this.rentals = result;
      },
      error => console.error(error));

    this.userService.getOldRents().subscribe(result => {
      this.rentalsOld = result;
    },
      error => console.error(error));
  }
}
