import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Color } from '../models/color';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { apiUrl } from 'src/api';

@Injectable({
  providedIn: 'root',
})
export class ColorService {

  constructor(private httpClient: HttpClient) {}

  getColors(): Observable<ListResponseModel<Color>> {
    let newPath = apiUrl + 'colors/getall';
    return this.httpClient.get<ListResponseModel<Color>>(newPath);
  }

  add(color: Color): Observable<ResponseModel> {
    let newPath = apiUrl + 'colors/add';
    return this.httpClient.post<ResponseModel>(newPath, color);
  }

  update(color: Color): Observable<ResponseModel> {
    let newPath = apiUrl + 'colors/update';
    return this.httpClient.post<ResponseModel>(newPath, color);
  }

  delete(color: Color): Observable<ResponseModel> {
    let newPath = apiUrl + 'colors/delete';
    return this.httpClient.post<ResponseModel>(newPath, color);
  }

  getByName(colorName: string): Observable<SingleResponseModel<Color>> {
    let newPath = apiUrl + 'colors/getbyname?colorname=' + colorName;
    return this.httpClient.get<SingleResponseModel<Color>>(newPath);
  }

  getById(colorId: number): Observable<SingleResponseModel<Color>> {
    let newPath = apiUrl + 'colors/getbyid?id=' + colorId;
    return this.httpClient.get<SingleResponseModel<Color>>(newPath);
  }
}
