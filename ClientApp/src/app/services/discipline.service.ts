import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/Response/Common/PaginatedResult';
import { DepartmentHeadDiscipline } from '../models/Response/DepartmentHeadDiscipline';
import { DisciplineBriefInfo } from '../models/Response/DisciplineBriefInfo';
import { TeacherFilter } from '../models/Request/Filters/TeacherFilter';
import { Teacher } from '../models/Response/Teacher';
import { DisciplinesFilter } from '../models/Request/Filters/DisciplinesFilter';
import { AdditionalDisciplineInfo } from '../models/Response/AdditionalDisciplineInfo';
import { UpdateDisciplineTeachersRequest } from '../models/Request/UpdateDisciplineTeachersRequest';
import { ChangeDisciplineStatusRequest } from '../models/Request/ChangeDisciplineStatusRequest';

@Injectable({
  providedIn: 'root',
})
export class DisciplineService {
  private url = environment.apiUrl + 'discipline';

  constructor(private http: HttpClient) {}

  getBriefInfo(id: number): Observable<DisciplineBriefInfo> {
    return this.http.get<DisciplineBriefInfo>(this.url + `/brief/${id}`);
  }

  getByDepartmetIdWithPagination(
    pageNumber: number,
    pageSize: number,
    filter: DisciplinesFilter
  ): Observable<PaginatedResult<DepartmentHeadDiscipline[]>> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', pageNumber);
    httpParams = httpParams.append('pageSize', pageSize);
    httpParams = httpParams.append('sortDirection', filter.sortDirection);

    if (filter.disciplineName !== null && filter.disciplineName !== '')
      httpParams = httpParams.append('disciplineName', filter.disciplineName);

    if (filter.studyStartingYear !== null)
      httpParams = httpParams.append(
        'studyStartingYear',
        filter.studyStartingYear
      );

    httpParams = httpParams.append('departmentId', filter.departmentId);

    if (filter.status !== null)
      httpParams = httpParams.append('status', filter.status);

    if (filter.propertyName !== null && filter.propertyName.length !== 0)
      httpParams = httpParams.append('propertyName', filter.propertyName);

    return this.http.get<PaginatedResult<DepartmentHeadDiscipline[]>>(
      this.url + '/pagination',
      {
        params: httpParams,
        withCredentials: true,
      }
    );
  }

  public getAdditionalDisciplineInfoById(
    disciplineId: number
  ): Observable<AdditionalDisciplineInfo> {
    return this.http.get<AdditionalDisciplineInfo>(
      this.url + `/additional/${disciplineId}`
    );
  }

  public updateDisciplineTeachers(
    request: UpdateDisciplineTeachersRequest
  ): Observable<void> {
    return this.http.put<void>(this.url + '/teachers', request);
  }

  public changeStatus(
    request: ChangeDisciplineStatusRequest
  ): Observable<void> {
    return this.http.put<void>(this.url + '/changeStatus', request);
  }

  public isEducationalProgramExist(id: number): Observable<boolean> {
    return this.http.get<boolean>(this.url + `/isProgramExist/${id}`);
  }
}
