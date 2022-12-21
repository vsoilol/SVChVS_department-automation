import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LessonType } from '../models/Enums/LessonType';
import { CreateLessonRequest } from '../models/Request/CreateLessonRequest';
import { UpdateLessonRequest } from '../models/Request/UpdateLessonRequest';
import { LectureDto } from '../models/Response/Lecture';
import { LecturesBriefInfo } from '../models/Response/LecturesBriefInfo';
import { Lesson } from '../models/Response/Lesson';

@Injectable({
  providedIn: 'root',
})
export class LessonService {
  private url = environment.apiUrl + 'lesson';

  constructor(private http: HttpClient) {}

  public getAllLessonsByProgramId(
    programId: number,
    lessonType: LessonType
  ): Observable<Lesson[]> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('educationalProgramId', programId);
    httpParams = httpParams.append('lessonType', lessonType);

    return this.http.get<Lesson[]>(this.url + `/byProgramId`, {
      params: httpParams,
      withCredentials: true,
    });
  }

  public createLesson(
    createLessonRequest: CreateLessonRequest
  ): Observable<void> {
    return this.http.post<void>(this.url, createLessonRequest);
  }

  public updateLesson(
    updateLessonRequest: UpdateLessonRequest
  ): Observable<void> {
    return this.http.put<void>(this.url, updateLessonRequest);
  }

  public deleteLesson(lessonId: number): Observable<void> {
    return this.http.delete<void>(this.url + `/${lessonId}`);
  }

  public getAllLessonsWithoutWeek(
    programId: number,
    lessonType: LessonType
  ): Observable<Lesson[]> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('educationalProgramId', programId);
    httpParams = httpParams.append('lessonType', lessonType);

    return this.http.get<Lesson[]>(this.url + `/withoutWeek`, {
      params: httpParams,
      withCredentials: true,
    });
  }

  public getAllLecturesByProgramId(
    programId: number
  ): Observable<LecturesBriefInfo[]> {
    return this.http.get<LecturesBriefInfo[]>(
      this.url + `/lectures/${programId}`
    );
  }

  public getLectureById(id: number): Observable<LectureDto> {
    return this.http.get<LectureDto>(this.url + `/lecture/${id}`);
  }

  public getAllLessonsWithoutTrainingCourseForm(
    programId: number,
    lessonType: LessonType
  ): Observable<Lesson[]> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('educationalProgramId', programId);
    httpParams = httpParams.append('lessonType', lessonType);

    return this.http.get<Lesson[]>(this.url + `/withoutTrainingCourseForm`, {
      params: httpParams,
      withCredentials: true,
    });
  }

  public getLessonByWeekId(
    weekId: number,
    lessonType: LessonType
  ): Observable<Lesson> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append('weekId', weekId);
    httpParams = httpParams.append('lessonType', lessonType);

    return this.http.get<Lesson>(this.url + `/byWeek`, {
      params: httpParams,
      withCredentials: true,
    });
  }
}
