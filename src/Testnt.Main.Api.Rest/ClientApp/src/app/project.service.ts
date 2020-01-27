import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ListObject } from './common/model/ListObject';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http:HttpClient) { }

  getProjects(): Observable<ListObject<Project>>{
    return this.http.get<ListObject<Project>>('http://localhost:7000/api/project')
  }
}

export class Project {

}