import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandAddComponent } from './components/brand-add/brand-add.component';
import { BrandListComponent } from './components/brand-list/brand-list.component';
import { BrandUpdateComponent } from './components/brand-update/brand-update.component';
import { CarAddComponent } from './components/car-add/car-add.component';
import { CarDetailComponent } from './components/car-detail/car-detail.component';
import { CarImageUpdateComponent } from './components/car-image-update/car-image-update.component';
import { CarRentalComponent } from './components/car-rental/car-rental.component';
import { CarUpdateComponent } from './components/car-update/car-update.component';
import { CarComponent } from './components/car/car.component';
import { ColorAddComponent } from './components/color-add/color-add.component';
import { ColorListComponent } from './components/color-list/color-list.component';
import { ColorUpdateComponent } from './components/color-update/color-update.component';
import { CustomerComponent } from './components/customer/customer.component';
import { LoginComponent } from './components/login/login.component';
import { PaymentComponent } from './components/payment/payment.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { RentalComponent } from './components/rental/rental.component';
import { LoginDisableGuard } from './guards/login-disable.guard';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: CarComponent },

  { path: 'cars/list', component: CarComponent },
  { path: 'cars/brand/:brandId', component: CarComponent },
  { path: 'cars/color/:colorId', component: CarComponent },
  { path: 'cars/:brand/:color', component: CarComponent },

  { path: 'cars/:carId', component: CarDetailComponent },

  {
    path: 'car-rental/:car',
    component: CarRentalComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'car-rental/:carIdForPay/pay',
    component: PaymentComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'add/brand-add',
    component: BrandAddComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'add/color-add',
    component: ColorAddComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'add/car-add',
    component: CarAddComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'update/car/:carId',
    component: CarUpdateComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'brands/list',
    component: BrandListComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'brands/:brandId',
    component: BrandUpdateComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'colors/list',
    component: ColorListComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'colors/:colorId',
    component: ColorUpdateComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'update/car/:carId/images/:carImageId',
    component: CarImageUpdateComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'auth/login',
    component: LoginComponent,
    canActivate: [LoginDisableGuard],
  },

  {
    path: 'auth/register',
    component: RegisterComponent,
    canActivate: [LoginDisableGuard],
  },

  {
    path: 'user/:userId',
    component: ProfileComponent,
    canActivate: [LoginGuard],
  },

  {
    path: 'rentals/list',
    component: RentalComponent,
  },
  {
    path: 'customers/list',
    component: CustomerComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
