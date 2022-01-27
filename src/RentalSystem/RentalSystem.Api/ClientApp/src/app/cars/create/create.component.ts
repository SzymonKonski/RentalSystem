import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CarsService } from "../cars.service";
import { Dealer } from "../../dealer/dealer";
import { DealersService } from "../../dealer/dealer.service";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  createForm;
  dealers: Dealer[];
  selectedDealer: Dealer;

  constructor(
    public carsService: CarsService,
    public dealersService: DealersService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder) {

      this.createForm = this.formBuilder.group({
        brand: ['', Validators.required],
        model: ['', Validators.required],
        horsepower: ['', Validators.required],
        yearOfProduction: ['', Validators.required],
        description: ['', Validators.required],
        basePrice: ['', Validators.required],
        dealerId: null
      });

  }

  ngOnInit(): void {
    var httpGet = this.dealersService.getDealers();

    httpGet.subscribe(result => {
      this.dealers = result;
      this.selectedDealer = this.dealers[0];
    },
      error => console.error(error));
  }

  onSubmit(formData) {

    formData.value.dealerId = this.selectedDealer.id;

    this.carsService.createCar(formData.value).subscribe(res => {
      this.router.navigateByUrl('/cars');
    });
  }

}
