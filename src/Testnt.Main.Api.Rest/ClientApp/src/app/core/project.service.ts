import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListObject } from '../common/model/ListObject';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  getProjects(): Observable<ListObject<Project>> {
    return this.http.get<ListObject<Project>>('http://localhost:7000/api/project')
  }

  getProject(id:string): Observable<Project> {
    return this.http.get<Project>(`http://localhost:7000/api/project/${id}`)
  }

  createProject(project: Project) {
    return this.http.post('http://localhost:7000/api/project', project);
  }

  deleteProject(id:string){
    return this.http.delete(`http://localhost:7000/api/project/${id}`);
  }
}

export class Project {
  name: string
  id: string
  isEnabled: boolean = false;
}