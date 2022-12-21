import { Lesson } from './Lesson';

export interface TrainingCourseForm {
  id: number;
  name: string;
  lectures: Lesson[];
  practicalLessons: Lesson[];
  laboratoryLessons: Lesson[];
}
