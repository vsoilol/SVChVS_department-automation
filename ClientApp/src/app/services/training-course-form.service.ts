import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddLessonsToTrainingCourseFormRequest } from '../models/Request/AddLessonsToTrainingCourseFormRequest';
import { TrainingCourseForm } from '../models/Response/TrainingCourseForm';

@Injectable({
  providedIn: 'root',
})
export class TrainingCourseFormService {
  private url = environment.apiUrl + 'trainingCourseForm';

  constructor(private http: HttpClient) {}

  public getAllByProgramId(
    programId: number
  ): Observable<TrainingCourseForm[]> {
    return this.http.get<TrainingCourseForm[]>(
      this.url + `/byProgram/${programId}`
    );
  }

  public getAllWithoutLessons(
    programId: number
  ): Observable<TrainingCourseForm[]> {
    return this.http.get<TrainingCourseForm[]>(
      this.url + `/withoutLessons/${programId}`
    );
  }

  public addLessonsToTrainingCourseForm(
    request: AddLessonsToTrainingCourseFormRequest
  ): Observable<void> {
    return this.http.post<void>(this.url + `/addLessons`, request);
  }

  public deleteLessonsFromTrainingCourseForm(
    programId: number,
    trainingCourseFormId: number
  ): Observable<void> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('educationalProgramId', programId);
    httpParams = httpParams.append(
      'trainingCourseFormId',
      trainingCourseFormId
    );

    return this.http.delete<void>(this.url + `/deleteLessons`, {
      params: httpParams,
      withCredentials: true,
    });
  }
}
