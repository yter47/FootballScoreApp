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
  selector: 'app-register-page',
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss',
})
export class RegisterPageComponent {
  fb = inject(FormBuilder);

  private authService = inject(AuthService);
  apiErrors: string[] = [];
  router = inject(Router);

  form = this.fb.nonNullable.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    username: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  });

  onSubmit() {
    this.authService.registerUser(this.form.getRawValue()).subscribe({
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
      },
    });
  }
}
