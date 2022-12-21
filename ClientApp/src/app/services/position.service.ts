import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Position } from '../models/Response/Position';

@Injectable({
  providedIn: 'root',
})
export class PositionService {
  private url = environment.apiUrl + 'position';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Position[]> {
    return this.http.get<Position[]>(this.url);
  }
}
