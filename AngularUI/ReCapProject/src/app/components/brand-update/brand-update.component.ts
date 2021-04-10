import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Brand } from 'src/app/models/brand';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-update',
  templateUrl: './brand-update.component.html',
  styleUrls: ['./brand-update.component.css'],
})
export class BrandUpdateComponent implements OnInit {
  brandUpdateForm: FormGroup;
  brand: Brand;
  dataLoaded: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private brandService: BrandService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params['brandId']) {
        this.brandService.getById(params['brandId']).subscribe((response) => {
          this.brand = response.data;
          this.dataLoaded = true;
          this.createBrandUpdateForm();
        });
      }
    });
  }

  createBrandUpdateForm() {
    this.brandUpdateForm = this.formBuilder.group({
      brandName: [this.brand.brandName, Validators.required],
    });
  }

  update() {
    if (this.brandUpdateForm.valid) {
      let brandModel = Object.assign({}, this.brandUpdateForm.value);
      brandModel.id = this.brand.id;
      console.log(brandModel);
      this.brandService.update(brandModel).subscribe(
        (response) => {
          this.toastrService.success(response.message, 'Başarılı');
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
              'Bir Şeyler Ters Gitti İşlem Başarısız Yönlendiriliyorsunuz',
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

  delete() {
    this.brandService.delete(this.brand).subscribe(
      (response) => {
        this.toastrService.success(response.message, 'İşlem Başarılı');
        this.router.navigate(['']);
      },
      (responseError) => {
        this.toastrService.error(
          'Bir Şeyler Ters Gitti İşlem Başarısız Yönlendiriliyorsunuz',
          'Hata'
        );
        this.router.navigate(['']);
      }
    );
  }
}
