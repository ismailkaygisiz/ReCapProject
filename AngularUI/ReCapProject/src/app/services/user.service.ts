import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { User } from '../models/user';
import { AuthService } from './auth.service';
import { LocalStorageService } from './local-storage.service';
import { apiUrl } from 'src/api';

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

  update(user: User): Observable<ResponseModel> {
    let newPath = apiUrl + 'users/update';

    return this.httpClient.post<ResponseModel>(newPath, user);
  }

  delete(user: User): Observable<ResponseModel> {
    let newPath = apiUrl + 'users/delete';

    return this.httpClient.post<ResponseModel>(newPath, user);
  }
}
