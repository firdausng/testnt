import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectService } from '../project.service';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../authorization.guard';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { ProjectComponent, NewProjectDialog } from './project-list/project.component';


const routes: Routes = [
  { path: '', component: ProjectComponent, canActivate: [AuthorizationGuard] },
  { path: ':id', component: ProjectDetailComponent }
];

let components = [
  ProjectComponent,
  NewProjectDialog,
  ProjectDetailComponent
];

@NgModule({
  declarations: components,
  exports: components,
  entryComponents: [
    NewProjectDialog
  ],
  providers: [
    ProjectService,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class ProjectModule { }
