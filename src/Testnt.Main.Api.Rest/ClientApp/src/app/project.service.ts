import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ListObject } from './common/model/ListObject';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  getProjects(): Observable<ListObject<Project>> {
    return this.http.get<ListObject<Project>>('http://localhost:7000/api/project')
  }

  createProject(name: string) {
    return this.http.post('http://localhost:7000/api/project', { name });
  }

  deleteProject(id:string){
    return this.http.delete(`http://localhost:7000/api/project/${id}`);
  }
}

export class Project {
  name: string
  id: string
}