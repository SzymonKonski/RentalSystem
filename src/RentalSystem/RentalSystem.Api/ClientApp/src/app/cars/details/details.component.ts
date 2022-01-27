import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from "../car";
import { CarsService } from "../cars.service";


@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  id: number;
  car: Car;

  constructor(
    public carsService: CarsService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['carId'];
    this.carsService.getCar(this.id).subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }
}
