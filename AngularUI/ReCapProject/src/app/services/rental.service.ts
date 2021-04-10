import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Rental } from '../models/rental';
import { RentalAdd } from '../models/rentalAdd';
import { ResponseModel } from '../models/responseModel';
import { CustomerService } from './customer.service';
import { UserService } from './user.service';
import { apiUrl } from 'src/api';

@Injectable({
  providedIn: 'root',
})
export class RentalService {

  constructor(
    private httpClient: HttpClient,
    private customerService: CustomerService,
    private userService: UserService
  ) {}

  getRentals(): Observable<ListResponseModel<Rental>> {
    let newPath = apiUrl + 'rentals/getall';
    return this.httpClient.get<ListResponseModel<Rental>>(newPath);
  }

  getRentalsByCar(carId: number): Observable<ListResponseModel<Rental>> {
    let newPath = apiUrl + 'rentals/getbycarid?carid=' + carId;
    return this.httpClient.get<ListResponseModel<Rental>>(newPath);
  }

  addRental(
    rental: RentalAdd,
    customerFindeksPoint: number,
    carFindeksPoint: number
  ): Observable<ResponseModel> {
    let newPath = apiUrl + 'rentals/add';

    let customer = {
      id: rental.customerId,
      findeksPoint: customerFindeksPoint,
      carFindeksPoint: carFindeksPoint,
    };

    this.customerService.increase(customer).subscribe((response) => {console.log(response.message)});

    const formData: FormData = new FormData();

    formData.append('carId', rental.carId.toString());
    formData.append('customerId', rental.customerId.toString());
    formData.append('rentDate', rental.rentDate.toString());
    formData.append('returnDate', rental.returnDate.toString());
    formData.append('customerFindeksPoint', customerFindeksPoint.toString());
    formData.append('carFindeksPoint', carFindeksPoint.toString());

    return this.httpClient.post<ResponseModel>(newPath, formData);
  }
}
