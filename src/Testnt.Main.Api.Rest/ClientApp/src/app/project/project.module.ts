import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent, NewProjectDialog } from './project.component';
import { ProjectService } from '../project.service';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../authorization.guard';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';


const routes: Routes = [
  { path: "", component: ProjectComponent, canActivate: [AuthorizationGuard]},
];



let components = [
  ProjectComponent,
  NewProjectDialog
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
