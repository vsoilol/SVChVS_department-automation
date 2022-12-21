import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EducationalProgramDataService {
  public id: number;

  public isPracticalLessons: boolean;

  public isLaboratoryLessons: boolean;

  public disciplineId: number;

  constructor() {}
}
