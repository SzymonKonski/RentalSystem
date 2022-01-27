import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Dealer } from './dealer';
import { catchError } from 'rxjs/operators';
import { EnvironmentUrlService } from "../environment-url.service";


@Injectable({
  providedIn: 'root'
})
export class DealersService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient, private envUrl: EnvironmentUrlService) {}

  getDealers(): Observable<Dealer[]> {
    return this.httpClient.get<Dealer[]>(this.envUrl.createCompleteRoute('api/dealers'));
  }


  getDealer(id): Observable<Dealer> {
    return this.httpClient.get<Dealer>(this.envUrl.createCompleteRoute('api/dealers/' + id))
      .pipe(
        catchError(this.errorHandler)
      );
  }

  createDealer(dealer): Observable<Dealer> {
    return this.httpClient
      .post<Dealer>(this.envUrl.createCompleteRoute('api/dealers/'), JSON.stringify(dealer), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      );
  }

  updateDealer(id, dealer): Observable<Dealer> {
    return this.httpClient.put<Dealer>(this.envUrl.createCompleteRoute('api/dealers/' + id),
      JSON.stringify(dealer),
        this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      );
  }

  deleteDealer(id) {
    return this.httpClient.delete<Dealer>(this.envUrl.createCompleteRoute('api/dealers/' + id), this.httpOptions)
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

