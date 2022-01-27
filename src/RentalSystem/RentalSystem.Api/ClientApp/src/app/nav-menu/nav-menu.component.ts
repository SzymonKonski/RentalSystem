import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { AuthenticationService } from "../shared/services/authentication.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{

  ngOnInit() {
    //this.isUserAdmin = this._authService.isUserAdmin();
  }

  constructor(public _authService: AuthenticationService) {

  }


  isUserAdmin() {
    return this._authService.isUserAdmin();
  }

  isExpanded = false;
  @Input() loggedIn: boolean;
  //isUserAdmin: boolean;
  @Output() loginClicked = new EventEmitter<void>();
  @Output() logoutClicked = new EventEmitter<void>();

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  login() {
    this.loginClicked.emit();
  }

  logout() {
    this.logoutClicked.emit();
  }
}
