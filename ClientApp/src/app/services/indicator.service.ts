import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IndicatorWithLevels } from '../models/Response/IndicatorWithLevels';

@Injectable({
  providedIn: 'root',
})
export class IndicatorService {
  private url = environment.apiUrl + 'indicator';

  constructor(private http: HttpClient) {}

  public getIndicatorWithLevelsByProgramId(
    educationalProgramId: number
  ): Observable<IndicatorWithLevels[]> {
    return this.http.get<IndicatorWithLevels[]>(
      this.url + `/with-levels/${educationalProgramId}`
    );
  }
}
