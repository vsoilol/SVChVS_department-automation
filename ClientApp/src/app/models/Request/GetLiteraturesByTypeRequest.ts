import { LiteratureType } from 'src/app/models/Enums/LiteratureType';

export interface GetLiteraturesByTypeRequest {
  educationalProgramId: number;
  literatureType: LiteratureType;
}
