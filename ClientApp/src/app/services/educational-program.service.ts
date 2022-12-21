import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ChangeProgramStatusRequest } from '../models/Request/ChangeProgramStatusRequest';
import { UpdateAudiences } from '../models/Request/UpdateAudiences';
import { PaginatedResult } from '../models/Response/Common/PaginatedResult';
import { EducationalProgram } from '../models/Response/EducationalProgram';
import { EducationalProgramBriefInfo } from '../models/Response/EducationalProgramBriefInfo';
import { EducationalProgramsFilter } from '../models/Request/Filters/EducationalProgramsFilter';

@Injectable({
  providedIn: 'root',
})
export class EducationalProgramService {
  private url = environment.apiUrl + 'educationalProgram';

  constructor(private http: HttpClient) {}

  getById(id: number): Observable<EducationalProgram> {
    return this.http.get<EducationalProgram>(this.url + `/${id}`);
  }

  update(educationalProgram: EducationalProgram): Observable<void> {
    return this.http.put<void>(this.url, educationalProgram);
  }

  getWordDocument(id: number): Observable<Blob> {
    return this.http.get(this.url + `/wordDocument/${id}`, {
      responseType: 'blob',
    });
  }

  updateAudiencesInfo(request: UpdateAudiences): Observable<void> {
    return this.http.put<void>(this.url + `/audiences`, request);
  }

  changeStatus(request: ChangeProgramStatusRequest): Observable<void> {
    return this.http.put<void>(this.url + '/changeStatus', request);
  }

  getWithPagination(
    pageNumber: number,
    pageSize: number,
    filter: EducationalProgramsFilter
  ): Observable<PaginatedResult<EducationalProgramBriefInfo[]>> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', pageNumber);
    httpParams = httpParams.append('pageSize', pageSize);

    httpParams = httpParams.append('disciplineName', filter.disciplineName);
    httpParams = httpParams.append('sortDirection', filter.sortDirection);
    httpParams = httpParams.append('userId', filter.userId);

    if (filter.propertyName !== null && filter.propertyName.length !== 0)
      httpParams = httpParams.append('propertyName', filter.propertyName);

    return this.http.get<PaginatedResult<EducationalProgramBriefInfo[]>>(
      this.url + '/pagination',
      {
        params: httpParams,
        withCredentials: true,
      }
    );
  }

  createDefaultProgram(disciplineId: number): Observable<void> {
    return this.http.post<void>(this.url + `/default/${disciplineId}`, null);
  }
}
