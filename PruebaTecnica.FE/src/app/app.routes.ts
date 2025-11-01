import { Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard/dashboard.component';
import { authGuard } from './guards/auth.guard';
import { LayoutComponent } from './components/layout/layout.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { TasksComponent } from './components/tasks/tasks/tasks.component';

export const routes: Routes = [
  {
    path: '',
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'tasks', component: TasksComponent },
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: 'dashboard' }
];
