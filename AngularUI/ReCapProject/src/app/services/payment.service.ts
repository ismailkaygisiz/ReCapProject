import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Payment } from '../models/payment';
import { RentalAdd } from '../models/rentalAdd';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  apiUrl = 'https://localhost:5001/api/';
  rental: RentalAdd;
  constructor(private httpClient: HttpClient) {}

  pay(payment: Payment): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'payment/pay';
    return this.httpClient.post<ResponseModel>(newPath, payment);
  }

  add(payment: Payment): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'payment/add';

    return this.httpClient.post<ResponseModel>(newPath, payment);
  }

  getPaymentsByCustomerId(
    customerId: number
  ): Observable<ListResponseModel<Payment>> {
    let newPath =
      this.apiUrl + 'payment/getpaymentsbycustomerid?customerid=' + customerId;
    return this.httpClient.get<ListResponseModel<Payment>>(newPath);
  }

  getPaymentById(id: number): Observable<SingleResponseModel<Payment>> {
    let newPath = this.apiUrl + 'payment/getpaymentbyid?id=' + id;
    return this.httpClient.get<SingleResponseModel<Payment>>(newPath);
  }

  getRental() {
    return this.rental;
  }

  setRental(rental: RentalAdd) {
    this.rental = rental;
  }
}
