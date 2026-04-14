import { Component } from '@angular/core';

@Component({
  selector: 'app-spinner',
  template: `<div class="spinner-wrap"><div class="spinner"></div></div>`,
  styles: [`
    .spinner-wrap { display:flex; justify-content:center; padding:3rem; }
    .spinner {
      width:40px; height:40px;
      border:3px solid var(--border);
      border-top-color: var(--primary);
      border-radius:50%;
      animation: spin .7s linear infinite;
    }
    @keyframes spin { to { transform:rotate(360deg); } }
  `]
})
export class Spinner {}
