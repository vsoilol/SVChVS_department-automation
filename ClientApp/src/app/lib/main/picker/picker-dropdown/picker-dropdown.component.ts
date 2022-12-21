import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LookupModel } from '../models/lookup-model';

@Component({
  selector: 'app-picker-dropdown',
  templateUrl: './picker-dropdown.component.html',
  styleUrls: ['./picker-dropdown.component.scss'],
})
export class PickerDropdownComponent implements OnInit {
  ngOnInit(): void {
    this._filterOptionsWithEmptySearchString();
  }

  @Input()
  public maxHeight: string;

  @Input()
  public isMultiSelect: boolean = false;

  @Input()
  set filterText(val: string) {
    this._filterText = val;
    this.filterOptions(val);
  }

  get filterText(): string {
    return this._filterText;
  }

  private _filterText: string = '';

  public filteredOptions: LookupModel[] = [];

  @Input()
  public placeholder: string = '';

  @Input()
  public required: boolean = false;

  @Input()
  public options: LookupModel[] = [];

  @Input()
  public selectedOptions: LookupModel[];

  @Output()
  public suggest = new EventEmitter<MouseEvent>();

  @Output()
  public selectedOptionsChanged = new EventEmitter<LookupModel[]>();

  public filterOptions(searchString: string) {
    if (searchString === '') {
      this._filterOptionsWithEmptySearchString();
    } else {
      this.filteredOptions = this.options.filter((option) =>
        option.name.toLowerCase().includes(searchString.toLowerCase())
      );
    }
  }

  public selectOption(option: LookupModel, event: MouseEvent) {
    if (!Array.isArray(this.selectedOptions)) {
      return;
    }
    let isOptionFound = this.selectedOptions.find(
      (item) => item.id === option.id && item.name === option.name
    );

    if (this.isMultiSelect) {
      isOptionFound
        ? this.selectedOptionsChanged.emit(
            this.selectedOptions.filter(
              (item) => item.id !== option.id || item.name !== option.name
            )
          )
        : this.selectedOptionsChanged.emit([...this.selectedOptions, option]);
    } else {
      if (!isOptionFound) {
        this.selectedOptionsChanged.emit(
          option.name === this.placeholder ? [] : [option]
        );
      }

      this.filterText = '';

      this._suggestChanged(event);
    }
  }

  public isSelected(option: LookupModel) {
    if (!Array.isArray(this.selectedOptions)) {
      return false;
    }
    return this.selectedOptions?.findIndex(
      (item) => item.id === option.id && item.name === option.name
    ) > -1
      ? true
      : false;
  }

  public selectAll() {
    this.selectedOptionsChanged.emit(
      this.filteredOptions
        .filter(
          (filteredOption) =>
            !this.selectedOptions.some(
              (option) =>
                option.id === filteredOption.id &&
                option.name === filteredOption.name
            )
        )
        .concat(this.selectedOptions)
    );
  }

  public unselectAllOptions(event: MouseEvent) {
    this.selectedOptionsChanged.emit(
      this.selectedOptions.filter(
        (selectedOption) =>
          !this.filteredOptions.some(
            (option) =>
              option.id === selectedOption.id &&
              option.name === selectedOption.name
          )
      )
    );
  }

  public isUnselectBtnShowed(): boolean {
    if (
      !Array.isArray(this.filteredOptions) ||
      !Array.isArray(this.selectedOptions)
    ) {
      return false;
    }
    return (
      this.filteredOptions.filter(
        (filteredOption) =>
          !this.selectedOptions?.some(
            (option) =>
              option.id === filteredOption.id &&
              option.name === filteredOption.name
          )
      ).length === 0
    );
  }

  private _suggestChanged(event: MouseEvent) {
    this.suggest.emit(event);
  }

  private _filterOptionsWithEmptySearchString() {
    this.filteredOptions = this.options;

    if (
      !this.isMultiSelect &&
      this.placeholder &&
      !this.required &&
      this.filteredOptions[0].id !== this.placeholder
    ) {
      this.filteredOptions.unshift({
        id: this.placeholder,
        name: this.placeholder,
      });
    }
  }
}
