import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ValidationService } from 'src/app/core/services/validation.service';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  templateUrl: 'register.component.html'
})
export class RegisterComponent  implements OnInit{
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private validationService: ValidationService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router) {
    this.initializeForm();
   }
   ngOnInit() {
    this.authService.logout();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      userName: ['', [this.validationService.userName]],
      password: ['', [this.validationService.password]],
      confirmPassword: ['', [this.validationService.password]],
    });
  }

  register() {
    const formValue = this.registerForm.value;
    if (formValue.userName && formValue.password) {
      this.authService.register(formValue.userName, formValue.password, formValue.confirmPassword)
        .subscribe(() => {
          this.router.navigate(['/login']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/login']);
  }
}
