import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Car } from '../models/car';
import { SingleResponseModel } from '../models/singleResponseModel';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  apiUrl = 'https://localhost:5001/api/';

  constructor(private httpClient: HttpClient) {}

  getCars(): Observable<ListResponseModel<Car>> {
    let newPath = this.apiUrl + 'cars/getall';
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  getCarById(carId: number): Observable<SingleResponseModel<Car>> {
    let newPath = this.apiUrl + 'cars/getbyid?id=' + carId;
    return this.httpClient.get<SingleResponseModel<Car>>(newPath);
  }

  getCarsByBrand(brandId: number): Observable<ListResponseModel<Car>> {
    let newPath = this.apiUrl + 'cars/getbybrandid?brandId=' + brandId;
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  getCarsByColor(colorId: number): Observable<ListResponseModel<Car>> {
    let newPath = this.apiUrl + 'cars/getbycolorid?colorId=' + colorId;
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  getCarsByBrandAndColor(brandId:number,colorId:number):Observable<ListResponseModel<Car>>{
    let newPath = this.apiUrl + 'cars/getbycolorandbrand?brandId=' + brandId + '&colorId=' + colorId;
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  add(car:Car):Observable<ResponseModel>{
    let newPath = this.apiUrl + "cars/add";
    return this.httpClient.post<ResponseModel>(newPath,car)
  }

  update(car:Car):Observable<ResponseModel>{
    let newPath = this.apiUrl + "cars/update";
    return this.httpClient.post<ResponseModel>(newPath,car)
  }

  delete(car:Car):Observable<ResponseModel>{
    let newPath = this.apiUrl + "cars/delete";
    return this.httpClient.post<ResponseModel>(newPath,car)
  }
}
