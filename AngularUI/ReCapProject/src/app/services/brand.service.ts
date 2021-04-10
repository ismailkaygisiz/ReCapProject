import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Brand } from '../models/brand';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { apiUrl } from 'src/api';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private httpClient: HttpClient) {}

  getBrands(): Observable<ListResponseModel<Brand>> {
    let newPath = apiUrl + 'brands/getall';
    return this.httpClient.get<ListResponseModel<Brand>>(newPath);
  }

  add(brand: Brand): Observable<ResponseModel> {
    let newPath = apiUrl + 'brands/add';
    return this.httpClient.post<ResponseModel>(newPath, brand);
  }

  update(brand: Brand): Observable<ResponseModel> {
    let newPath = apiUrl + 'brands/update';
    return this.httpClient.post<ResponseModel>(newPath, brand);
  }

  delete(brand: Brand): Observable<ResponseModel> {
    let newPath = apiUrl + 'brands/delete';
    return this.httpClient.post<ResponseModel>(newPath, brand);
  }

  getByName(brandName: string): Observable<SingleResponseModel<Brand>> {
    let newPath = apiUrl + 'brands/getbyname?brandname=' + brandName;
    return this.httpClient.get<SingleResponseModel<Brand>>(newPath);
  }

  getById(brandId: number): Observable<SingleResponseModel<Brand>> {
    let newPath = apiUrl + 'brands/getbyid?id=' + brandId;
    return this.httpClient.get<SingleResponseModel<Brand>>(newPath);
  }
}
