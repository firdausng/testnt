import { Injectable } from '@angular/core';

@Injectable()
export class AuthService {
  public getToken(): string {
      //authorizationData_testnt.main.spa.client
      return sessionStorage.getItem('authorizationData_testnt.main.spa.client').replace('"', "").replace('"', "");
    //return localStorage.getItem('token');
  }

}