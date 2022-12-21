import { Status } from '../Enums/Status';

export interface ChangeProgramStatusRequest {
  status: Status;
  id: number;
}
