import { Component, inject, signal } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private fb   = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);

  error   = signal('');
  loading = signal(false);

  form = this.fb.group({
    email:    ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  f(name: string) { return this.form.get(name); }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading.set(true);
    this.error.set('');
    this.auth.login(this.form.value as any).subscribe({
      next: () => this.router.navigate(['/admin/surveys']),
      error: (err) => {
        this.error.set(err.status === 401 ? 'Invalid email or password.' : 'Login failed. Please try again.');
        this.loading.set(false);
      }
    });
  }
}
