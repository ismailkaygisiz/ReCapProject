import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { User } from '../models/user';
import { AuthService } from './auth.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiUrl = 'https://localhost:5001/api/';

  constructor(
    private localStorageService: LocalStorageService,
    private httpClient: HttpClient
  ) {}

  getUserByMailUseLocalStorage(): Observable<SingleResponseModel<User>> {
    let newPath =
      this.apiUrl +
      'users/getbymail?email=' +
      this.localStorageService.getItem('email');

    return this.httpClient.get<SingleResponseModel<User>>(newPath);
  }

  update(user: User): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'users/update';

    return this.httpClient.post<ResponseModel>(newPath, user);
  }

  delete(user: User): Observable<ResponseModel> {
    let newPath = this.apiUrl + 'users/delete';

    return this.httpClient.post<ResponseModel>(newPath, user);
  }
}
