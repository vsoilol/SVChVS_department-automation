import { CompetenceFormationLevel } from './CompetenceFormationLevel';

export interface IndicatorWithLevels {
  competenceId: number;

  competenceCode: string;

  competenceName: string;

  competenceFormationLevels: CompetenceFormationLevel[];
}
