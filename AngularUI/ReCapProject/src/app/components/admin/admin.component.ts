import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OperationClaim } from 'src/app/models/operationClaim';
import { User } from 'src/app/models/user';
import { OperationClaimService } from 'src/app/services/operation-claim.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
})
export class AdminComponent implements OnInit {
  operationClaims: OperationClaim[];
  users: User[];

  constructor(
    private userService: UserService,
    private operaionClaimService: OperationClaimService
  ) {}

  ngOnInit(): void {
    this.getOperationClaims();
    this.getUsers();
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
