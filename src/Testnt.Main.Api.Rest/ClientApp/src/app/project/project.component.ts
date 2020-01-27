import { Component, OnInit } from '@angular/core';
import { ProjectService, Project } from '../project.service';
import { ListObject } from '../common/model/ListObject';

@Component({
  selector: 'tnt-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  data: ListObject<Project>;

  constructor(public projectService: ProjectService) { }

  ngOnInit() {
    this.projectService.getProjects()
      .subscribe(
        data => this.data = data,
        err => console.log(err)
      );
  }


}
