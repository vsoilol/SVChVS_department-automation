import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateEvaluationTool } from '../models/Request/CreateEvaluationTool';
import { DeleteEvaluationTool } from '../models/Request/DeleteEvaluationTool';
import { EvaluationTool } from '../models/Response/EvaluationTool';
import { EvaluationToolType } from '../models/Response/EvaluationToolType';

@Injectable({
  providedIn: 'root',
})
export class EvaluationToolService {
  private url = environment.apiUrl + 'evaluationTool';

  constructor(private http: HttpClient) {}

  public getNotChoosenEvaluationTool(
    programId: number
  ): Observable<EvaluationToolType[]> {
    return this.http.get<EvaluationToolType[]>(
      this.url + `/notChoosenEvaluationTool/${programId}`
    );
  }

  public getAllEvaluationToolTypeByProgramId(
    programId: number
  ): Observable<EvaluationToolType[]> {
    return this.http.get<EvaluationToolType[]>(this.url + `/type/${programId}`);
  }

  public getAllByProgramId(programId: number): Observable<EvaluationTool[]> {
    return this.http.get<EvaluationTool[]>(
      this.url + `/byProgramId/${programId}`
    );
  }

  public createEvaluationTool(request: CreateEvaluationTool): Observable<void> {
    return this.http.post<void>(this.url, request);
  }

  public deleteEvaluationTool(request: DeleteEvaluationTool): Observable<void> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append(
      'educationalProgramId',
      request.educationalProgramId
    );
    httpParams = httpParams.append(
      'evaluationToolTypeId',
      request.evaluationToolTypeId
    );

    return this.http.delete<void>(this.url, { params: httpParams });
  }
}
