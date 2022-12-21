import { LessonType } from '../Enums/LessonType';
import { Lesson } from '../Response/Lesson';

export interface CreateLessonRequest {
  lesson: Lesson;
  lessonType: LessonType;
  competencesId?: number[];
  educationalProgramId: number;
}
