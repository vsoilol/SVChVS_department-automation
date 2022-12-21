import { Status } from '../Enums/Status';

export interface ChangeDisciplineStatusRequest {
  status: Status;
  id: number;
}
