import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { BehaviorSubject } from 'rxjs';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { Lesson } from 'src/app/models/Response/Lesson';
import { TrainingCourseForm } from 'src/app/models/Response/TrainingCourseForm';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { TrainingCourseFormService } from 'src/app/services/training-course-form.service';
import { EditTrainingCourseFormDialogComponent } from '../../dialogs/edit-training-course-form-dialog/edit-training-course-form-dialog.component';

@Component({
  selector: 'app-training-course-form',
  templateUrl: './training-course-form.component.html',
  styleUrls: ['./training-course-form.component.scss'],
})
export class TrainingCourseFormComponent implements OnInit {
  public trainingCourseFormLessons: BehaviorSubject<TrainingCourseForm[]> =
    new BehaviorSubject<TrainingCourseForm[]>([]);

  faTrash = faTrash;

  public trainingCourseFormLayerSpinner: string =
    'trainingCourseFormLayerSpinner';

  public columns = [
    {
      columnDef: 'trainingCourseForm',
      header: 'Форма проведения занятия',
      cell: (element: TrainingCourseForm) => `${element.name}`,
    },
    {
      columnDef: 'lectures',
      header: 'Лекции',
      cell: (element: TrainingCourseForm) =>
        `${this.getInfomationAboutNumbersClassClassroom(element.lectures)}`,
    },
  ];

  displayedColumns: BehaviorSubject<string[]> = new BehaviorSubject<string[]>(
    []
  );

  constructor(
    private dialog: MatDialog,
    public educationalProgramData: EducationalProgramDataService,
    private trainingCourseFormService: TrainingCourseFormService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.getNeededData();

    this.spinnerService.showSpinner(this.trainingCourseFormLayerSpinner);

    this.getAll();
  }

  private getNeededData() {
    if (this.educationalProgramData.isLaboratoryLessons) {
      this.columns.push({
        columnDef: 'laboratoryClasses',
        header: 'Лабораторная',
        cell: (element: TrainingCourseForm) =>
          `${this.getInfomationAboutNumbersClassClassroom(
            element.laboratoryLessons
          )}`,
      });
    }

    if (this.educationalProgramData.isPracticalLessons) {
      this.columns.push({
        columnDef: 'practicalClasses',
        header: 'Практическая',
        cell: (element: TrainingCourseForm) =>
          `${this.getInfomationAboutNumbersClassClassroom(
            element.practicalLessons
          )}`,
      });
    }

    this.columns.push({
      columnDef: 'action',
      header: 'Действия',
      cell: (element: TrainingCourseForm) => '',
    });
  }

  public deleteItem(element: TrainingCourseForm) {
    this.spinnerService.showSpinner(this.trainingCourseFormLayerSpinner);

    this.trainingCourseFormService
      .deleteLessonsFromTrainingCourseForm(
        this.educationalProgramData.id,
        element.id
      )
      .subscribe((_) => {
        this.getAll();
      });
  }

  private getAll(): void {
    this.trainingCourseFormService
      .getAllByProgramId(this.educationalProgramData.id)
      .subscribe((_) => {
        this.trainingCourseFormLessons.next(_);
        this.initTable();

        this.spinnerService.hideSpinner(this.trainingCourseFormLayerSpinner);
      });
  }

  private initTable() {
    this.displayedColumns.next(this.columns.map((c) => c.columnDef));
  }

  public getInfomationAboutNumbersClassClassroom(
    classClassroom: Lesson[]
  ): string {
    if (classClassroom?.length !== 0) {
      const firstNumber: number = classClassroom[0].number;
      const lastNumber: number =
        classClassroom[classClassroom.length - 1].number;
      return `${firstNumber} - ${lastNumber}`;
    }

    return '';
  }

  enableEditing() {
    const dialogRef = this.dialog.open(EditTrainingCourseFormDialogComponent, {
      minWidth: '550px',
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.getAll();
      }
    });
  }
}
