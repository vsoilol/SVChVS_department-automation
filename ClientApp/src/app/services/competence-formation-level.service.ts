import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UpdateCompetenceFormationLevelRequest } from '../models/Request/UpdateCompetenceFormationLevelRequest';
import { CompetenceFormationLevel } from '../models/Response/CompetenceFormationLevel';

@Injectable({
  providedIn: 'root',
})
export class CompetenceFormationLevelService {
  private url = environment.apiUrl + 'competenceFormationLevel';

  constructor(private http: HttpClient) {}

  public getCompetenceFormationLevelByCompetence(
    educationalProgramId: number,
    competenceId: number
  ): Observable<CompetenceFormationLevel[]> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append(
      'educationalProgramId',
      educationalProgramId
    );
    httpParams = httpParams.append('competenceId', competenceId);

    return this.http.get<CompetenceFormationLevel[]>(
      this.url + `/byCompetence`,
      {
        params: httpParams,
        withCredentials: true,
      }
    );
  }

  public updateCompetenceFormationLevel(
    request: UpdateCompetenceFormationLevelRequest
  ): Observable<void> {
    return this.http.put<void>(this.url, request);
  }
}
