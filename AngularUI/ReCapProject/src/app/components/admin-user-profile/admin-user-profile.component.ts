import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OperationClaim } from 'src/app/models/operationClaim';
import { OperationClaimDetailDto } from 'src/app/models/operationClaimDetailDto';
import { User } from 'src/app/models/user';
import { OperationClaimService } from 'src/app/services/operation-claim.service';
import { UserOperationClaimService } from 'src/app/services/user-operation-claim.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin-user-profile',
  templateUrl: './admin-user-profile.component.html',
  styleUrls: ['./admin-user-profile.component.css'],
})
export class AdminUserProfileComponent implements OnInit {
  user: User;
  userOperationClaims: OperationClaimDetailDto[];
  claims: OperationClaim[];
  data: OperationClaim;
  id: number;

  dataLoaded: boolean = false;
  dataLoadedUserClaims: boolean = false;
  claimId: number;

  constructor(
    private userService: UserService,
    private operationClaimService: OperationClaimService,
    private activatedRoute: ActivatedRoute,
    private userOperationClaimService: UserOperationClaimService
  ) {}

  ngOnInit(): void {
    this.userOperationClaimService.control();

    this.activatedRoute.params.subscribe((params) => {
      if (params['userId']) {
        this.userService.getUserById(params['userId']).subscribe((response) => {
          this.user = response.data;
          this.getOperationClaims(this.user.id);
          this.dataLoaded = true;
        });
      }
    });
  }

  getOperationClaims(id: number) {
    this.operationClaimService.getAll().subscribe((responseClaim) => {
      this.userOperationClaimService
        .getDetailsByUserId(id)
        .subscribe((response) => {
          this.userOperationClaims = response.data;
          this.dataLoadedUserClaims = true;
          if (response.data.length > 0) {
            // Filtreleme uygulanacak
            this.userService
              .getClaims(this.user)
              .subscribe((responseUser) => {});

            this.claims = responseClaim.data;
          } else {
            this.claims = responseClaim.data;
          }
        });
    });
  }

  add() {
    this.activatedRoute.params.subscribe((params) => {
      if (params['userId']) {
        this.userOperationClaimService
          .add({
            userId: +params['userId'],
            operationClaimId: +this.claimId,
          })
          .subscribe((response) => {
            window.location.reload();
          });
      }
    });
  }

  delete(id: number) {
    this.activatedRoute.params.subscribe((params) => {
      if (params['userId']) {
        this.userOperationClaimService
          .delete({
            id: +id,
            userId: +params['userId'],
            operationClaimId: 0,
          })
          .subscribe((response) => {
            window.location.reload();
          });
      }
    });
  }

  set(id: number) {
    this.id = id;
  }

  get() {
    return this.id;
  }
}
