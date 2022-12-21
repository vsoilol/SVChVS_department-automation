import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Observable, of } from 'rxjs';
import {
  DocumentInfo,
  DOCUMENTS_INFO,
  DocumentType,
} from 'src/app/lib/documentInfo/DocumentInfo';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { Status } from 'src/app/models/Enums/Status';
import { IColumnInfo } from 'src/app/models/TableInfo/IColumnInfo';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { saveAs } from 'file-saver';
import { Order } from 'src/app/models/Enums/Order';
import { EducationalProgramBriefInfo } from 'src/app/models/Response/EducationalProgramBriefInfo';
import { EducationalProgramsFilter } from 'src/app/models/Request/Filters/EducationalProgramsFilter';
import { ButtonItem } from 'src/app/models/TableInfo/ButtonItem';
import { PaginatedResult } from 'src/app/models/Response/Common/PaginatedResult';

@Component({
  selector: 'app-educational-program',
  templateUrl: './educational-program.component.html',
  styleUrls: ['./educational-program.component.scss'],
})
export class EducationalProgramComponent implements OnInit {
  public columnList: IColumnInfo[];

  public actionButtons: ButtonItem[];

  readonly STORAGE_KEY = 'EDUCATIONAL_PROGRAMS_TABLE';

  constructor(
    private educationalProgramService: EducationalProgramService,
    public loaderService: LoaderService,
    private authenticationService: AuthenticationService,
    private router: Router,
    private loader: LoaderService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.columnList = [
      {
        field: 'status',
        label: 'Статус',
        visible: true,
        propertyName: 'Discipline.Status',
        getRowStyle: this.getStatusStyle,
        getRowInfo: this.getStatusDescription,
      },
      {
        field: 'disciplineName',
        label: 'Дисциплина',
        visible: true,
        propertyName: 'Discipline.Name',
      },
    ];

    this.actionButtons = [
      {
        name: 'Изменить',
        isMethodObservable: false,
        onClick: this.editEducationalProgramFunction,
      },
      {
        name: 'Скачать',
        isMethodObservable: false,
        onClick: this.downloadEducationalProgramFunction,
      },
    ];
  }

  private editEducationalProgramFunction = (educationalProgram: any): void => {
    this.router.navigate([
      '/teacher/edit-educational-program',
      educationalProgram.id,
    ]);
  };

  public getAllWithPagination = (
    data: any
  ): Observable<PaginatedResult<EducationalProgramBriefInfo[]>> => {
    return this.educationalProgramService.getWithPagination(
      data.currentPage,
      data.itemsPerPage,
      {
        propertyName: data.propertyName,
        sortDirection: data.sortDirection,
        disciplineName: data.searchString,
        userId: this.authenticationService.userValue.id,
      }
    );
  };

  public getStatusStyle = (status: Status): string => {
    return Status.STATUS_STYLES.get(status) + ' label';
  };

  public getStatusDescription = (status: Status): string => {
    return Status.STATUS_DESCRIPTION.get(status);
  };

  private downloadEducationalProgramFunction = (
    educationalProgram: any
  ): void => {
    const wordDocumentInfo: DocumentInfo = DOCUMENTS_INFO.find(
      (_) => _.documentType == DocumentType.Word
    );

    const joinedWithObjectForm$ = forkJoin({
      wordDocument: this.educationalProgramService.getWordDocument(
        educationalProgram.id
      ),
    });

    this.loader.show();

    joinedWithObjectForm$.subscribe(
      (result) => {
        let date = new Date();
        const data: Blob = new Blob([result.wordDocument], {
          type: wordDocumentInfo.contentType,
        });
        saveAs(
          data,
          `УП_${educationalProgram.disciplineName}` +
            wordDocumentInfo.fileExtension
        );
        this.loader.hide();
      },
      (error) => {
        this.loader.hide();
      }
    );
  };
}
