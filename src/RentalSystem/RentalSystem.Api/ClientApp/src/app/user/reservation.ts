import { Car } from "../cars/car";

export interface Reservation {
  id: number;
  rentFrom: Date;
  rentTo: Date;
  car: Car;
}
