import { TeacherFullName } from './Common/TeacherFullName';
import { Teacher } from './Teacher';

export interface AdditionalDisciplineInfo {
  name: string;

  specialtyName: string;

  studyStartingYear: number;

  teachers: TeacherFullName[];
}
