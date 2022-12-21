import { SortDirectionInfo } from '../../Response/Common/SortDirectionInfo';

export interface EducationalProgramsFilter extends SortDirectionInfo {
  disciplineName: string;
  userId: string;
}
