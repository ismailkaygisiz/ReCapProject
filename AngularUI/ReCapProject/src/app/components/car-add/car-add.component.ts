import { Component, OnInit } from '@angular/core';
import { CarImageService } from 'src/app/services/car-image.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CarService } from 'src/app/services/car.service';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carImage';
import { BrandService } from 'src/app/services/brand.service';
import { ColorService } from 'src/app/services/color.service';
import { Brand } from 'src/app/models/brand';
import { Color } from 'src/app/models/color';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-add',
  templateUrl: './car-add.component.html',
  styleUrls: ['./car-add.component.css'],
})
export class CarAddComponent implements OnInit {
  carAddForm: FormGroup;
  brands: Brand[];
  colors: Color[];
  file: File;

  constructor(
    private carService: CarService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private brandService: BrandService,
    private colorService: ColorService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getBrands();
    this.getColors();
    this.createCarAddForm();
  }

  // RESİM EKLEME İŞLEMİ YAPILACAK

  createCarAddForm() {
    this.carAddForm = this.formBuilder.group({
      description: ['', Validators.required],
      colorId: ['', Validators.required],
      brandId: ['', Validators.required],
      modelYear: ['', Validators.required],
      dailyPrice: ['', Validators.required],
    });
  }

  add() {
    if (this.carAddForm.valid) {
      let carModel = Object.assign({}, this.carAddForm.value);
      carModel.brandId = +carModel.brandId;
      carModel.colorId = +carModel.colorId;

      this.carService.add(carModel).subscribe(
        (response) => {
          this.toastrService.success('Araç Ekleme Başarılı', 'Başarılı');
          this.router.navigate(['']);
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
              'Bir Şeyler Ters Gitti Yönlendiriliyorsunuz',
              'Hata'
            );
            this.router.navigate(['']);
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
