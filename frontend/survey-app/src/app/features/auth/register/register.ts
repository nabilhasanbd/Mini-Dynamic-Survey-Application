import { Component, inject, signal } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

function passwordMatch(control: AbstractControl) {
  const pass    = control.get('password')?.value;
  const confirm = control.get('confirmPassword')?.value;
  return pass === confirm ? null : { passwordMismatch: true };
}

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {
  private fb   = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);

  error   = signal('');
  success = signal('');
  loading = signal(false);

  form = this.fb.group({
    email:           ['', [Validators.required, Validators.email]],
    password:        ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', Validators.required]
  }, { validators: passwordMatch });

  f(name: string) { return this.form.get(name); }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading.set(true);
    this.error.set('');
    this.auth.register(this.form.value as any).subscribe({
      next: () => {
        this.success.set('Account created! Redirecting to login…');
        setTimeout(() => this.router.navigate(['/login']), 1500);
      },
      error: (err) => {
        this.error.set(err.error?.message ?? 'Registration failed. Email may already be in use.');
        this.loading.set(false);
      }
    });
  }
}
