import {
  Component,
  OnInit,
  HostListener,
  Input,
  forwardRef,
  ChangeDetectorRef,
  AfterViewInit,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { LookupModel } from './models/lookup-model';

@Component({
  selector: 'app-picker',
  templateUrl: './picker.component.html',
  styleUrls: ['./picker.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PickerComponent),
      multi: true,
    },
  ],
})
export class PickerComponent implements ControlValueAccessor, AfterViewInit {
  public isReady: boolean = false;

  constructor(private cdr: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    setTimeout(() => {
      if (this._selectedOptions) {
        this.onChangeValue();
      }
    });
  }

  private _options: LookupModel[] = [];

  @Input()
  public set options(data: LookupModel[]) {
    this._options = data;
    if (this._options.length > 0) {
      this.isReady = true;
    }
    this.cdr.markForCheck();
  }

  public get options() {
    return this._options;
  }

  @Input()
  public width: string;

  @Input()
  public maxHeight: string;

  @Input()
  public isMultiSelect: boolean = false;

  @Input()
  public required: boolean = false;

  @Input()
  public placeholder: string = '';

  @Input()
  public label: string = '';

  @Input()
  public tooltip: string;

  @Input()
  public errorMessage: string = '';

  public showErrorMessage: boolean = false;

  public filteredOptions: LookupModel[] = [];

  public filterText: string = '';

  public show: boolean = false;

  private _isClickInside: boolean = false;

  private _selectedOptions: LookupModel[] = [];

  public get selectedOptions() {
    return this._selectedOptions;
  }

  @Input()
  public set selectedOptions(val) {
    this._selectedOptions = val;

    this.onChangeValue();
  }

  private onChange: any = () => {};
  private onTouch: any = () => {};

  public writeValue(value: LookupModel[]) {
    if (value) {
      this._selectedOptions = Array.isArray(value) ? value : [];
    }
  }

  private onChangeValue(): void {
    if (this.isMultiSelect) {
      this.onChange(this._selectedOptions.map((x) => x.id));
    } else {
      this.onChange(this._selectedOptions[0]?.id);
    }
  }

  public registerOnChange(fn: any) {
    this.onChange = fn;
  }

  registerOnTouched(onTouched: Function) {
    this.onTouch = onTouched;
  }

  @HostListener('mousedown', ['$event'])
  clicked(event: any) {
    if (event.toElement.id !== 'errorMessage') {
      this._isClickInside = true;

      if (this.required && this.showErrorMessage) {
        this.showErrorMessage = !this.showErrorMessage;
      }
    }
  }

  @HostListener('document:mousedown')
  clickArea() {
    if (!this._isClickInside) {
      if (this.show && this.required && this.selectedOptions.length === 0) {
        this.showErrorMessage = true;
      }

      this.show = false;
    }

    this._isClickInside = false;
  }

  @HostListener('document:scroll')
  scrolling() {
    if (!this._isClickInside) {
      if (this.show && this.required && this.selectedOptions.length === 0) {
        this.showErrorMessage = true;
      }

      this.show = false;
    }

    this._isClickInside = false;
  }

  //   @HostListener('scroll') scrolling() {
  //     console.log('scrolling');
  //   }

  public suggest(event: MouseEvent) {
    this.show = !this.show;
  }

  public suggestFromInput(event: MouseEvent) {
    if (!this.show) {
      this.show = true;
    }
  }

  public filterOptionsChanged(event: string) {
    this.filterText = event;
  }

  public selectedOptionsChanged(event: LookupModel[]) {
    this.selectedOptions = event;
  }
}
