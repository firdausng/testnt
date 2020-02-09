import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { ProjectService, Project } from 'src/app/project.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'tnt-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss']
})
export class ProjectDetailComponent implements OnInit {

  project$: Observable<Project>

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService
  ) { }

  ngOnInit(): void {
    this.project$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.projectService.getProject(params.get('id')))
    );
    
  }

}
