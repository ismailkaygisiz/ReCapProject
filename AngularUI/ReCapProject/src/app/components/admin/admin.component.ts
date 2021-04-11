import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OperationClaim } from 'src/app/models/operationClaim';
import { OperationClaimDetailDto } from 'src/app/models/operationClaimDetailDto';
import { User } from 'src/app/models/user';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { OperationClaimService } from 'src/app/services/operation-claim.service';
import { UserOperationClaimService } from 'src/app/services/user-operation-claim.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
})
export class AdminComponent implements OnInit {
  user: User;
  operationClaims: OperationClaim[];
  users: User[];

  constructor(
    private userService: UserService,
    private operaionClaimService: OperationClaimService,
    private userOperationClaimService: UserOperationClaimService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.control();
    this.getOperationClaims();
    this.getUsers();
  }

  control() {
    this.userService.getUserByMailUseLocalStorage().subscribe((response) => {
      this.user = response.data;

      this.userOperationClaimService
        .getDetailsByUserId(this.user.id)
        .subscribe((response) => {
          for (let i = 0; i < response.data.length; i++) {
            if (response.data[i].claim == 'Admin') {
              return true;
            }
          }

          this.router.navigate(['']);
          return false;
        });
    });
  }

  getOperationClaims() {
    this.operaionClaimService.getAll().subscribe((response) => {
      this.operationClaims = response.data;
    });
  }

  getUsers() {
    this.userService.getAll().subscribe((response) => {
      this.users = response.data;
    });
  }
}
