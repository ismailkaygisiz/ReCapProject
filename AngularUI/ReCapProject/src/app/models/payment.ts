export interface Payment {
  id: number;
  customerId: number;
  firstName: string;
  lastName: string;
  cardNumber: string;
  year: number;
  month: number;
  cvv: number;
}
