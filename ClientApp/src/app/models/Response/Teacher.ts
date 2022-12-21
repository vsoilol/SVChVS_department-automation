import { Department } from './Department';
import { Position } from './Position';

export interface Teacher {
  userId: string;

  id: number;

  name: string;

  surname: string;

  patronymic: string;

  isActive: boolean;

  applicationUserId: string;

  position: string;

  department: string;
}
