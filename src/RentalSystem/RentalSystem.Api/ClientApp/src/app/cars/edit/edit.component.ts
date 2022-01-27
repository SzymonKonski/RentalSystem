import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Car } from "../car";
import { CarsService } from "../cars.service";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  car: Car;
  editForm;

  constructor(
    public carsService: CarsService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
      this.editForm = this.formBuilder.group({
        id: [''],
        brand: ['', Validators.required],
        model: ['', Validators.required],
        horsepower: ['', Validators.required],
        yearOfProduction: ['', Validators.required],
        description: ['', Validators.required]
      });
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['carId'];

    this.carsService.getCar(this.id).subscribe(res => {
      this.car = res;
      this.editForm.patchValue(res);
    });
  }

  onSubmit(formData) {
    this.carsService.updateCar(this.id, formData.value).subscribe(res => {
      this.router.navigateByUrl('/cars');
    });
  }
}
