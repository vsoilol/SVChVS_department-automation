import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateLiteratureRequest } from '../models/Request/CreateLiteratureRequest';
import { GetLiteraturesByTypeRequest } from '../models/Request/GetLiteraturesByTypeRequest';
import { UpdateLiteratureRequest } from '../models/Request/UpdateLiteratureRequest';
import { Literature } from '../models/Response/Literature';

@Injectable({
  providedIn: 'root',
})
export class LiteratureService {
  private url = environment.apiUrl + 'literature';

  constructor(private http: HttpClient) {}

  public getLiteraturesByType(
    request: GetLiteraturesByTypeRequest
  ): Observable<Literature[]> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append(
      'educationalProgramId',
      request.educationalProgramId
    );
    httpParams = httpParams.append('literatureType', request.literatureType);

    return this.http.get<Literature[]>(this.url + `/byType`, {
      params: httpParams,
      withCredentials: true,
    });
  }

  public createLiterature(request: CreateLiteratureRequest): Observable<void> {
    return this.http.post<void>(this.url, request);
  }

  public deleteLiterature(literatureId: number): Observable<void> {
    return this.http.delete<void>(this.url + `/${literatureId}`);
  }

  public updateLiterature(request: UpdateLiteratureRequest): Observable<void> {
    return this.http.put<void>(this.url, request);
  }
}
