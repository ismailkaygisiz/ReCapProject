import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
})
export class LogoutComponent implements OnInit {
  user: User;
  dataLoaded: boolean = false;

  constructor(
    private localStorageService: LocalStorageService,
    private authService: AuthService,
    private userService: UserService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.isAuthenticated();
    this.getUser();
  }

  logout() {
    if (this.authService.logout()) {
      this.user.status = false;
      this.toastrService.info('Çıkış Yapıldı', 'Bilgilendirme');

      this.router.navigate(['auth/login']);
    }
  }

  isAuthenticated() {
    return this.authService.isAuthenticated();
  }

  getUser() {
    this.userService.getUserByMailUseLocalStorage().subscribe((response) => {
      this.user = response.data;
      if (this.user != null) {
        this.dataLoaded = true;
      } else {
        this.dataLoaded = false;
        this.getUser();
      }
    });
  }
}
