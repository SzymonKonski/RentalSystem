import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Reservation } from "../user/reservation";
import { EnvironmentUrlService } from "../environment-url.service";


@Injectable({
  providedIn: 'root'
})
export class AdminService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient, private envUrl: EnvironmentUrlService) { }

  getAllCurrentRents(): Observable<Reservation[]> {
    return this.httpClient.get<Reservation[]>(this.envUrl.createCompleteRoute('api/reservations/admin/GetCurrent'))
      .pipe(
        catchError(this.errorHandler)
      );
  }

  getAllOldRents(): Observable<Reservation[]> {
    return this.httpClient.get<Reservation[]>(this.envUrl.createCompleteRoute('api/reservations/admin/GetOld'))
      .pipe(
        catchError(this.errorHandler)
      );
  }

  getPrice(id): Observable<number> {
    return this.httpClient.get<number>(this.envUrl.createCompleteRoute('api/reservations/admin/Payment/' + id))
      .pipe(
        catchError(this.errorHandler)
      );
  }

  errorHandler(error) {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}
