import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Customer } from 'src/app/models/customer';
import { CustomerAdd } from 'src/app/models/customerAddModel';
import { User } from 'src/app/models/user';
import { CustomerService } from 'src/app/services/customer.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  user: User;
  customer: Customer;
  dataLoaded: boolean = false;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private router: Router,
    private customerService: CustomerService,
    private localStorageService: LocalStorageService
  ) {}

  ngOnInit(): void {
    this.userService.getUserByMailUseLocalStorage().subscribe((response) => {
      this.user = response.data;
      this.customerService
        .getCustomerByUserId(this.user.id)
        .subscribe((responseCustomer) => {
          this.customer = responseCustomer.data;
          this.dataLoaded = true;
          this.createProfileForm();
        });
    });
  }

  createProfileForm() {
    this.profileForm = this.formBuilder.group({
      firstName: [this.user.firstName, Validators.required],
      lastName: [this.user.lastName, Validators.required],
      companyName: [this.customer.companyName, Validators.required],
    });
  }

  update() {
    if (this.profileForm.valid) {
      let userModel = Object.assign({}, this.profileForm.value);

      userModel.id = this.user.id;
      userModel.status = this.user.status;
      userModel.email = this.user.email;

      let customerModel = {
        id: this.customer.id,
        userId: this.user.id,
        companyName: userModel.companyName,
        findeksPoint: this.customer.findeksPoint,
      };

      this.userService.update(userModel).subscribe(
        (response) => {
          this.updateCustomer(customerModel);
        },
        (responseError) => {
          if (responseError.error.Errors != null) {
            for (let i = 0; i < responseError.error.Errors.length; i++) {
              this.toastrService.error(
                responseError.error.Errors[i].ErrorMessage,
                'Hata'
              );
            }
          }
        }
      );
    } else {
      this.toastrService.error('Anlaşılan Formunuz Eksik', 'Form Eksik');
    }
  }

  delete() {
    this.userService.delete(this.user).subscribe((response) => {
      this.localStorageService.removeItem('token');
      this.localStorageService.removeItem('mail');

      this.router.navigate(['auth/login']);
    });
  }

  updateCustomer(customerModel: any) {
    this.customerService.update(customerModel).subscribe(
      (responseCustomer) => {
        window.location.reload();
      },
      (responseCustomerError) => {
        if (responseCustomerError.error.Errors != null) {
          for (let i = 0; i < responseCustomerError.error.Errors.length; i++) {
            this.toastrService.error(
              responseCustomerError.error.Errors[i].ErrorMessage,
              'Hata'
            );
          }
        }
      }
    );
  }
}
