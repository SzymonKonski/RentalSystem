import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Dealer } from "../dealer";
import { DealersService } from "../dealer.service";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  dealer: Dealer;
  editForm;

  constructor(
    public dealersService: DealersService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.editForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['dealerId'];

    this.dealersService.getDealer(this.id).subscribe(res => {
      this.dealer = res;
      this.editForm.patchValue(res);
    });
  }

  onSubmit(formData) {
    this.dealersService.updateDealer(this.id, formData.value).subscribe(res => {
      this.router.navigateByUrl('/dealers');
    });
  }
}
