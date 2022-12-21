import { Component, Input, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';
import { SpinnerService } from './spinner.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss'],
})
export class SpinnerComponent implements OnInit {
  @Input()
  public name: string;

  get isSpinnerWork(): BehaviorSubject<boolean> {
    return this.spinnerService.isSpinnerWork;
  }

  constructor(private spinnerService: SpinnerService) {}

  ngOnInit(): void {}
}
