import { KnowledgeAssessment } from './KnowledgeAssessment';
import { Lesson } from './Lesson';

export interface Week {
  id: number;
  number: number;
  independentWorkHours: number;
  trainingModuleNumber: number;
  laboratoryLesson: Lesson;
  lecture: Lesson;
  practicalLesson: Lesson;
  knowledgeAssessments: KnowledgeAssessment[];
}
