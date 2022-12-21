import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BehaviorSubject, forkJoin, Observable, of } from 'rxjs';
import { LessonType } from 'src/app/models/Enums/LessonType';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { EditWeekDialogComponent } from '../edit-week-dialog/edit-week-dialog.component';
import { LessonService } from 'src/app/services/lesson.service';
import { TrainingCourseFormService } from 'src/app/services/training-course-form.service';
import { AddLessonsToTrainingCourseFormRequest } from 'src/app/models/Request/AddLessonsToTrainingCourseFormRequest';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { Lesson } from 'src/app/models/Response/Lesson';
import { TrainingCourseForm } from 'src/app/models/Response/TrainingCourseForm';

@Component({
  selector: 'app-edit-training-course-form-dialog',
  templateUrl: './edit-training-course-form-dialog.component.html',
  styleUrls: ['./edit-training-course-form-dialog.component.scss'],
})
export class EditTrainingCourseFormDialogComponent implements OnInit {
  public form: FormGroup;

  public classTypes: { type: LessonType; name: string }[] = [
    { type: LessonType.Lecture, name: 'Лекции' },
  ];

  public isSpinnerWork: boolean = false;
  public editTrainingCourseFormLayerSpinner: string =
    'editTrainingCourseFormLayerSpinner';

  public trainingCourseForms: TrainingCourseForm[];

  public classes: BehaviorSubject<Lesson[]> = new BehaviorSubject<Lesson[]>([]);

  public toClasses: BehaviorSubject<Lesson[]> = new BehaviorSubject<Lesson[]>(
    []
  );

  get classType(): { type: LessonType; name: string } {
    return this.classTypes.find(
      (_) => _.type === this.form.get('classType').value
    );
  }

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<EditWeekDialogComponent>,
    public educationalProgramData: EducationalProgramDataService,
    private lessonService: LessonService,
    private trainingCourseFormService: TrainingCourseFormService,
    private loader: LoaderService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.getNeededData();
    this.getClassesInfo();

    this.form.get('classType').setValue(LessonType.Lecture);
  }

  getNeededData() {
    if (this.educationalProgramData.isPracticalLessons) {
      this.classTypes.push({
        type: LessonType.Practical,
        name: 'Практические',
      });
    }

    if (this.educationalProgramData.isLaboratoryLessons) {
      this.classTypes.push({
        type: LessonType.Laboratory,
        name: 'Лабораторные',
      });
    }

    this.loader.show();
    this.trainingCourseFormService
      .getAllWithoutLessons(this.educationalProgramData.id)
      .subscribe((_) => {
        this.trainingCourseForms = _;

        this.loader.hide();
      });
  }

  getClassesInfo() {
    this.form.get('classType').valueChanges.subscribe((data) => {
      this.toClasses.next([]);

      let lessonsObservable: Observable<Lesson[]>;

      switch (data) {
        case LessonType.Laboratory:
          this.lessonService;

          lessonsObservable =
            this.lessonService.getAllLessonsWithoutTrainingCourseForm(
              this.educationalProgramData.id,
              LessonType.Laboratory
            );
          break;

        case LessonType.Lecture:
          lessonsObservable =
            this.lessonService.getAllLessonsWithoutTrainingCourseForm(
              this.educationalProgramData.id,
              LessonType.Lecture
            );
          break;

        case LessonType.Practical:
          lessonsObservable =
            this.lessonService.getAllLessonsWithoutTrainingCourseForm(
              this.educationalProgramData.id,
              LessonType.Practical
            );
          break;
      }

      this.isSpinnerWork = true;
      this.spinner.show(this.editTrainingCourseFormLayerSpinner);
      lessonsObservable.subscribe((_) => {
        this.classes.next(_);
        this.isSpinnerWork = false;
        this.spinner.hide(this.editTrainingCourseFormLayerSpinner);
      });
    });

    this.form.get('fromClassNumber').valueChanges.subscribe((data) => {
      const classes: Lesson[] = this.classes.value.filter((_) => _.id >= data);

      this.toClasses.next(classes);
    });
  }

  getClasses() {
    this.lessonService
      .getAllLessonsWithoutTrainingCourseForm(
        this.educationalProgramData.id,
        LessonType.Lecture
      )
      .subscribe((_) => this.classes.next(_));
  }

  initForm() {
    this.form = this.fb.group({
      trainingCourseFormId: ['', Validators.compose([Validators.required])],
      classType: ['', Validators.compose([Validators.required])],
      fromClassNumber: ['', Validators.compose([Validators.required])],
      toClassNumber: ['', Validators.compose([Validators.required])],
    });
  }

  closeDialog() {
    const trainingCourseFormId: number = this.form.get(
      'trainingCourseFormId'
    ).value;
    const classType: LessonType = this.form.get('classType').value;
    const fromClassNumber: number = this.form.get('fromClassNumber').value;
    const toClassNumber: number = this.form.get('toClassNumber').value;

    var request: AddLessonsToTrainingCourseFormRequest = {
      trainingCourseFormId: trainingCourseFormId,
      lessonType: classType,
      fromLessonId: fromClassNumber,
      toLessonId: toClassNumber,
      educationalProgramId: this.educationalProgramData.id,
    };

    this.trainingCourseFormService
      .addLessonsToTrainingCourseForm(request)
      .subscribe((_) => this.dialogRef.close(true));
  }
}
