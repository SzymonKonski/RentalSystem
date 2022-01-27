import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { IPublicClientApplication, PublicClientApplication, InteractionType } from '@azure/msal-browser';
import { MsalGuard, MsalInterceptor, MsalBroadcastService, MsalInterceptorConfiguration, MsalModule, MsalService, MSAL_GUARD_CONFIG, MSAL_INSTANCE, MSAL_INTERCEPTOR_CONFIG, MsalGuardConfiguration, MsalRedirectComponent } from '@azure/msal-angular';
import { msalConfig, loginRequest, protectedResources } from './auth-config';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { JwtModule } from "@auth0/angular-jwt";
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { AdminGuard } from "./shared/guards/admin.guard";
import { environment } from "../environments/environment";


export function tokenGetter() {
  return localStorage.getItem("token");
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication(msalConfig);
}

export function MSALInterceptorConfigFactory() {
  const protectedResourceMap = new Map<string, Array<string>>();

  protectedResourceMap.set(protectedResources.carListApi.endpoint, protectedResources.carListApi.scopes);
  protectedResourceMap.set(protectedResources.reservationApi.endpoint, protectedResources.reservationApi.scopes);
  protectedResourceMap.set(protectedResources.authorizationApi.endpoint, protectedResources.authorizationApi.scopes);
  protectedResourceMap.set(protectedResources.dealersApi.endpoint, protectedResources.dealersApi.scopes);
  protectedResourceMap.set(protectedResources.adminReservationsApi.endpoint, protectedResources.adminReservationsApi.scopes);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}


export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: loginRequest
  };
}


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ForbiddenComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'cars', loadChildren: './cars/cars.module#CarsModule', canActivate: [MsalGuard] },
      { path: 'dealers', loadChildren: './dealer/dealer.module#DealerModule', canActivate: [MsalGuard] },
      { path: 'rentme', loadChildren: './rentme/rentme.module#RentmeModule', canActivate: [MsalGuard] },
      { path: 'user', loadChildren: './user/user.module#UserModule', canActivate: [MsalGuard] },
      { path: 'admin', loadChildren: './admin/admin.module#AdminModule', canActivate: [MsalGuard, AdminGuard] },
      { path: 'forbidden', component: ForbiddenComponent },
      { path: '404', component: NotFoundComponent },
      { path: '**', redirectTo: '/404', pathMatch: 'full' }
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5001", environment.urlAddress],
        blacklistedRoutes: []
      }
    }),
    MsalModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
