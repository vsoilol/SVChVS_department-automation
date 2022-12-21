export class PaginatedResult<T> {
  items: T;

  hasPreviousPage: boolean;

  hasNextPage: boolean;

  totalPages: number;

  totalCount: number;

  pageNumber: number;
}
