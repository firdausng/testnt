import { Component, OnInit, Inject } from '@angular/core';
import { ProjectService, Project } from '../project.service';
import { ListObject } from '../common/model/ListObject';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';




@Component({
  selector: 'tnt-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'enabled', 'action'];
  data: ListObject<Project>;
  dataSource: [Project]
  project: Project = new Project();

  constructor(public projectService: ProjectService, public dialog: MatDialog) { }

  ngOnInit() {
    this.getAllProject();
  }

  openCreateNewProjectDialog(): void {
    const dialogRef = this.dialog.open(NewProjectDialog, {
      width: '250px',
      data: this.project
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.project = result;
      if(this.project.name.length > 0){
        this.projectService.createProject(this.project).subscribe(
          data => {
            this.getAllProject();
          },
          err => console.log(err)
        );
      }
      
    });
  }

  getAllProject() {
    this.projectService.getProjects()
      .subscribe(
        data => {
          this.data = data,
            this.dataSource = data.data
        },
        err => console.log(err)
      );
  }

  deleteProject(project: Project) {
    this.projectService.deleteProject(project.id)
      .subscribe(
        data => {
          this.getAllProject();
        },
        err => console.log(err)
      );
  }
}


@Component({
  selector: 'new-project-dialog',
  templateUrl: 'new-project-dialog.html',
})
export class NewProjectDialog {

  constructor(
    public dialogRef: MatDialogRef<NewProjectDialog>,
    @Inject(MAT_DIALOG_DATA) public project: Project) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
