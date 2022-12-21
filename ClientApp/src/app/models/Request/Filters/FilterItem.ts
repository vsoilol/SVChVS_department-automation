import { SortDirectionInfo } from '../../Response/Common/SortDirectionInfo';

export interface FilterItem extends SortDirectionInfo {
  searchString: string;
  currentPage: number;
  itemsPerPage: number;
  additionalFilters?: any;
}
