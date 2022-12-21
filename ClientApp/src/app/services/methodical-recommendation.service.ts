import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateMethodicalRecommendationRequest } from '../models/Request/CreateMethodicalRecommendationRequest';
import { MethodicalRecommendation } from '../models/Response/MethodicalRecommendation';

@Injectable({
  providedIn: 'root',
})
export class MethodicalRecommendationService {
  private url = environment.apiUrl + 'methodicalRecommendation';

  constructor(private http: HttpClient) {}

  getMethodicalRecommendationByProgramId(
    educationalProgramId: number
  ): Observable<MethodicalRecommendation[]> {
    return this.http.get<MethodicalRecommendation[]>(
      this.url + `/byProgram/${educationalProgramId}`
    );
  }

  public createMethodicalRecommendation(
    request: CreateMethodicalRecommendationRequest
  ): Observable<void> {
    return this.http.post<void>(this.url, request);
  }

  public deleteMethodicalRecommendation(
    methodicalRecommendationId: number
  ): Observable<void> {
    return this.http.delete<void>(this.url + `/${methodicalRecommendationId}`);
  }

  public updateMethodicalRecommendation(
    methodicalRecommendation: MethodicalRecommendation
  ): Observable<void> {
    return this.http.put<void>(this.url, methodicalRecommendation);
  }
}
