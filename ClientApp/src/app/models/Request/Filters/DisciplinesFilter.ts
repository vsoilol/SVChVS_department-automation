import { Status } from '../../Enums/Status';
import { SortDirectionInfo } from '../../Response/Common/SortDirectionInfo';

export interface DisciplinesFilter extends SortDirectionInfo {
  disciplineName: string;

  status: Status | null;

  studyStartingYear: number | null;

  departmentId: number;
}
