import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
})
export class LoginPageComponent {
  fb = inject(FormBuilder);
  authService = inject(AuthService);
  router = inject(Router);
  apiErrors: string[] = [];

  form = this.fb.nonNullable.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  onSubmit() {
    this.authService.loginUser(this.form.getRawValue()).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.accessToken);
        this.authService.setTokens(response);
        this.router.navigateByUrl('/home');
      },
      error: (error) => {
        if (error.status === 400 && error.error?.errors) {
          const errorObj = error.error.errors
          this.apiErrors = Object.values(errorObj);
        } else {
          this.apiErrors = ['Ett oväntat fel inträffade.'];
        }
      }
    });
  }
}
