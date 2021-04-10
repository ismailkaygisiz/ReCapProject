import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Car } from '../models/car';
import { SingleResponseModel } from '../models/singleResponseModel';
import { ResponseModel } from '../models/responseModel';
import { apiUrl } from 'src/api';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(private httpClient: HttpClient) {}

  getCars(): Observable<ListResponseModel<Car>> {
    let newPath = apiUrl + 'cars/getall';
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  getCarById(carId: number): Observable<SingleResponseModel<Car>> {
    let newPath = apiUrl + 'cars/getbyid?id=' + carId;
    return this.httpClient.get<SingleResponseModel<Car>>(newPath);
  }

  getCarsByBrand(brandId: number): Observable<ListResponseModel<Car>> {
    let newPath = apiUrl + 'cars/getbybrandid?brandId=' + brandId;
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  getCarsByColor(colorId: number): Observable<ListResponseModel<Car>> {
    let newPath = apiUrl + 'cars/getbycolorid?colorId=' + colorId;
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  getCarsByBrandAndColor(brandId:number,colorId:number):Observable<ListResponseModel<Car>>{
    let newPath = apiUrl + 'cars/getbycolorandbrand?brandId=' + brandId + '&colorId=' + colorId;
    return this.httpClient.get<ListResponseModel<Car>>(newPath);
  }

  add(car:Car):Observable<ResponseModel>{
    let newPath = apiUrl + "cars/add";
    return this.httpClient.post<ResponseModel>(newPath,car)
  }

  update(car:Car):Observable<ResponseModel>{
    let newPath = apiUrl + "cars/update";
    return this.httpClient.post<ResponseModel>(newPath,car)
  }

  delete(car:Car):Observable<ResponseModel>{
    let newPath = apiUrl + "cars/delete";
    return this.httpClient.post<ResponseModel>(newPath,car)
  }
}
