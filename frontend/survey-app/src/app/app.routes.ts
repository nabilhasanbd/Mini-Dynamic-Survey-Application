import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login',    loadComponent: () => import('./features/auth/login/login').then(m => m.Login) },
  { path: 'register', loadComponent: () => import('./features/auth/register/register').then(m => m.Register) },

  { path: 'admin', redirectTo: 'admin/surveys', pathMatch: 'full' },
  {
    path: 'admin/surveys',
    canActivate: [authGuard],
    loadComponent: () => import('./features/admin/survey-list/survey-list').then(m => m.SurveyList)
  },
  {
    path: 'admin/surveys/new',
    canActivate: [authGuard],
    loadComponent: () => import('./features/admin/survey-builder/survey-builder').then(m => m.SurveyBuilder)
  },
  {
    path: 'admin/surveys/:id',
    canActivate: [authGuard],
    loadComponent: () => import('./features/admin/survey-builder/survey-builder').then(m => m.SurveyBuilder)
  },
  {
    path: 'admin/analytics/:id',
    canActivate: [authGuard],
    loadComponent: () => import('./features/admin/analytics/analytics').then(m => m.Analytics)
  },

  { path: 's/:slug', loadComponent: () => import('./features/survey/take-survey/take-survey').then(m => m.TakeSurvey) },

  { path: '**', redirectTo: 'login' }
];
