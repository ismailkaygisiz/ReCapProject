import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Brand } from 'src/app/models/brand';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carImage';
import { Color } from 'src/app/models/color';
import { BrandService } from 'src/app/services/brand.service';
import { CarImageService } from 'src/app/services/car-image.service';
import { CarService } from 'src/app/services/car.service';
import { ColorService } from 'src/app/services/color.service';
import { imageUrl } from 'src/api';

@Component({
  selector: 'app-car-update',
  templateUrl: './car-update.component.html',
  styleUrls: ['./car-update.component.css'],
})
export class CarUpdateComponent implements OnInit {
  url = imageUrl;

  file: File = null;
  carUpdateForm: FormGroup;
  brands: Brand[];
  colors: Color[];
  car: Car;
  brandId: number;
  colorId: number;
  dataLoaded: boolean = false;
  carImages: CarImage[] = [];
  carImage: CarImage;

  constructor(
    private carService: CarService,
    private carImageService: CarImageService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private brandService: BrandService,
    private colorService: ColorService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.getBrands();
      this.getColors();
      if (params['carId']) {
        this.carService.getCarById(params['carId']).subscribe((response) => {
          this.car = response.data;
          this.car.imagePath = this.car.imagePaths[0];
          this.car.imagePaths.forEach((c) => {
            if (c != null && c != this.car.imagePath) {
              this.carImages.push(c);
            }
          });

          this.brandService
            .getByName(this.car.brandName)
            .subscribe((responseBrand) => {
              this.brandId = responseBrand.data.id;
              this.createCarUpdateForm();
            });
          this.colorService
            .getByName(this.car.colorName)
            .subscribe((responseColor) => {
              this.colorId = responseColor.data.id;

              this.dataLoaded = true;
              this.createCarUpdateForm();
            });
        });
      }
    });
  }

  // RESİM EKLEME İŞLEMİ YAPILACAK
  handleFileInput(event: any) {
    this.file = event.target.files.item(0);
  }

  createCarUpdateForm() {
    this.carUpdateForm = this.formBuilder.group({
      description: [this.car.description, Validators.required],
      colorId: [this.colorId, Validators.required],
      brandId: [this.brandId, Validators.required],
      modelYear: [this.car.modelYear, Validators.required],
      dailyPrice: [this.car.dailyPrice, Validators.required],
    });
  }

  update() {
    if (this.carUpdateForm.valid) {
      let carModel = Object.assign({}, this.carUpdateForm.value);
      carModel.brandId = +carModel.brandId;
      carModel.colorId = +carModel.colorId;
      carModel.id = +this.car.id;

      this.carService.update(carModel).subscribe(
        (response) => {
          this.carImage = {
            id: null,
            carId: carModel.id,
            date: null,
            imagePath: null,
          };
          if (this.file != null) {
            this.carImageService.add(this.carImage, this.file).subscribe(
              (responseCarImage) => {
                this.toastrService.success(
                  'Araç Güncelleme Başarılı',
                  'Başarılı'
                );
                this.router.navigate(['cars', this.car.id]);
              },
              (responseError) => {
                console.log(this.file);
                this.toastrService.error(responseError.error.message, 'Hata');
              }
            );
          } else if (this.file == null && this.car.imagePaths.length == 0) {
            this.carImageService.add(this.carImage, this.file).subscribe(
              (responseCarImage) => {
                this.toastrService.success(
                  'Araç Güncelleme Başarılı',
                  'Başarılı'
                );
                this.router.navigate(['cars', this.car.id]);
              },
              (responseError) => {
                this.toastrService.error(responseError.error.message, 'Hata');
              }
            );
          } else {
            this.toastrService.success('Araç Güncelleme Başarılı', 'Başarılı');
            this.router.navigate(['cars', this.car.id]);
          }
        },
        (responseError) => {
          if (responseError.error.Errors != null) {
            for (let i = 0; i < responseError.error.Errors.length; i++) {
              this.toastrService.error(
                responseError.error.Errors[i].ErrorMessage,
                'Doğrulama Hatası'
              );
            }
          } else {
            this.toastrService.error(
              'Bir Şeyler Ters Gitti İşlem Başarısız Yönlendiriliyorsunuz',
              'Hata'
            );
            this.router.navigate(['cars', this.car.id]);
          }
        }
      );
    } else {
      this.toastrService.error('Anlaşılan Formunuz Eksik', 'Form Eksik');
    }
  }

  getColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response.data;
    });
  }

  getBrands() {
    this.brandService.getBrands().subscribe((response) => {
      this.brands = response.data;
    });
  }
}
