import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { forkJoin, Observable } from 'rxjs';
import { map, tap, mergeMap, finalize } from 'rxjs/operators';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import {
  getLookupModelsFromArray,
  getLookupModelsFromModelArray,
  getLookupModelsFromObjectsArray,
} from 'src/app/lib/main/picker/models/lookup-model';
import { Status } from 'src/app/models/Enums/Status';
import { ChangeDisciplineStatusRequest } from 'src/app/models/Request/ChangeDisciplineStatusRequest';
import { AdditionalFilterInfo } from 'src/app/models/Request/Filters/AdditionalFilterInfo';
import { AdditionalDisciplineInfo } from 'src/app/models/Response/AdditionalDisciplineInfo';
import { PaginatedResult } from 'src/app/models/Response/Common/PaginatedResult';
import { DepartmentHeadDiscipline } from 'src/app/models/Response/DepartmentHeadDiscipline';
import { ButtonItem } from 'src/app/models/TableInfo/ButtonItem';
import { IColumnInfo } from 'src/app/models/TableInfo/IColumnInfo';
import { AddInformationBlockComponent } from 'src/app/pages/teacher/pages/edit-educational-program/dialogs/add-information-block/add-information-block.component';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CurriculumService } from 'src/app/services/curriculum.service';
import { DisciplineService } from 'src/app/services/discipline.service';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { AdditionalDisciplineInfoComponent } from './dialogs/additional-discipline-info/additional-discipline-info.component';

@Component({
  selector: 'app-disciplines-table',
  templateUrl: './disciplines-table.component.html',
  styleUrls: ['./disciplines-table.component.scss'],
})
export class DisciplinesTableComponent implements OnInit {
  public columnList: IColumnInfo[];

  public actionButtons: ButtonItem[];

  public additionalFilters: AdditionalFilterInfo[] = [];

  readonly STORAGE_KEY = 'DEPARTMENT_HEAD_DISCIPLINES_TABLE';

  constructor(
    public loaderService: LoaderService,
    private disciplineService: DisciplineService,
    private authenticationService: AuthenticationService,
    private curriculumService: CurriculumService,
    private dialog: MatDialog,
    private educationalProgramService: EducationalProgramService
  ) {}

  ngOnInit(): void {
    this.columnList = [
      {
        field: 'name',
        label: 'Название',
        visible: true,
        propertyName: 'Name',
        width: '650px',
      },
      {
        field: 'studyStartingYear',
        label: 'Год подготовки',
        width: '400px',
        visible: true,
      },

      {
        field: 'status',
        label: 'Статус',
        visible: true,

        getRowStyle: this.getStatusStyle,
        getRowInfo: this.getStatusDescription,
      },
    ];

    this.actionButtons = [
      {
        name: 'Подробная информация',
        isMethodObservable: false,
        onClick: this.showAdditionalDisciplineInfo,
      },
      {
        name: 'Отправить на разработку',
        isMethodObservable: true,
        onClick: this.checkDisciplineStatus,
        getButtonInfo: this.getDescriptionByStatus,
      },
    ];

    if (!this.loaderService.isShow) {
      this.loaderService.show();
    }

    this.curriculumService
      .getAllYearsByDepartmentId(
        this.authenticationService.userValue.departmentId
      )
      .subscribe((_) => {
        this.additionalFilters = [
          {
            formName: 'studyStartingYear',
            name: 'Год подготовки',
            values: getLookupModelsFromModelArray(_, 'studyStartingYear'),
            selectWidth: '250px',
          },
          {
            formName: 'status',
            name: 'Статус',
            values: getLookupModelsFromObjectsArray(
              Status.toKeyValuePair(),
              'number',
              'description'
            ),
            selectWidth: '200px',
          },
        ];
        this.loaderService.hide();
      });
  }

  checkDisciplineStatus = (
    departmentHeadDiscipline: DepartmentHeadDiscipline
  ): Observable<void> => {
    this.loaderService.show();
    if (departmentHeadDiscipline.status === Status.NotExist) {
      return this.checkEducationalProgramExist(
        departmentHeadDiscipline.disciplineId
      );
    }

    return this.disableEducationalProgram(
      departmentHeadDiscipline.disciplineId
    );
  };

  public disableEducationalProgram(id: number): Observable<void> {
    const request: ChangeDisciplineStatusRequest = {
      id: id,
      status: Status.NotExist,
    };

    return this.disciplineService.changeStatus(request).pipe(
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }

  public checkEducationalProgramExist(id: number): Observable<void> {
    return this.disciplineService.isEducationalProgramExist(id).pipe(
      mergeMap((isExist) => {
        if (isExist) {
          const request: ChangeDisciplineStatusRequest = {
            id: id,
            status: Status.NotStarted,
          };

          return this.disciplineService.changeStatus(request);
        }

        return this.educationalProgramService.createDefaultProgram(id);
      }),
      finalize(() => {
        this.loaderService.hide();
      })
    );
  }

  public getDescriptionByStatus = (
    discipline: DepartmentHeadDiscipline
  ): string => {
    return discipline.status === Status.NotExist
      ? 'Отправить на разработку'
      : 'Закрыть РП';
  };

  public getStatusStyle = (status: Status): string => {
    return Status.STATUS_STYLES.get(status) + ' label';
  };

  public getStatusDescription = (status: Status): string => {
    return Status.STATUS_DESCRIPTION.get(status);
  };

  public getAllWithPagination = (
    data: any
  ): Observable<PaginatedResult<DepartmentHeadDiscipline[]>> => {
    let status;

    if (data.additionalFilters) {
      status = data.additionalFilters.status;
    }

    return this.disciplineService.getByDepartmetIdWithPagination(
      data.currentPage,
      data.itemsPerPage,
      {
        disciplineName: data.searchString,
        departmentId: this.authenticationService.userValue.departmentId,
        status: status !== undefined ? status : null,
        studyStartingYear: data.additionalFilters?.studyStartingYear || null,
        sortDirection: data.sortDirection,
        propertyName: data.propertyName,
      }
    );
  };

  showAdditionalDisciplineInfo = (
    departmentHeadDiscipline: DepartmentHeadDiscipline
  ): void => {
    this.loaderService.show();
    this.disciplineService
      .getAdditionalDisciplineInfoById(departmentHeadDiscipline.disciplineId)
      .subscribe((_) => {
        this.loaderService.hide();

        const disciplineInfo: {
          discipline: AdditionalDisciplineInfo;
          disciplineId: number;
        } = {
          discipline: _,
          disciplineId: departmentHeadDiscipline.disciplineId,
        };

        const dialogRef = this.dialog.open(AdditionalDisciplineInfoComponent, {
          data: disciplineInfo,

          disableClose: false,
        });
      });

    // dialogRef.afterClosed().subscribe((result) => {
    //   if (result) {
    //     this.getAll();
    //   }
    // });
  };
}
