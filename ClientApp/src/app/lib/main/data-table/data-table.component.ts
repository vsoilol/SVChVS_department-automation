import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { Order } from 'src/app/models/Enums/Order';
import { FilterItem } from 'src/app/models/Request/Filters/FilterItem';
import { ButtonItem } from 'src/app/models/TableInfo/ButtonItem';
import { IColumnInfo } from 'src/app/models/TableInfo/IColumnInfo';
import {
  debounceTime,
  distinctUntilChanged,
  tap,
  switchMap,
} from 'rxjs/operators';
import { AdditionalFilterInfo } from 'src/app/models/Request/Filters/AdditionalFilterInfo';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss'],
})
export class DataTableComponent implements OnInit {
  public itemsPerPage = 5;
  public itemsCount = 0;
  public currentPage = 1;

  public propertyName: string = '';
  public sortDirection: Order = Order.Asc;

  public form: FormGroup;

  public isSpinerWork = false;

  @Input()
  public columnList: IColumnInfo[];

  @Input()
  public additionalFilters: AdditionalFilterInfo[] = [];

  public dataSource: BehaviorSubject<Object[]> = new BehaviorSubject<Object[]>(
    []
  );

  @Input()
  public filterInfo: FilterItem;

  @Input()
  public actionButtons: ButtonItem[] = [];

  @Input()
  public storageKey: string;

  @Input()
  public getAll: (data: any) => Observable<any>;

  @Output()
  public onGetData = new EventEmitter<any>();

  constructor(private fb: FormBuilder) {}

  columnsToDisplay: string[] = [];

  get displayedColumns() {
    const column = this.columnInfoFormValue
      .filter((column) => column.visible)
      .map((column) => column.field);

    if (this.actionButtons.length !== 0) {
      column.push('action');
    }

    return column;
  }

  get columnInfoFormValue(): IColumnInfo[] {
    return this.form.get('filterForm').get('columnList').value as IColumnInfo[];
  }

  get additionalFiltersFormGroup(): FormGroup {
    return this.form.get('filterForm').get('additionalFilters') as FormGroup;
  }

  set columnInfoFormValue(columsInfo: IColumnInfo[]) {
    this.form.get('filterForm').get('columnList').setValue(columsInfo);
  }

  set itemsPerPageFormValue(value: number) {
    this.form.get('filterForm').get('itemsPerPage').setValue(value);
  }

  ngOnInit(): void {
    this.formInit();
    this.filterInit();

    if (localStorage.getItem(this.storageKey)) {
      this.getInfoAboutTableColumnsFromLocalStorage();
    } else {
      this.getData();
    }
  }

  private getData() {
    const request: FilterItem = {
      searchString: '',
      currentPage: this.currentPage,
      itemsPerPage: this.itemsPerPage,
      propertyName: this.propertyName,
      sortDirection: this.sortDirection,
    };

    this.showSpinner();

    this.getAll(request).subscribe((result) => {
      this.dataSource.next(result.items);
      this.itemsCount = result.totalCount;
      this.hideSpinner();
    });
  }

  getInfoAboutTableColumnsFromLocalStorage(): void {
    const preferences = JSON.parse(localStorage.getItem(this.storageKey));
    this.columnInfoFormValue = this.columnInfoFormValue.map((column) => {
      const preference = preferences.find(
        (data) => data.field === column.field
      );
      if (preference) {
        column.visible = preference.visible;
      }
      return column;
    });
  }

  public pageChange(pageNumber): void {
    this.currentPage = pageNumber.pageIndex + 1;
    this.itemsPerPageFormValue = pageNumber.pageSize;
  }

  public persistColumnPreference(): void {
    localStorage.setItem(
      this.storageKey,
      JSON.stringify(this.columnInfoFormValue)
    );
  }

  private formInit() {
    this.form = this.fb.group({
      filterForm: this.fb.group({
        searchString: '',
        propertyName: '',
        itemsPerPage: 5,
        currentPage: 1,
        columnList: this.fb.array(
          this.columnList.map((column) => {
            return this.fb.group({
              field: column.field,
              label: column.label,
              visible: column.visible,
            });
          })
        ),
        additionalFilters: this.fb.group({}),
      }),
    });

    if (this.additionalFilters) {
      this.additionalFilters.forEach((value) => {
        this.additionalFiltersFormGroup.addControl(
          value.formName,
          new FormControl('')
        );
      });
    }
  }

  public doSort(fieldName: string): void {
    if (fieldName === this.propertyName) {
      this.sortDirection === Order.Asc
        ? (this.sortDirection = Order.Desc)
        : (this.sortDirection = Order.Asc);
    } else {
      this.propertyName = fieldName;
      this.sortDirection = Order.Asc;
    }
    this.form.get('filterForm').get('propertyName').setValue(this.propertyName);
  }

  filterInit() {
    const filterForm: FormGroup = this.form.get('filterForm') as FormGroup;

    filterForm.valueChanges
      .pipe(
        tap(() => {
          this.dataSource.next([]);
          this.showSpinner();
        }),
        debounceTime(500),
        distinctUntilChanged(),
        switchMap((query) =>
          this.getAll({
            searchString: query.searchString,
            currentPage: this.currentPage,
            itemsPerPage: query.itemsPerPage,
            propertyName: this.propertyName,
            sortDirection: this.sortDirection,
            additionalFilters: this.additionalFiltersFormGroup.value,
          })
        ),
        tap(() => this.hideSpinner())
      )
      .subscribe((result) => {
        this.dataSource.next(result.items);
        this.itemsCount = result.totalCount;
      });
  }

  private showSpinner() {
    this.isSpinerWork = true;
  }

  private hideSpinner() {
    this.isSpinerWork = false;
  }

  private getDataWithFormInfo() {
    const filterForm: FormGroup = this.form.get('filterForm') as FormGroup;

    this.dataSource.next([]);
    this.showSpinner();

    this.getAll({
      searchString: filterForm.get('searchString').value,
      currentPage: this.currentPage,
      itemsPerPage: filterForm.get('itemsPerPage').value,
      propertyName: this.propertyName,
      sortDirection: this.sortDirection,
      additionalFilters: this.additionalFiltersFormGroup.value,
    }).subscribe((result) => {
      this.dataSource.next(result.items);
      this.itemsCount = result.totalCount;
      this.hideSpinner();
    });
  }

  public runClickButtonFunction(buttonInfo: ButtonItem, data: any) {
    if (buttonInfo.isMethodObservable) {
      (buttonInfo.onClick(data) as Observable<void>).subscribe(() => {
        this.getDataWithFormInfo();
      });
    } else {
      buttonInfo.onClick(data);
    }
  }
}
