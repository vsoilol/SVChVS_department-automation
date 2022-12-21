import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SpinnerService {
  public isSpinnerWork: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  constructor(private spinner: NgxSpinnerService) {}

  public hideSpinner(name?: string) {
    this.spinner.hide(name);
    this.isSpinnerWork.next(false);
  }

  public showSpinner(name?: string) {
    this.isSpinnerWork.next(true);
    this.spinner.show(name);
  }
}
