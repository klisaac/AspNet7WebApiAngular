import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { AuthGuardService } from './core/services/auth-guard.service';
import {LoginComponent} from './views/login/login.component';
import {RegisterComponent} from './views/login/register.component';

import { P404Component } from './views/error/p404.component';
import { P500Component } from './views/error/p500.component';
import { LayoutComponent } from './core/layout/layout.component';
import { AboutComponent } from './views/about/about.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full', },
  { path: '404', component: P404Component, data: { title: 'Page 404' } },
  { path: '500', component: P500Component, data: { title: 'Page 500' } },
  { path: 'login', component: LoginComponent, data: { title: 'Login' } },
  { path: 'register', component: RegisterComponent, data: { title: 'Register new user' } },
  {
    path: '', component: LayoutComponent, data: { title: '' },
    children: [
      { path: '',  redirectTo: 'dashboard', pathMatch:'full' },
      { path: 'dashboard', loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'product', loadChildren: () => import('./views/product/product.module').then(m => m.ProductModule) },
      { path: 'category', loadChildren: () => import('./views/category/category.module').then(m => m.CategoryModule) },
      { path: 'customer', loadChildren: () => import('./views/customer/customer.module').then(m => m.CustomerModule) },
      { path: 'about', component: AboutComponent, data: { title: 'About' } }
    ],
    canActivateChild: [AuthGuardService],
    canActivate: [AuthGuardService]
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

