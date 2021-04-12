import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Customer } from 'src/app/models/customer';
import { CustomerAdd } from 'src/app/models/customerAddModel';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { CustomerService } from 'src/app/services/customer.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  customer: CustomerAdd;
  user: User;

  constructor(
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private authService: AuthService,
    private localStorageService: LocalStorageService,
    private router: Router,
    private customerService: CustomerService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      companyName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  register() {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      let registerModel = Object.assign({}, this.registerForm.value);

      this.authService.register(registerModel).subscribe(
        (response) => {
          this.localStorageService.setItem('email', registerModel.email);

          this.userService
            .getUserByMailUseLocalStorage()
            .subscribe((responseUser) => {
              this.user = responseUser.data;
              this.customer = {
                userId: this.user.id,
                companyName: registerModel.companyName,
              };
              this.customerService
                .add(this.customer)
                .subscribe((responseCustomer) => {
                  this.user.status = true;
                  this.localStorageService.setItem(
                    'token',
                    response.data.token
                  );
                  this.router.navigate(['']).then((c) => {
                    window.location.reload();
                  });
                });
            });
        },
        (responseError) => {
          if (responseError.error.Errors != null) {
            for (let i = 0; i < responseError.error.Errors.length; i++) {
              this.toastrService.error(
                responseError.error.Errors[i].ErrorMessage,
                'Hata'
              );
            }
          } else [this.toastrService.error(responseError.error)];
        }
      );
    } else {
      this.toastrService.error(
        'Anlaşılan Formunuz Tamamlanmamış',
        'Form Eksik'
      );
    }
  }
}
