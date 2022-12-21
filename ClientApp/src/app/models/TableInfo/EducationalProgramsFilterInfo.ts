import { EducationalProgramsFilter } from '../Request/Filters/EducationalProgramsFilter';

export interface EducationalProgramsFilterInfo {
  pageNumber: number;
  pageSize: number;
  filter: EducationalProgramsFilter;
  teacherId: number;
}
