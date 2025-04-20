import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: 'home',
        loadComponent: () => import('./components/home/home.component').then((c) => c.HomeComponent)
    },
    {
        path: 'auth',
        loadComponent: () => import('./components/auth/auth.component').then((c) => c.AuthComponent)
    },
    {
        path: 'post',
        loadComponent: () => import('./components/post/post.component').then((c) => c.PostComponent)
    }
];
