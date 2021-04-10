import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Color } from 'src/app/models/color';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-color-update',
  templateUrl: './color-update.component.html',
  styleUrls: ['./color-update.component.css'],
})
export class ColorUpdateComponent implements OnInit {
  colorUpdateForm: FormGroup;
  color: Color;
  dataLoaded: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private colorServie: ColorService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params['colorId']) {
        this.colorServie.getById(params['colorId']).subscribe((response) => {
          this.color = response.data;
          this.dataLoaded = true;
          this.createColorUpdateForm();
        });
      }
    });
  }

  createColorUpdateForm() {
    this.colorUpdateForm = this.formBuilder.group({
      colorName: [this.color.colorName, Validators.required],
    });
  }

  update() {
    if (this.colorUpdateForm.valid) {
      let colorModel = Object.assign({}, this.colorUpdateForm.value);
      colorModel.id = this.color.id;
      console.log(colorModel);
      this.colorServie.update(colorModel).subscribe(
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
    this.colorServie.delete(this.color).subscribe(
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
