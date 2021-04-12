import { Component, OnInit } from '@angular/core';
import { UserOperationClaimService } from 'src/app/services/user-operation-claim.service';

@Component({
  selector: 'app-admin-loyaut',
  templateUrl: './admin-loyaut.component.html',
  styleUrls: ['./admin-loyaut.component.css'],
})
export class AdminLoyautComponent implements OnInit {
  constructor(private userOperationClaimService: UserOperationClaimService) {}

  ngOnInit(): void {
    this.userOperationClaimService.adminControl();
  }
}
