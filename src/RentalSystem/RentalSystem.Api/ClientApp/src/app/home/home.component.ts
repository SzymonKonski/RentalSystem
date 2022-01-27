import { Component } from '@angular/core';
import { EnvironmentUrlService } from "../environment-url.service";
import { environment } from "../../environments/environment";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public urlAddress: string = environment.urlAddress;

}
