import { CarImage } from './carImage';

export interface Car {
  id: number;
  description: string;
  brandName: string;
  colorName: string;
  dailyPrice: number;
  modelYear: number;
  imagePaths: CarImage[];
  imagePath: CarImage;
  findeksPoint: number;
}
