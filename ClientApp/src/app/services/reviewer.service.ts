import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UpdateReviewer } from '../models/Request/UpdateReviewer';
import { Reviewer } from '../models/Response/Reviewer';

@Injectable({
  providedIn: 'root',
})
export class ReviewerService {
  private url = environment.apiUrl + 'reviewer';

  constructor(private http: HttpClient) {}

  getByProgramId(educationalProgramId: number): Observable<Reviewer> {
    return this.http.get<Reviewer>(
      this.url + `/byProgramId/${educationalProgramId}`
    );
  }

  update(request: UpdateReviewer): Observable<void> {
    return this.http.put<void>(this.url, request);
  }
}
