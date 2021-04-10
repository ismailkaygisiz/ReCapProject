import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { CustomerAdd } from '../models/customerAddModel';
import { ListResponseModel } from '../models/listResponseModel';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  apiUrl = 'https://localhost:5001/api/';

  constructor(private httpClient: HttpClient) {}

  add(customer: CustomerAdd): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'customers/add';

    return this.httpClient.post<ResponseModel>(newPath, customer);
  }

  update(customer: CustomerAdd): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'customers/update';

    return this.httpClient.post<ResponseModel>(newPath, customer);
  }

  increase(customer: any): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'customers/increasefindekspoint';
    const formData: FormData = new FormData();

    formData.append('id', customer.id.toString());
    formData.append('carFindeksPoint', customer.carFindeksPoint.toString());

    return this.httpClient.post<ResponseModel>(newPath, formData);
  }

  getCustomers(): Observable<ListResponseModel<Customer>> {
    let newPath = this.apiUrl + 'customers/getall';
    return this.httpClient.get<ListResponseModel<Customer>>(newPath);
  }

  getCustomerById(id: number): Observable<SingleResponseModel<Customer>> {
    let newPath = this.apiUrl + 'customers/getbyid?id=' + id;
    return this.httpClient.get<SingleResponseModel<Customer>>(newPath);
  }

  getCustomerByUserId(
    userId: number
  ): Observable<SingleResponseModel<Customer>> {
    let newPath = this.apiUrl + 'customers/getbyuserid?userid=' + userId;
    return this.httpClient.get<SingleResponseModel<Customer>>(newPath);
  }
}
