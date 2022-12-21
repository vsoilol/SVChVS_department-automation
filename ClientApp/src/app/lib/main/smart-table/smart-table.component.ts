import {
  Component,
  ElementRef,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  faCheck,
  faFilter,
  faPencilAlt,
  faTimes,
  faTrash,
} from '@fortawesome/free-solid-svg-icons';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';
import { EMPLOYEE_LIST, IEmployee } from '../employee-meta-data';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];

@Component({
  selector: 'app-smart-table',
  templateUrl: './smart-table.component.html',
  styleUrls: ['./smart-table.component.scss'],
})
export class SmartTableComponent implements OnInit {

  @ViewChild('toast', { static: false })
  toast: ElementRef;

  public employeeList = EMPLOYEE_LIST;

  faFilter = faFilter;
  faPencil = faPencilAlt;
  faTrash = faTrash;
  faCheck = faCheck;
  faTimes = faTimes;

  readonly STORAGE_KEY = 'EMPLOYEE_TABLE';
  public itemsPerPage = 5;
  public itemsCount = 0;
  public currentPage = 1;

  private employeeId: number;

  public columnList = [
    { field: 'status', label: 'Status', visible: true },
    { field: 'name', label: 'Name', visible: true },
    { field: 'salary', label: 'Salary', visible: true },
    { field: 'email', label: 'Email', visible: true },
    { field: 'action', label: 'Action', visible: true },
  ];

  public dataSource = new BehaviorSubject<IEmployee[]>([]);

  public search = '';
  public status = 'All';
  public sortKey = '';
  public sortDirection = 'asc';
  private errorMessages = validations;

