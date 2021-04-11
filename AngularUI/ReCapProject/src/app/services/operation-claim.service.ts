import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/api';
import { ListResponseModel } from '../models/listResponseModel';
import { OperationClaim } from '../models/operationClaim';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root',
})
export class OperationClaimService {
  constructor(private httpClient: HttpClient) {}

  add(operationClaim: OperationClaim): Observable<ResponseModel> {
    let newPath = apiUrl + 'operationclaims/add';
    return this.httpClient.post<ResponseModel>(newPath, operationClaim);
  }

  delete(operationClaim: OperationClaim): Observable<ResponseModel> {
    let newPath = apiUrl + 'operationclaims/delete';
    return this.httpClient.post<ResponseModel>(newPath, operationClaim);
  }

  update(operationClaim: OperationClaim): Observable<ResponseModel> {
    let newPath = apiUrl + 'operationclaims/update';
    return this.httpClient.post<ResponseModel>(newPath, operationClaim);
  }

  getAll(): Observable<ListResponseModel<OperationClaim>> {
    let newPath = apiUrl + 'operationclaims/getall';
    return this.httpClient.get<ListResponseModel<OperationClaim>>(newPath);
  }

  getById(id: number): Observable<SingleResponseModel<OperationClaim>> {
    let newPath = apiUrl + 'operationclaims/getbyid?id=' + id;
    return this.httpClient.get<SingleResponseModel<OperationClaim>>(newPath);
  }
}
