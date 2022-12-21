import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LookupModel } from '../models/lookup-model';

@Component({
  selector: 'app-picker-header-multi',
  templateUrl: './picker-header-multi.component.html',
  styleUrls: ['./picker-header-multi.component.scss'],
})
export class PickerHeaderMultiComponent {
  @Input()
  public selectedOptions: LookupModel[];

  @Input()
  public showErrorMessage: boolean = false;

  @Input()
  public required: boolean = false;

  @Input()
  public placeholder: string = '';

  @Output()
  public filterOptionsChanged = new EventEmitter<string>();

  @Output()
  public suggest = new EventEmitter<MouseEvent>();

  @Output()
  public suggestFromInput = new EventEmitter<MouseEvent>();

  @Output()
  public selectedOptionsChanged = new EventEmitter<LookupModel[]>();

  @Input()
  public filterText: string = '';

  public clickOnButton(event: MouseEvent) {
    this.suggest.emit(event);
  }

  public clickOnInput(event: MouseEvent) {
    this.suggestFromInput.emit(event);
  }

  public inputFilterTextChanged(searchString: string) {
    this.filterOptionsChanged.emit(searchString);
  }

  public unselectAllOptions(event: MouseEvent) {
    this.selectedOptionsChanged.emit([]);
  }

  public deleteSelectedOption(option: LookupModel) {
    this.selectedOptionsChanged.emit(
      this.selectedOptions.filter((item) => item !== option)
    );
  }
}
