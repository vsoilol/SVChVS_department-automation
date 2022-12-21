import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Semester } from '../models/Response/Semester';

@Injectable({
  providedIn: 'root',
})
export class SemesterService {
  private url = environment.apiUrl + 'semester';

  constructor(private http: HttpClient) {}

  public getAllSemestersByProgramId(programId: number): Observable<Semester[]> {
    return this.http.get<Semester[]>(this.url + `/byProgramId/${programId}`);
  }
}
