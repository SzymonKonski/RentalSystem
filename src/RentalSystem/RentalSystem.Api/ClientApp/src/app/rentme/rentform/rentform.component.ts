import { Component, OnInit } from '@angular/core';
import { MatDatepicker, MatDatepickerInputEvent } from '@angular/material/datepicker';
import { CarsService } from "../../cars/cars.service";
import { Car } from "../../cars/car";
import { ActivatedRoute, Router } from '@angular/router';
import { RentmeService } from "../rentme.service";

@Component({
  selector: 'rentform',
  templateUrl: './rentform.component.html',
  styleUrls: ['./rentform.component.css']
})
export class RentformComponent implements OnInit {

  pick1: Date = new Date();
  pick2: Date = new Date();
  diffDays: number;
  basePrice: number = 150;
  picker: MatDatepicker<Date>;
  carId: number = 1;
  car: Car;
  events: string[] = [];

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    if (this.pick2 < this.pick1) this.pick2 = this.pick1;
  }

  constructor(public carsService: CarsService, private route: ActivatedRoute, private router: Router, public rentService: RentmeService) {}

  ngOnInit() {
    this.carId = this.route.snapshot.params['carId'];
    var httpGet = this.carsService.getCar(this.carId);

    httpGet.subscribe(result => {
      this.car = result;
    },
      error => console.error(error));
  }

  myFilter1 = (d: Date | null): boolean => {
    const day = (d || new Date()); 
    const today = new Date();
    return day > today;
  };

  myFilter2 = (d: Date | null): boolean => {
    const day = (d || new Date());
    const diffTime = Math.abs(day.getTime() - this.pick1.getTime());
    this.diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return day > this.pick1 && this.diffDays < 14;
    
  };

  rentCar() {

    var rent = {
      carId: this.carId,
      rentFrom: this.pick1,
      rentTo: this.pick2
    };

    this.rentService.createRent(rent).subscribe(res => {
      this.router.navigateByUrl('/cars');
    });
  }

}
