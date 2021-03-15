import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ValidationService } from 'src/app/core/services/validation.service';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
  returnUrl: string = '';
  loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private validationService: ValidationService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router){
    this.initializeForm();
  }

  ngOnInit() {
    this.authService.logout();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/dashboard';
  }

  initializeForm() {
    this.loginForm = this.formBuilder.group({
      userName: ['', [this.validationService.userName]],
      password: ['', [this.validationService.password]]
    });
  }

  login() {
    const formValue = this.loginForm.value;

    if (formValue.userName && formValue.password) {
      this.authService.login(formValue.userName, formValue.password)
        .subscribe(() => {
          this.router.navigateByUrl(this.returnUrl);
        });
    }
  }

  register() {
    this.router.navigate(['/register']);
  }
}
