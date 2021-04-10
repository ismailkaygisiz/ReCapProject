import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Customer } from 'src/app/models/customer';
import { Rental } from 'src/app/models/rental';
import { CustomerService } from 'src/app/services/customer.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { PaymentService } from 'src/app/services/payment.service';
import { RentalService } from 'src/app/services/rental.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-car-rental',
  templateUrl: './car-rental.component.html',
  styleUrls: ['./car-rental.component.css'],
})
export class CarRentalComponent implements OnInit {
  rentalAddForm: FormGroup;
  rents: Rental[];
  lastRent: Rental;
  carId: number;
  currentDate: Date = new Date();
  rent: Rental;
  minRentDate: Date;
  minReturnDate: Date;
  customer: Customer;
  dataLoaded: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private rentalService: RentalService,
    private toastrService: ToastrService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private paymentService: PaymentService,
    private customerService: CustomerService,
    private userService: UserService,
    private localStorageService: LocalStorageService
  ) {}

  ngOnInit(): void {
    this.createRentalAddForm();
    this.getObjects();
  }

  createRentalAddForm() {
    this.rentalAddForm = this.formBuilder.group({
      rentDate: ['', Validators.required],
      returnDate: ['', Validators.required],
    });
  }

  add() {
    this.getObjects();

    if (this.rentalAddForm.valid) {
      let rentalModel = Object.assign({}, this.rentalAddForm.value);
      let rentDate = new Date(rentalModel.rentDate);
      let returnDate = new Date(rentalModel.returnDate);

      rentalModel.customerId = this.customer.id;
      rentalModel.carId = this.carId;

      if (rentDate <= this.currentDate) {
        this.toastrService.warning(
          'Kiralanma Tarihi Bugünden Sonrası Olmak Zorundadır',
          'İşlem Geri Alındı'
        );

        return false;
      } else if (returnDate <= rentDate) {
        this.toastrService.warning(
          'Aracın Geri Dönüş Tarihi Kiralanma Tarihinden Sonrası Olmak Zorundadır',
          'İşlem Geri Alındı'
        );

        return false;
      }

      this.paymentService.setRental(rentalModel);
      this.router.navigate(['car-rental/', rentalModel.carId, 'pay']);
    } else {
      this.toastrService.error(
        'Anlaşılan formunuz henüz tamamlanmamış',
        'Form Tamamlanmadı'
      );
    }

    return true;
  }

  getObjects() {
    this.activatedRoute.params.subscribe((params) => {
      this.userService.getUserByMailUseLocalStorage().subscribe((response) => {
        this.customerService
          .getCustomerByUserId(response.data.id)
          .subscribe((responseCustomer) => {
            this.customer = responseCustomer.data;
            this.dataLoaded = true;
          });
      });
      if (params['car']) {
        this.rentalService
          .getRentalsByCar(params['car'])
          .subscribe((response) => {
            this.carId = +params['car'];
            this.rents = response.data;
            this.lastRent = this.rents[this.rents.length - 1];

            // Düzenlenecek
            if (this.lastRent) {
              if (new Date(this.lastRent.returnDate) > this.currentDate) {
                this.minRentDate = new Date(this.lastRent.returnDate);
                this.minReturnDate = new Date(this.lastRent.returnDate);
              } else {
                this.minRentDate = new Date();
                this.minReturnDate = new Date();

                this.minRentDate = new Date(
                  this.minRentDate.setDate(this.currentDate.getDay() + 5)
                );
                this.minReturnDate = new Date(
                  this.minReturnDate.setDate(this.minRentDate.getDay() + 5)
                );
              }
            } else {
              this.minRentDate = new Date();
              this.minReturnDate = new Date();

              this.minRentDate = new Date(
                this.minRentDate.setDate(this.currentDate.getDay() + 5)
              );
              this.minReturnDate = new Date(
                this.minReturnDate.setDate(this.currentDate.getDay() + 6)
              );
            }
            // Düzenlenecek
          });
      }
    });
  }
}
