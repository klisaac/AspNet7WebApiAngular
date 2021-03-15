import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable()
export class ValidationService {

  getValidatorErrorMessage(name: string, value?: any) {
    const config: {[key: string]:any} = {
        'required': 'Required',
        'invalidUserName': 'Invalid user name',
        'invalidEmailAddress': 'Invalid email address',
        'invalidPassword': 'Password has to be aplhanumeric, cannout start with a digit, underscore or special character and must contain at least a digit.',
        'minlength': `Minimum length ${value.requiredLength}`,
        'maxlength': `Maximum length ${value.requiredLength}`
    };
    return config[name];
  }

  userName(control: AbstractControl) {
    if (control.value.match(/^[a-zA-Z0-9\s'-]+$/)) {
        return null;
    } else {
        return { 'invalidUserName': true };
    }
  }


  email(control: AbstractControl) {
      if (control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
          return null;
      } else {
          return { 'invalidEmailAddress': true };
      }
  }

  password(control: AbstractControl) {
      if (control.value.match(/^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}/)) {
          return null;
      } else {
          return { 'invalidPassword': true };
      }
  }

  confirmPassword(control: AbstractControl) {
    if (control.value.match(/^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}/)) {
        return null;
    } else {
        return { 'invalidPassword': true };
    }
  }  
}
