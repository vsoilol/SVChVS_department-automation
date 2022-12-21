import { Status } from '../Enums/Status';

export interface EducationalProgram {
  id: number;
  status: Status;
  isPracticalLessons: boolean;
  isLaboratoryLessons: boolean;
  disciplineId: number;
}
