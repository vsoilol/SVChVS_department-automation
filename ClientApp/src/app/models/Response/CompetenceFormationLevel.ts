import { FormationLevel } from '../Enums/FormationLevel';
import { EvaluationToolType } from './EvaluationToolType';

export interface CompetenceFormationLevel {
  id: number;
  formationLevel: FormationLevel;
  factualDescription: string;
  learningOutcomes: string;
  evaluationToolTypes: EvaluationToolType[];
}
