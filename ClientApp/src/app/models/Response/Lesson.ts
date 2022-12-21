export interface Lesson {
  id: number;
  number: number;
  name: string;
  hours: number;
  content?: string;
  competencesId?: number[];
  competenceInfo?: string;
  isEdit?: boolean;
}
