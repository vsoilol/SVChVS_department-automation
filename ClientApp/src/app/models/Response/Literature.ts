import { LiteratureType } from '../Enums/LiteratureType';

export interface Literature {
  id: number;
  description: string;
  recommended: string;
  setNumber: string;
  literatureType?: LiteratureType;
}
