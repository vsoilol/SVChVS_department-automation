import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  public loaderName: string = 'mainLoader';
  public isShow: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private spinner: NgxSpinnerService) {}

  public show() {
    this.spinner.show(this.loaderName);
    this.isShow.next(true);
  }

  public hide() {
    this.spinner.hide(this.loaderName);
    this.isShow.next(false);
  }
}
