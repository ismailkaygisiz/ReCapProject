import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  localStorage: Storage;

  constructor() {
    this.localStorage = window.localStorage;
  }

  getItem(key: string) {
    return this.localStorage.getItem(key);
  }

  setItem(key: string, value: string) {
    this.localStorage.setItem(key, value);
  }

  removeItem(key: string) {
    this.localStorage.removeItem(key);
  }

  clearAll() {
    localStorage.clear();
  }
}
