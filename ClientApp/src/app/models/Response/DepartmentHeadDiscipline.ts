import { Status } from '../Enums/Status';

export interface DepartmentHeadDiscipline {
  disciplineId: number;

  name: string;

  studyStartingYear: number;

  status: Status;
}
