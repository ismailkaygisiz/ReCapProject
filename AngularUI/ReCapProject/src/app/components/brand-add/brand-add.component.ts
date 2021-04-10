import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-add',
  templateUrl: './brand-add.component.html',
  styleUrls: ['./brand-add.component.css'],
})
export class BrandAddComponent implements OnInit {
  brandAddForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private brandService: BrandService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createBrandAddForm();
  }

  createBrandAddForm() {
    this.brandAddForm = this.formBuilder.group({
      brandName: ['', Validators.required],
    });
  }

  add() {
    if (this.brandAddForm.valid) {
      let brandModel = Object.assign({}, this.brandAddForm.value);

      this.brandService.add(brandModel).subscribe(
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
}
