import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/api';
import { ListResponseModel } from '../models/listResponseModel';
import { OperationClaimDetailDto } from '../models/operationClaimDetailDto';
import { ResponseModel } from '../models/responseModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { UserOperationClaim } from '../models/userOperationClaim';
import { UserOperationClaimAdd } from '../models/userOperationClaimAdd';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class UserOperationClaimService {
  constructor(
    private httpClient: HttpClient,
    private userService: UserService,
    private router: Router
  ) {}

  add(userOperationClaim: UserOperationClaimAdd): Observable<ResponseModel> {
    let newPath = apiUrl + 'useroperationclaims/add';
    return this.httpClient.post<ResponseModel>(newPath, userOperationClaim);
  }

  delete(userOperationClaim: UserOperationClaim): Observable<ResponseModel> {
    let newPath = apiUrl + 'useroperationclaims/delete';
    return this.httpClient.post<ResponseModel>(newPath, userOperationClaim);
  }

  update(userOperationClaim: UserOperationClaim): Observable<ResponseModel> {
    let newPath = apiUrl + 'useroperationclaims/update';
    return this.httpClient.post<ResponseModel>(newPath, userOperationClaim);
  }

  getAll(): Observable<ListResponseModel<UserOperationClaim>> {
    let newPath = apiUrl + 'useroperationclaims/getall';
    return this.httpClient.get<ListResponseModel<UserOperationClaim>>(newPath);
  }

  getById(id: number): Observable<SingleResponseModel<UserOperationClaim>> {
    let newPath = apiUrl + 'useroperationclaims/getbyid?id=' + id;
    return this.httpClient.get<SingleResponseModel<UserOperationClaim>>(
      newPath
    );
  }

  getByClaimId(id: number): Observable<ListResponseModel<UserOperationClaim>> {
    let newPath = apiUrl + 'useroperationclaims/getbyclaimid?claimid=' + id;
    return this.httpClient.get<ListResponseModel<UserOperationClaim>>(newPath);
  }

  getByUserId(id: number): Observable<ListResponseModel<UserOperationClaim>> {
    let newPath = apiUrl + 'useroperationclaims/getbyuserid?userid=' + id;
    return this.httpClient.get<ListResponseModel<UserOperationClaim>>(newPath);
  }

  getAllDetails(): Observable<ListResponseModel<OperationClaimDetailDto>> {
    let newPath = apiUrl + 'useroperationclaims/getalldetails';
    return this.httpClient.get<ListResponseModel<OperationClaimDetailDto>>(
      newPath
    );
  }

  getDetailsById(
    id: number
  ): Observable<SingleResponseModel<OperationClaimDetailDto>> {
    let newPath = apiUrl + 'useroperationclaims/getdetailsbyid?id=' + id;
    return this.httpClient.get<SingleResponseModel<OperationClaimDetailDto>>(
      newPath
    );
  }

  getDetailsByClaimId(
    id: number
  ): Observable<ListResponseModel<OperationClaimDetailDto>> {
    let newPath =
      apiUrl + 'useroperationclaims/getdetailsbyclaimid?claimid=' + id;
    return this.httpClient.get<ListResponseModel<OperationClaimDetailDto>>(
      newPath
    );
  }

  getDetailsByUserId(
    id: number
  ): Observable<ListResponseModel<OperationClaimDetailDto>> {
    let newPath =
      apiUrl + 'useroperationclaims/getdetailsbyuserid?userid=' + id;
    return this.httpClient.get<ListResponseModel<OperationClaimDetailDto>>(
      newPath
    );
  }

  adminControl() {
    this.userService.getUserByMailUseLocalStorage().subscribe((response) => {
      this.getDetailsByUserId(response.data.id).subscribe((response) => {
        for (let i = 0; i < response.data.length; i++) {
          if (
            response.data[i].claim == 'Admin' ||
            response.data[i].claim == 'Moderatör'
          ) {
            return true;
          }
        }

        this.router.navigate(['']).then((c) => {});

        return false;
      });
    });
  }
}
