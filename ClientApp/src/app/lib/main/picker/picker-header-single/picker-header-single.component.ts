import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { LookupModel } from '../models/lookup-model';

@Component({
  selector: 'app-picker-header-single',
  templateUrl: './picker-header-single.component.html',
  styleUrls: ['./picker-header-single.component.scss']
})
export class PickerHeaderSingleComponent{
  
  @Input()
  public showErrorMessage: boolean = false;
  
  @Input()
  public required: boolean = false;

  private _selectedOptions:LookupModel[] = [];
  @Input()
  public set selectedOptions(options: LookupModel[]){
    this._selectedOptions = Array.isArray(options) ? options : [];
  };
  public get selectedOptions():LookupModel[] {
    return this._selectedOptions
  }

  @Input()
  public placeholder: string = '';

  @Output()
  public suggest = new EventEmitter<MouseEvent>();

  public clickOnInput(event: MouseEvent) {
    this.suggest.emit(event);
  }

  public isArray(any: any) {
    return Array.isArray(any)
  }
}
