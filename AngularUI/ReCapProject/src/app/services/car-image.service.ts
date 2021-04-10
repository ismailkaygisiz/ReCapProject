import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarImage } from '../models/carImage';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { apiUrl } from 'src/api';

@Injectable({
  providedIn: 'root',
})
export class CarImageService {
  constructor(private httpClient: HttpClient) {}

  add(carImage: CarImage, file: File): Observable<ResponseModel> {
    let newPath = apiUrl + 'carImages/add';

    const formData: FormData = new FormData();
    formData.append('carId', carImage.carId.toString());

    file != null
      ? formData.append('file', file, file.name)
      : formData.append('', '');

    return this.httpClient.post<ResponseModel>(newPath, formData);
  }

  update(carImage: CarImage, file: File): Observable<ResponseModel> {
    let newPath = apiUrl + 'carImages/update';

    const formData: FormData = new FormData();
    formData.append('id', carImage.id.toString());
    formData.append('carId', carImage.carId.toString());

    file != null
      ? formData.append('file', file, file.name)
      : formData.append('', '');

    return this.httpClient.post<ResponseModel>(newPath, formData);
  }

  delete(carImage: CarImage): Observable<ResponseModel> {
    let newPath = apiUrl + 'carImages/delete';

    const formData: FormData = new FormData();
    formData.append('id', carImage.id.toString());

    return this.httpClient.post<ResponseModel>(newPath, formData);
  }

  getById(carImageId: number): Observable<SingleResponseModel<CarImage>> {
    let newPath = apiUrl + 'carImages/getbyid?id=' + carImageId;

    return this.httpClient.get<SingleResponseModel<CarImage>>(newPath);
  }
}
