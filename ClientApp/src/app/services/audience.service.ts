import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AudienceBriefInfo } from '../models/Response/AudienceBriefInfo';

@Injectable({
  providedIn: 'root',
})
export class AudienceService {
  private url = environment.apiUrl + 'audience';

  constructor(private http: HttpClient) {}

  getAllByProgramId(
    educationalProgramId: number
  ): Observable<AudienceBriefInfo[]> {
    return this.http.get<AudienceBriefInfo[]>(
      this.url + `/byProgramId/${educationalProgramId}`
    );
  }

  public getAll(): Observable<AudienceBriefInfo[]> {
    return this.http.get<AudienceBriefInfo[]>(this.url);
  }
}
