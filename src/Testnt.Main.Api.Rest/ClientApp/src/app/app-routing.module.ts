import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectComponent } from './project/project.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthorizationGuard } from './authorization.guard';
import { AutoLoginComponent } from './auto-login/auto-login.component';


const routes: Routes = [
  { path: "", component: DashboardComponent, pathMatch: 'full'},
  { path: "dashboard", component: DashboardComponent},
  { path: 'project', loadChildren: () => import('./project/project.module').then(m => m.ProjectModule)},
  { path: 'autologin', component: AutoLoginComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
