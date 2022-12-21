import { SortDirectionInfo } from '../../Response/Common/SortDirectionInfo';

export interface TeacherFilter extends SortDirectionInfo {
  surname: string | null;
  positionId: number | null;
  departmentId: number | null;
}
