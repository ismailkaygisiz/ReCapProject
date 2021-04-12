import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { User } from '../models/user';
import { AuthService } from './auth.service';
import { LocalStorageService } from './local-storage.service';
import { apiUrl } from 'src/api';
import { ListResponseModel } from '../models/listResponseModel';
import { OperationClaim } from '../models/operationClaim';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private localStorageService: LocalStorageService,
    private httpClient: HttpClient
  ) {}

  getUserByMailUseLocalStorage(): Observable<SingleResponseModel<User>> {
    let newPath =
      apiUrl +
      'users/getbymail?email=' +
      this.localStorageService.getItem('email');

    return this.httpClient.get<SingleResponseModel<User>>(newPath);
  }

  getUserById(id: number): Observable<SingleResponseModel<User>> {
    let newPath = apiUrl + 'users/getbyid?id=' + id;

    return this.httpClient.get<SingleResponseModel<User>>(newPath);
  }

  getAll(): Observable<ListResponseModel<User>> {
    let newPath = apiUrl + 'users/getall';

    return this.httpClient.get<ListResponseModel<User>>(newPath);
  }

  update(user: User): Observable<ResponseModel> {
    let newPath = apiUrl + 'users/update';

    return this.httpClient.post<ResponseModel>(newPath, user);
  }

  delete(user: User): Observable<ResponseModel> {
    let newPath = apiUrl + 'users/delete';

    return this.httpClient.post<ResponseModel>(newPath, user);
  }

  getClaims(user: User): Observable<ListResponseModel<OperationClaim>> {
    let newPath = apiUrl + 'users/getclaims';

    return this.httpClient.post<ListResponseModel<OperationClaim>>(
      newPath,
      user
    );
  }
}
