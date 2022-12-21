import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Department } from '../models/Response/Department';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private url = environment.apiUrl + 'department';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Department[]> {
    return this.http.get<Department[]>(this.url);
  }
}
