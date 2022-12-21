import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Competence } from '../models/Response/Competence';

@Injectable({
  providedIn: 'root',
})
export class CompetenceService {
  private url = environment.apiUrl + 'competence';

  constructor(private http: HttpClient) {}

  public getByLessonId(lessonId: number): Observable<Competence[]> {
    return this.http.get<Competence[]>(this.url + `/byLesson/${lessonId}`);
  }

  public getAllByProgramId(
    educationalProgramId: number
  ): Observable<Competence[]> {
    return this.http.get<Competence[]>(this.url + `/${educationalProgramId}`);
  }
}
