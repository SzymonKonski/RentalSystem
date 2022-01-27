import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Rent } from "./rent";
import { EnvironmentUrlService } from "../environment-url.service";


@Injectable({
  providedIn: 'root'
})

export class RentmeService {


  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient, private envUrl: EnvironmentUrlService) { }


  createRent(rent): Observable<Rent> {
    return this.httpClient.post<Rent>(this.envUrl.createCompleteRoute('api/reservations/'), JSON.stringify(rent), this.httpOptions)
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
