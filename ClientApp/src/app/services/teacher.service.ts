import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ChangePasswordRequest } from '../models/Request/ChangePasswordRequest';
import { ChangePasswordResult } from '../models/Response/ChangePasswordResult';
import { PaginatedResult } from '../models/Response/Common/PaginatedResult';
import { Teacher } from '../models/Response/Teacher';
import { TeacherFilter } from '../models/Request/Filters/TeacherFilter';
import { TeacherFullName } from '../models/Response/Common/TeacherFullName';

@Injectable({
  providedIn: 'root',
})
export class TeacherService {
  private url = environment.apiUrl + 'teacher';

  constructor(private http: HttpClient) {}

  getWithPagination(
    pageNumber: number,
    pageSize: number,
    filter: TeacherFilter
  ): Observable<PaginatedResult<Teacher[]>> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', pageNumber);
    httpParams = httpParams.append('pageSize', pageSize);
    httpParams = httpParams.append('sortDirection', filter.sortDirection);

    if (filter.surname !== null && filter.surname !== '')
      httpParams = httpParams.append('surname', filter.surname);

    if (filter.departmentId !== null)
      httpParams = httpParams.append('departmentId', filter.departmentId);

    if (filter.positionId !== null)
      httpParams = httpParams.append('positionId', filter.positionId);

    if (filter.propertyName !== null && filter.propertyName.length !== 0)
      httpParams = httpParams.append('propertyName', filter.propertyName);

    return this.http.get<PaginatedResult<Teacher[]>>(this.url + '/pagination', {
      params: httpParams,
      withCredentials: true,
    });
  }

  changeTeacherPassword(id: string): Observable<ChangePasswordResult> {
    let request: ChangePasswordRequest = {
      id: id,
    };

    return this.http.post<ChangePasswordResult>(
      this.url + '/changeTeacherPassword',
      request
    );
  }

  public getTeachersByDepartmentId(
    departmentId: number
  ): Observable<TeacherFullName[]> {
    return this.http.get<TeacherFullName[]>(
      this.url + `/byDepartment/${departmentId}`
    );
  }

  public getTeachersFullNameByDisciplineId(
    disciplineId: number
  ): Observable<TeacherFullName[]> {
    return this.http.get<TeacherFullName[]>(
      this.url + `/byDiscipline/${disciplineId}`
    );
  }
}
