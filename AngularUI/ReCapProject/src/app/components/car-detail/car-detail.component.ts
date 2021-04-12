import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carImage';
import { CarService } from 'src/app/services/car.service';
import { imageUrl } from 'src/api';

@Component({
  selector: 'app-cardetail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.css'],
})
export class CarDetailComponent implements OnInit {
  url = imageUrl;

  car: Car;
  carImages: CarImage[] = [];
  dataLoaded: boolean = false;

  constructor(
    private carService: CarService,
    private activatedRoute: ActivatedRoute,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getCar();
  }

  getCarById(carId: number) {
    this.carService.getCarById(carId).subscribe((response) => {
      this.car = response.data;

      this.car.imagePath = this.car.imagePaths[0];
      this.car.imagePaths.forEach((c) => {
        if (c != null && c != this.car.imagePath) {
          this.carImages.push(c);
        }
      });
      this.dataLoaded = true;
    });
  }

  delete() {
    this.getCar();
    this.carService.delete(this.car).subscribe(
      (response) => {
        this.toastrService.success(response.message, 'İşlem Başarılı');
        this.router.navigate(['admin']);
      },
      (responseError) => {
        if (responseError.error.Errors != null) {
          for (let i = 0; i < responseError.error.Errors.length; i++) {
            this.toastrService.error(
              responseError.error.Errors[i].ErrorMessage
            );
          }
        } else {
          this.toastrService.error(
            'Bir Şeyler Ters Gitti İşlem Başarısız Yönlendiriliyorsunuz',
            'Hata'
          );
          this.router.navigate(['']);
        }
      }
    );
  }

  getCar() {
    this.activatedRoute.params.subscribe((params) => {
      if (params['carId']) {
        this.getCarById(params['carId']);
      }
    });
  }
}
