import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetWeeksByModuleNumberRequest } from '../models/Request/GetWeeksByModuleNumberRequest';
import { UpdateWeekRequest } from '../models/Request/UpdateWeekRequest';
import { Week } from '../models/Response/Week';

@Injectable({
  providedIn: 'root',
})
export class WeekService {
  private url = environment.apiUrl + 'week';

  constructor(private http: HttpClient) {}

  public getTrainingModuleNumbers(programId: number): Observable<number[]> {
    return this.http.get<number[]>(
      this.url + `/getTrainingModuleNumbers/${programId}`
    );
  }

  public getWeeksByModuleNumber(
    request: GetWeeksByModuleNumberRequest
  ): Observable<Week[]> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append(
      'educationalProgramId',
      request.educationalProgramId
    );
    httpParams = httpParams.append('semesterId', request.semesterId);
    httpParams = httpParams.append('moduleNumber', request.moduleNumber);

    return this.http.get<Week[]>(this.url + `/byModuleNumber`, {
      params: httpParams,
      withCredentials: true,
    });
  }

  public updateWeek(request: UpdateWeekRequest): Observable<void> {
    return this.http.put<void>(this.url, request);
  }
}
