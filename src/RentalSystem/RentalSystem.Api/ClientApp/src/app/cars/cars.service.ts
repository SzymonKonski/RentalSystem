import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Car } from "./car";
import { EnvironmentUrlService } from "../environment-url.service";


@Injectable({
  providedIn: 'root'
})

export class CarsService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient, private envUrl: EnvironmentUrlService) {}

  getCarsFiltered(query?: string): Observable<Car[]> {

    if (!query)
      return this.httpClient.get<Car[]>(this.envUrl.createCompleteRoute('api/cars'));

    return this.httpClient.get<Car[]>(this.envUrl.createCompleteRoute('api/cars?' + query));
  }

  getCar(id): Observable<Car> {
    return this.httpClient.get<Car>(this.envUrl.createCompleteRoute('api/cars/' + id))
      .pipe(
        catchError(this.errorHandler)
      );
  }

  createCar(car): Observable<Car> {
    return this.httpClient.post<Car>(this.envUrl.createCompleteRoute('api/cars/'), JSON.stringify(car), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      );
  }

  updateCar(id, car): Observable<Car> {
    return this.httpClient.put<Car>(this.envUrl.createCompleteRoute('api/cars/' + id), JSON.stringify(car), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      );
  }

  deleteCar(id) {
    return this.httpClient.delete<Car>(this.envUrl.createCompleteRoute('api/cars/' + id), this.httpOptions)
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
