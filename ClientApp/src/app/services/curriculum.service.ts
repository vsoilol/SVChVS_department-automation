import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CurriculumsStudyStartingYear } from '../models/Response/CurriculumsStudyStartingYear';

@Injectable({
  providedIn: 'root',
})
export class CurriculumService {
  private url = environment.apiUrl + 'curriculum';

  constructor(private http: HttpClient) {}

  public getAllYearsByDepartmentId(
    departmentId: number
  ): Observable<CurriculumsStudyStartingYear[]> {
    return this.http.get<CurriculumsStudyStartingYear[]>(
      this.url + `/years/${departmentId}`
    );
  }
}
