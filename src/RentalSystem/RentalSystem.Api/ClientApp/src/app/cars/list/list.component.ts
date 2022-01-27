import { Component, OnInit } from '@angular/core';
import { Car } from "../car";
import { CarsService } from "../cars.service";
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DealersService } from "../../dealer/dealer.service";
import { Options } from "@angular-slider/ngx-slider";
import { AuthenticationService } from "../../shared/services/authentication.service";

export interface PeriodicElement {
  brand: string;
  model: string;
  horsepower: number;
  yearOfProduction: number;
  description: string;
  id: number;
}

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, AfterViewInit {

  isUserAdmin: boolean;
  cars: Car[] = [];
  displayedColumns: string[] = ['brand', 'model', 'yearOfProduction', 'basePrice', 'buttons'];
  cars2: MatTableDataSource<PeriodicElement> = new MatTableDataSource();

  constructor(public carsService: CarsService, private _liveAnnouncer: LiveAnnouncer, public dalerService: DealersService,
    public _authService: AuthenticationService) { }

  @ViewChild((MatSort) as any, { static: false }) sort: MatSort;

  ngAfterViewInit() {
    this.cars2.sort = this.sort;
  }

  ngOnInit(): void {

    this.isUserAdmin = this._authService.isUserAdmin();

    var httpGet = this.carsService.getCarsFiltered();

    httpGet.subscribe(result => {
      this.cars = result;
      this.cars2 = new MatTableDataSource(this.cars);
      this.cars2.sort = this.sort;
    },
      error => console.error(error));
  }

  deleteCar(id) {
    this.carsService.deleteCar(id).subscribe(res => {
      this.cars = this.cars.filter(car => car.id !== id);
      this.cars2 = new MatTableDataSource(this.cars);
    });
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  minYear: number = 2015;
  maxYear: number = 2019;
  minPrice: number = 500;
  maxPrice: number = 1000;
  optionsYear: Options = {
    floor: 1995,
    ceil: 2021,
    step: 1,
    showTicks: true,
  };
  optionsPrice: Options = {
    floor: 0,
    ceil: 500,
    step: 10,
    showTicks: false,
  };
  carBrands = ['BMW', 'Tesla', 'Volvo', 'Batmobile', 'Toyota', 'Renault', 'Volkswagen'];
  brands: boolean[] = [false, false, false, false, false];


  filter() {

    let str: string = "filters=YearOfProduction>=" + this.minYear + ",YearOfProduction<=" + this.maxYear
      + ",BasePrice<=" + this.maxPrice + ",BasePrice>=" + this.minPrice;
    str += ",Brand==l";

    for (var i = 0; i < this.brands.length; i++) {
      if (this.brands[i]) str += "|" + this.carBrands[i];
    }

    var httpGet = this.carsService.getCarsFiltered(str);

    httpGet.subscribe(result => {
        this.cars = result;
        this.cars2 = new MatTableDataSource(this.cars);
        this.cars2.sort = this.sort;
      },
      error => console.error(error));
  }
}
