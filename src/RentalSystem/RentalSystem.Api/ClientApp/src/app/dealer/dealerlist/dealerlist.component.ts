import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../../shared/services/authentication.service";
import { DealersService } from "../../dealer/dealer.service";
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Dealer } from "../../dealer/dealer";

@Component({
  selector: 'app-dealerlist',
  templateUrl: './dealerlist.component.html',
  styleUrls: ['./dealerlist.component.css']
})
export class DealerlistComponent implements OnInit {

  isUserAdmin: boolean;
  dealers: Dealer[];
  displayedColumns: string[] = ['id', 'name', 'phoneNumber'];

  constructor(private _liveAnnouncer: LiveAnnouncer, public dealersService: DealersService,
    public _authService: AuthenticationService) { }

  ngOnInit() {

    this.isUserAdmin = this._authService.isUserAdmin();

    var httpGet = this.dealersService.getDealers();

    httpGet.subscribe(result => {
      this.dealers = result;
    },
      error => console.error(error));

    console.log(this.dealers[0].phoneNumber);
  }
  deleteDealer(id) {
    this.dealersService.deleteDealer(id).subscribe(res => {
      this.dealers = this.dealers.filter(car => car.id !== id);
    });
  }
}

