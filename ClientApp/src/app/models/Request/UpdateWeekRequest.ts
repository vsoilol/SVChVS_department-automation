import { KnowledgeAssessment } from '../Response/KnowledgeAssessment';
import { Lesson } from '../Response/Lesson';

export interface UpdateWeekRequest {
  id: number;
  independentWorkHours: number;
  lessons: Lesson[];
  knowledgeAssessments: KnowledgeAssessment[];
}
