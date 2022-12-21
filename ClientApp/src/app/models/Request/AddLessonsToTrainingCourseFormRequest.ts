import { LessonType } from '../Enums/LessonType';

export interface AddLessonsToTrainingCourseFormRequest {
  trainingCourseFormId: number;
  educationalProgramId: number;
  lessonType: LessonType;
  fromLessonId: number;
  toLessonId: number;
}
