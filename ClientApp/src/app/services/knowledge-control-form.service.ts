import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { KnowledgeAssessment } from '../models/Response/KnowledgeAssessment';
import { KnowledgeControlForm } from '../models/Response/KnowledgeControlForm';

@Injectable({
  providedIn: 'root',
})
export class KnowledgeControlFormService {
  private url = environment.apiUrl + 'knowledgeControlForm';

  constructor(private http: HttpClient) {}

  public getAllByWeekId(weekId: number): Observable<KnowledgeAssessment[]> {
    return this.http.get<KnowledgeAssessment[]>(this.url + `/byWeek/${weekId}`);
  }

  public getAll(): Observable<KnowledgeControlForm[]> {
    return this.http.get<KnowledgeControlForm[]>(this.url);
  }
}
