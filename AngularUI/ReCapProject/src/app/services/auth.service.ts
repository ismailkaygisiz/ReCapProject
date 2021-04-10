import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { LoginModel } from '../models/loginModel';
import { RegisterModel } from '../models/registerModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { TokenModel } from '../models/tokenModel';
import { CustomerService } from './customer.service';
import { LocalStorageService } from './local-storage.service';
import { apiUrl } from 'src/api';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private localStorageService: LocalStorageService,
    private customerService: CustomerService
  ) {}

  login(user: LoginModel): Observable<SingleResponseModel<TokenModel>> {
    let newPath = apiUrl + 'auth/login';

    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, user);
  }

  register(user: RegisterModel): Observable<SingleResponseModel<TokenModel>> {
    let newPath = apiUrl + 'auth/register';

    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, user);
  }

  logout() {
    if (
      this.localStorageService.getItem('token') &&
      this.localStorageService.getItem('email')
    ) {
      this.localStorageService.removeItem('token');
      this.localStorageService.removeItem('email');
      return true;
    }

    return false;
  }

  isAuthenticated() {
    if (
      this.localStorageService.getItem('token') &&
      this.localStorageService.getItem('email')
    ) {
      return true;
    }

    return false;
  }
}
