import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DealersService } from "../dealer.service";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  createForm;

  constructor(
    public dealerService: DealersService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder) {


    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    });

  }

  ngOnInit(): void {
  }

  onSubmit(formData) {
    this.dealerService.createDealer(formData.value).subscribe(res => {
      this.router.navigateByUrl('/dealers');
    });
  }

}