  public form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private modalService: NgbModal
  ) {
    this.employeeList.map((employee: IEmployee) => {
      return {
        ...employee,
        edit: employee.edit ? employee.edit : false,
      };
    });
  }

  get displayedColumns() {
    return this.columnList
      .filter((column) => column.visible)
      .map((column) => column.field);
  }

  ngOnInit(): void {
    if (localStorage.getItem(this.STORAGE_KEY)) {
      const preferences = JSON.parse(localStorage.getItem(this.STORAGE_KEY));

      this.columnList = this.columnList.map((column) => {
        const preference = preferences.find(
          (data) => data.field === column.field
        );

        if (preference) {
          column.visible = preference.visible;
        }

        return column;
      });
    }

    this.getEmployees();
  }

  getEmployees(): void {
    const items = this.employeeList
      .sort((x, y) => {
        if (x.id > y.id) {
          return -1;
        }
        return 1;
      })
      .filter((employee: IEmployee) => {
        let allowed = this.status === 'All' || employee.status === this.status;

        if (allowed && this.search) {
          const mathes = employee.employee_name
            .toLocaleUpperCase()
            .match(this.search.toLocaleUpperCase());
          return mathes && mathes.length > 0;
        }

        return allowed;
      });

    if (this.sortKey) {
      items.sort((x, y) => {
        let xField, yField;

        if (['employee_salary'].indexOf(this.sortKey) !== -1) {
          xField = x[this.sortKey];
          yField = y[this.sortKey];
        } else {
          xField = x[this.sortKey].toString().toLocaleUpperCase();
          yField = y[this.sortKey].toString().toLocaleUpperCase();
        }

        if (xField === yField) return 0;
        if (this.sortDirection === 'asc' && xField < yField) return -1;
        if (this.sortDirection === 'dsc' && xField > yField) return -1;

        return 1;
      });
    }

    this.itemsCount = items.length;

    const noOfRowsToDisplay = items.slice(
      this.itemsPerPage * (this.currentPage - 1),
      this.itemsPerPage * this.currentPage
    );

    this.dataSource.next(noOfRowsToDisplay);
    this.formInit(noOfRowsToDisplay);
  }

  // asc -> ascending order
  // dsc -> descending order
  doSort(field: string): void {
    if (field === this.sortKey) {
      this.sortDirection === 'asc'
        ? (this.sortDirection = 'dsc')
        : (this.sortDirection = 'asc');
    } else {
      this.sortKey = field;
      this.sortDirection = 'asc';
    }

    this.getEmployees();
  }

  persistColumnPreference(): void {
    localStorage.setItem(this.STORAGE_KEY, JSON.stringify(this.columnList));
  }

  pageChange(value: number): void {
    this.currentPage = value;
    this.getEmployees();
  }

  formInit(employeeList: IEmployee[]): void {
    this.form = this.formBuilder.group({
      employeeList: this.formBuilder.array(
        employeeList.map((employee: IEmployee) => {
          return this.formBuilder.group({
            id: [employee.id],
            status: [
              employee.status || '',
              Validators.compose([Validators.required]),
            ],
            name: [
              employee.employee_name || '',
              Validators.compose([
                Validators.required,
                Validators.minLength(5),
                Validators.maxLength(32),
              ]),
            ],
            salary: [
              employee.employee_salary || '',
              Validators.compose([Validators.required, Validators.min(0)]),
            ],
            email: [
              employee.employee_email || '',
              Validators.compose([
                Validators.required,
                Validators.pattern(
                  /^[_a-zA-Z0-9]+(\.[_a-zA-Z0-9]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/
                ),
              ]),
            ],
          });
        })
      ),
    });
  }

  getError(index: number, property: string, validations: string[]) {
    const control = this.form
      .get('employeeList')
      .get(index.toString())
      .get(property);

    if (!control || !control.dirty || !control.errors) {
      return false;
    }

    for (const validation of validations) {
      if (control.errors[validation]) {
        return this.errorMessages[property][validation];
      }
    }

    return false;
  }

  addEmployee(): void {
    const newControl: IEmployee = {
      id: this.employeeList[0].id + 1, // not required if you are integrating with backend.
      status: '',
      employee_name: '',
      employee_salary: null,
      employee_email: '',
      edit: true,
    };

    this.employeeList.push(newControl);
    this.getEmployees();
  }

  saveEmployee(employee: IEmployee): void {
    const getIndex = this.employeeList.findIndex(
      (data) => data.id === employee.id
    );

    const employeeDetails = this.form
      .get('employeeList')
      .get(getIndex.toString()).value;

    if (this.form.get('employeeList').get(getIndex.toString()).valid) {
      this.employeeList[getIndex].status = employeeDetails.status;
      this.employeeList[getIndex].employee_name = employeeDetails.name;
      this.employeeList[getIndex].employee_salary = employeeDetails.salary;
      this.employeeList[getIndex].employee_email = employeeDetails.email;

      this.employeeList[getIndex].edit = false;

      this.enableToaster('success', 'Successfully updated !!!');
    } else {
      this.enableToaster('error', 'Oops !!! Error occurred');

      Object.keys(this.form.controls['employeeList'].value['0']).forEach(
        (field: string) => {
          const control = this.form
            .get('employeeList')
            .get(getIndex.toString())
            .get(field);
          control.markAsDirty();
        }
      );
    }
  }

  enableToaster(status: string, message: string): void {
    const element = this.toast.nativeElement;

    element.innerText = message;

    if (status === 'success') {
      element.classList.add('label-success');
    }

    if (status === 'error') {
      element.classList.add('label-danger');
    }

    element.classList.add('show-toast');

    setTimeout(() => {
      element.classList.remove('show-toast', 'label-success', 'label-danger');
      element.innerText = '';
    }, 3000);
  }

  openModal(content: TemplateRef<any>, employeeId: number): void {
    this.employeeId = employeeId;
    this.modalService.open(content, { backdrop: 'static', centered: true });
  }

  deleteEmployee(): void {
    this.employeeList = this.employeeList.filter(
      (employee) => employee.id !== this.employeeId
    );
    this.getEmployees();
    this.enableToaster('success', 'Successfully deleted !!!');
  }

  cancelEditing(employee: IEmployee): void {
    const getIndex = this.employeeList.findIndex(
      (data) => data.id === employee.id
    );

    const fieldValidity = !!(
      this.employeeList[getIndex].status &&
      this.employeeList[getIndex].employee_name &&
      this.employeeList[getIndex].employee_salary &&
      this.employeeList[getIndex].employee_email
    );

    if (fieldValidity) {
      employee.edit = false;
    } else {
      this.employeeList = this.employeeList.filter(
        (data) => data.id !== employee.id
      );
      employee.edit = false;
    }

    this.getEmployees();
  }
}

export const validations = {
  status: {
    required: 'Please provide status',
  },
  name: {
    required: 'Please provide name',
    minlength: 'Min length should be 5',
    maxlength: 'Max length should be 32',
  },
  salary: {
    required: 'Please provide salary',
    min: "Salary can't be less than 0",
  },
  email: {
    required: 'Please provide email',
    pattern: 'Please provide valid email',
  },
};
