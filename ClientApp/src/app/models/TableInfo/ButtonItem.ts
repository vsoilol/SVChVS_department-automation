import { Observable } from 'rxjs';

export interface ButtonItem {
  name: string;
  isMethodObservable: boolean;
  onClick: (data: any) => Observable<void> | void;
  getButtonInfo?: (data: any) => string;
}
