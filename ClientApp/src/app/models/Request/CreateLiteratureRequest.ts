import { LiteratureType } from 'src/app/models/Enums/LiteratureType';

export interface CreateLiteratureRequest {
  description: string;
  recommended: string;
  setNumber: string;
  literatureType: LiteratureType;
  educationalProgramId: number;
}
