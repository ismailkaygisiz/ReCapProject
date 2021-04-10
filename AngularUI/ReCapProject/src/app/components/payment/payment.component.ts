import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Car } from 'src/app/models/car';
import { Customer } from 'src/app/models/customer';
import { Payment } from 'src/app/models/payment';
import { Rental } from 'src/app/models/rental';
import { RentalAdd } from 'src/app/models/rentalAdd';
import { CarService } from 'src/app/services/car.service';
import { CustomerService } from 'src/app/services/customer.service';
import { PaymentService } from 'src/app/services/payment.service';
import { RentalService } from 'src/app/services/rental.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css'],
})
export class PaymentComponent implements OnInit {
  paymentForm: FormGroup;
  rent: RentalAdd;
  customer: Customer;
  car: Car;
  payment: Payment;
  payments: Payment[];
  carId: number;
  saveCard: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private paymentService: PaymentService,
    private rentalService: RentalService,
    private router: Router,
    private carService: CarService,
    private customerService: CustomerService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params['carIdForPay']) {
        this.carId = params['carIdForPay'];
        this.getRent();
        this.CreatePaymentForm();
      }
    });
  }

  CreatePaymentForm() {
    this.paymentForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      cardNumber: ['', Validators.required],
      year: ['', Validators.required],
      month: ['', Validators.required],
      cvv: ['', Validators.required],
    });
  }

  pay() {
    this.rent = this.paymentService.getRental();

    if (this.paymentForm.valid) {
      if (this.rent != null) {
        let paymentModel = Object.assign({}, this.paymentForm.value);

        paymentModel.cardNumber = paymentModel.cardNumber.toString();
        paymentModel.customerId = this.rent.customerId;

        this.payment = paymentModel;

        this.payManager(paymentModel);
      } else {
        this.toastrService.error(
          'Hata İşlem Tamamlanamadı Kiralama Sayfasına Yönlendiriliyorsunuz',
          'Hata'
        );
        this.router.navigate(['car-rental/', this.carId]);
      }
    } else {
      this.toastrService.error(
        'Anlaşılan Formunuz Henüz Tamamlanmamış',
        'Form Eksik'
      );
    }
  }

  setCurrentCard(payment: Payment) {
    this.paymentForm.setValue({
      firstName: payment.firstName,
      lastName: payment.lastName,
      cardNumber: payment.cardNumber,
      year: payment.year,
      month: payment.month,
      cvv: payment.cvv,
    });
  }

  save(payment: Payment) {
    for (let i = 0; i < this.payments.length; i++) {
      if (this.payments[i].cardNumber == payment.cardNumber) {
        return;
      }
    }

    this.paymentService.add(payment).subscribe(
      (response) => {
        this.toastrService.success('Kart Kaydedildi', 'Başarılı');
      },
      (responseError) => {
        this.toastrService.error(
          'Bir Şeyler Ters Gitti Kart Kayedilemedi',
          'Hata'
        );
      }
    );
  }

  payManager(paymentModel: any) {
    this.paymentService.pay(paymentModel).subscribe((response) => {
      this.customerService
        .getCustomerById(this.rent.customerId)
        .subscribe((response) => {
          this.customer = response.data;

          this.carService.getCarById(this.rent.carId).subscribe((response) => {
            this.car = response.data;
            this.rentalService
              .addRental(
                this.rent,
                this.customer.findeksPoint,
                this.car.findeksPoint
              )
              .subscribe(
                (response) => {
                  if (this.saveCard) {
                    this.save(this.payment);
                  }
                  this.toastrService.success(response.message);
                  this.router.navigate(['']);
                },
                (responseError) => {
                  this.toastrService.error(responseError.error.message);
                  this.router.navigate(['']);
                }
              );
          });
        });
    });
  }

  getRent() {
    this.rent = this.paymentService.getRental();
    if (this.rent != null) {
      this.paymentService
        .getPaymentsByCustomerId(this.rent.customerId)
        .subscribe(
          (response) => {
            this.payments = response.data;
          },
          (responseError) => {}
        );
    } else {
      this.toastrService.error(
        'Bir Şeyler Ters Gitti Yönlendiriliyorsunuz',
        'Hata'
      );
      this.router.navigate(['car-rental', this.carId]);
    }
  }
}
