import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carImage';
import { CarImageService } from 'src/app/services/car-image.service';
import { CarService } from 'src/app/services/car.service';
import { imageUrl } from 'src/api';

@Component({
  selector: 'app-car-image-update',
  templateUrl: './car-image-update.component.html',
  styleUrls: ['./car-image-update.component.css'],
})
export class CarImageUpdateComponent implements OnInit {
  url = imageUrl;

  file: File;
  carImageUpdateForm: FormGroup;
  car: Car;
  carImage: CarImage;
  dataLoaded: boolean = false;

  constructor(
    private carImageService: CarImageService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private carService: CarService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params['carId'] && params['carImageId']) {
        this.carImageService
          .getById(params['carImageId'])
          .subscribe((response) => {
            this.carService
              .getCarById(params['carId'])
              .subscribe((responseCar) => {
                this.car = responseCar.data;
              });

            this.carImage = response.data;
            this.dataLoaded = true;
          });
      }
    });
  }

  handleFileInput(event: any) {
    this.file = event.target.files.item(0);
  }

  update() {
    if (this.file != null) {
      this.carImage = {
        id: this.carImage.id,
        carId: this.car.id,
        date: null,
        imagePath: this.carImage.imagePath,
      };

      this.carImageService.update(this.carImage, this.file).subscribe(
        (responseCarImage) => {
          this.toastrService.success('Araç Güncelleme Başarılı', 'Başarılı');
          this.router.navigate(['/update/car', this.car.id]);
        },
        (responseError) => {
          console.log(this.carImage);
          console.log(this.file);
          this.toastrService.error(responseError.error.message, 'Hata');
        }
      );
    } else {
      this.toastrService.error('Resim Boş Olamaz', 'Lütfen Resim Seçin');
    }
  }

  delete() {
    if (this.car.imagePaths.length > 1) {
      let image = {
        id: this.carImage.id,
        carId: this.carImage.carId,
        date: this.carImage.date,
        imagePath: this.carImage.imagePath,
      };
      this.carImageService.delete(image).subscribe((response) => {
        this.toastrService.error(response.message, 'Silindi');
        this.router.navigate(['/update/car', this.car.id]);
      });
    } else {
      this.toastrService.warning(
        'Aracınızın En Az Bir Adet Resmi Bulunmalıdır Resmi Güncellemeyi Deneyin',
        'Uyarı'
      );
    }
  }
}
