import { Component, OnInit } from '@angular/core';
import { UserOperationClaimService } from 'src/app/services/user-operation-claim.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css'],
})
export class AdminLayoutComponent implements OnInit {
  constructor(private userOperationClaimService: UserOperationClaimService) {}

  ngOnInit(): void {
    this.userOperationClaimService.adminControl();
  }
}
