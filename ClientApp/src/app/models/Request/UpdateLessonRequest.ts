import { LessonType } from '../Enums/LessonType';
import { Lesson } from '../Response/Lesson';

export interface UpdateLessonRequest {
  lesson: Lesson;
  lessonType: LessonType;
  competencesId?: number[];
}
