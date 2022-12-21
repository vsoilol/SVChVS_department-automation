import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { InformationDialogComponent } from 'src/app/lib/main/information-dialog/information-dialog.component';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { InformationDialogData } from 'src/app/models/Dialog/InformationDialogData';
import { Order } from 'src/app/models/Enums/Order';
import { Department } from 'src/app/models/Response/Department';
import { Position } from 'src/app/models/Response/Position';
import { Teacher } from 'src/app/models/Response/Teacher';
import { IColumnInfo } from 'src/app/models/TableInfo/IColumnInfo';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DepartmentService } from 'src/app/services/department.service';
import { PositionService } from 'src/app/services/position.service';
import { TeacherService } from 'src/app/services/teacher.service';
import { AdditionalFilterInfo } from 'src/app/models/Request/Filters/AdditionalFilterInfo';
import { ButtonItem } from 'src/app/models/TableInfo/ButtonItem';
import { PaginatedResult } from 'src/app/models/Response/Common/PaginatedResult';
import { getLookupModelsFromObjectsArray } from 'src/app/lib/main/picker/models/lookup-model';

@Component({
  selector: 'app-teachers-table',
  templateUrl: './teachers-table.component.html',
  styleUrls: ['./teachers-table.component.scss'],
})
export class TeachersTableComponent implements OnInit {
  public columnList: IColumnInfo[];
  public actionButtons: ButtonItem[];

  public dataSource = new BehaviorSubject<Teacher[]>([]);
  public positions: Position[];
  public departments: Department[];

  public additionalFilters: AdditionalFilterInfo[] = [];

  readonly STORAGE_KEY = 'TEACHERS_TABLE';

  constructor(
    private teacherService: TeacherService,
    private departmentService: DepartmentService,
    private positionService: PositionService,
    public dialog: MatDialog,
    public loaderService: LoaderService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.columnList = [
      {
        field: 'surname',
        label: 'Фамилия',
        visible: true,
        propertyName: 'ApplicationUser.Surname',
        width: '12%',
      },
      {
        field: 'name',
        label: 'Имя',
        visible: true,
        propertyName: 'ApplicationUser.UserName',
        width: '12%',
      },
      {
        field: 'patronymic',
        label: 'Отчество',
        visible: true,
        propertyName: 'ApplicationUser.Patronymic',
        width: '12%',
      },
      {
        field: 'position',
        label: 'Должность',
        visible: true,
        propertyName: 'Position.Name',
        width: '16%',
      },
      {
        field: 'department',
        label: 'Кафедра',
        visible: true,
        propertyName: 'Department.Name',
        width: '37%',
      },
      {
        field: 'isActive',
        label: 'Доступ',
        visible: true,
        propertyName: 'ApplicationUser.IsActive',
        getRowInfo: this.getAccessDescription,
        getRowStyle: this.getAccessStyle,
        width: '9%',
      },
    ];

    this.actionButtons = [
      {
        name: 'Сгенерировать пароль',
        isMethodObservable: false,
        onClick: this.generatePassword,
      },
      {
        name: 'Активировать',
        isMethodObservable: true,
        onClick: this.changeUserAccess,
        getButtonInfo: this.getAccessDescriptionByUserInfo,
      },
    ];

    this.loaderService.show();

    const joinedWithObjectForm$ = forkJoin({
      departments: this.departmentService.getAll(),
      positions: this.positionService.getAll(),
    });

    joinedWithObjectForm$.subscribe((result) => {
      this.additionalFilters = [
        {
          formName: 'departmentId',
          name: 'Кафедра',
          values: getLookupModelsFromObjectsArray(
            result.departments,
            'id',
            'name'
          ),
          selectWidth: '250px',
        },
        {
          formName: 'positionId',
          name: 'Должность',
          values: getLookupModelsFromObjectsArray(
            result.positions,
            'id',
            'name'
          ),
          selectWidth: '200px',
        },
      ];
      this.loaderService.hide();
    });
  }

  public changeUserAccess = (teacher: any): Observable<void> => {
    if (teacher.isActive) {
      return this.authenticationService.deactivateUser(teacher.userId);
    }
    return this.authenticationService.activateUser(teacher.userId);
  };

  public getAccessDescription = (isActive: boolean): string => {
    return isActive ? 'Активен' : 'Неактивен';
  };

  public getAccessDescriptionByUserInfo = (teacher: Teacher): string => {
    return teacher.isActive ? 'Деактивировать' : 'Активировать';
  };

  public getAccessStyle = (isActive: boolean): string => {
    return (isActive ? 'active' : 'not-active') + ' label';
  };

  public getAllWithPagination = (
    data: any
  ): Observable<PaginatedResult<Teacher[]>> => {
    return this.teacherService.getWithPagination(
      data.currentPage,
      data.itemsPerPage,
      {
        surname: data.searchString,
        sortDirection: data.sortDirection,
        propertyName: data.propertyName,
        positionId: data.additionalFilters?.positionId || null,
        departmentId: data.additionalFilters?.departmentId || null,
      }
    );
  };

  generatePassword = (teacher: Teacher): void => {
    this.loaderService.show();
    this.teacherService
      .changeTeacherPassword(teacher.userId)
      .subscribe((result) => {
        let data: InformationDialogData = {
          title:
            teacher.name + ' ' + teacher.surname + ' ' + teacher.patronymic,
          content: 'Новый пароль: ' + result.newPassword,
        };
        this.loaderService.hide();

        this.openDialog(data);
      });
  };

  openDialog(data: InformationDialogData): void {
    const dialogRef = this.dialog.open(InformationDialogComponent, {
      width: '250px',
      data: data,
    });
  }
}
