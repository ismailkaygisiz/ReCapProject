import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css'],
})
export class ErrorComponent implements OnInit {
  constructor(private router: Router, private toastrService: ToastrService) {}

  ngOnInit(): void {
    this.router.navigate(['']).then((c) => {
      this.toastrService.error('Page Not Found', 'ERROR');
    });
  }
}
