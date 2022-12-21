import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { BehaviorSubject, forkJoin, of } from 'rxjs';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { LessonType } from 'src/app/models/Enums/LessonType';
import { UpdateWeekRequest } from 'src/app/models/Request/UpdateWeekRequest';
import { KnowledgeAssessment } from 'src/app/models/Response/KnowledgeAssessment';
import { KnowledgeControlForm } from 'src/app/models/Response/KnowledgeControlForm';
import { Lesson } from 'src/app/models/Response/Lesson';
import { Week } from 'src/app/models/Response/Week';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { KnowledgeControlFormService } from 'src/app/services/knowledge-control-form.service';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
  selector: 'app-edit-week-dialog',
  templateUrl: './edit-week-dialog.component.html',
  styleUrls: ['./edit-week-dialog.component.scss'],
})
export class EditWeekDialogComponent implements OnInit {
  public form: FormGroup;

  public lectures: Lesson[];
  public practicalClasses: Lesson[];
  public laboratoryClasses: Lesson[];

  public knowledgeControlForms: KnowledgeControlForm[];

  faTrash = faTrash;
  public isSpinnerWork: boolean = false;

  displayedColumns: string[] = ['knowledgeControlForm', 'maxMark', 'action'];
  dataSource = new BehaviorSubject<AbstractControl[]>([]);

  get knowledgeControlFormAssesmentsFormArray(): FormArray {
    return this.form.get('knowledgeControlFormAssesments') as FormArray;
  }

  constructor(
    public dialogRef: MatDialogRef<EditWeekDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public week: Week,
    private fb: FormBuilder,
    public educationalProgramData: EducationalProgramDataService,
    private lessonService: LessonService,
    private knowledgeControlFormService: KnowledgeControlFormService,
    private loader: LoaderService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.getAllLectures();

    this.updateView();
  }

  closeDialog() {
    const lecture: Lesson = this.lectures.find(
      (_) => _.id === this.form.get('lectureId').value
    );

    const knowledgeControlFormAssesments: KnowledgeAssessment[] =
      this.knowledgeControlFormAssesmentsFormArray.controls.map((formGroup) => {
        const knowledgeControlForm: KnowledgeControlForm =
          this.knowledgeControlForms.find(
            (_) => _.id === formGroup.get('knowledgeControlFormId').value
          );

        return {
          id: knowledgeControlForm.id,
          name: knowledgeControlForm.name,
          shortName: knowledgeControlForm.shortName,
          maxMark: formGroup.get('maxMark').value,
        };
      });

    const updateWeekRequest: UpdateWeekRequest = {
      id: this.week.id,
      independentWorkHours: this.form.get('independentWorkHours').value,
      lessons: [lecture],
      knowledgeAssessments: knowledgeControlFormAssesments,
    };

    if (this.educationalProgramData.isLaboratoryLessons) {
      const laboratoryLesson: Lesson = this.laboratoryClasses.find(
        (_) => _.id === this.form.get('laboratoryLessonId').value
      );

      updateWeekRequest.lessons.push(laboratoryLesson);
    }

    if (this.educationalProgramData.isPracticalLessons) {
      const practicalLesson: Lesson = this.practicalClasses.find(
        (_) => _.id === this.form.get('practicalLessonId').value
      );

      updateWeekRequest.lessons.push(practicalLesson);
    }

    this.dialogRef.close(updateWeekRequest);
  }

  initForm() {
    this.form = this.fb.group({
      independentWorkHours: [
        this.week.independentWorkHours || 0,
        Validators.compose([Validators.required]),
      ],
      lectureId: [
        this.week.lecture?.id || '',
        Validators.compose([Validators.required]),
      ],
      knowledgeControlFormAssesments: this.fb.array(
        this.week.knowledgeAssessments.map((data) => {
          return this.fb.group({
            knowledgeControlFormId: [
              data?.id || '',
              Validators.compose([Validators.required]),
            ],
            maxMark: [
              data.maxMark || '',
              Validators.compose([Validators.required]),
            ],
          });
        }) || []
      ),
    });

    if (this.educationalProgramData.isPracticalLessons) {
      this.form.addControl(
        'practicalLessonId',
        this.fb.control(
          this.week.practicalLesson?.id || '',
          Validators.compose([Validators.required])
        )
      );
    }

    if (this.educationalProgramData.isLaboratoryLessons) {
      this.form.addControl(
        'laboratoryLessonId',
        this.fb.control(
          this.week.laboratoryLesson?.id || '',
          Validators.compose([Validators.required])
        )
      );
    }
  }

  generateKnowledgeControlFormAssesment(): FormGroup {
    return this.fb.group({
      knowledgeControlFormId: ['', Validators.compose([Validators.required])],
      maxMark: ['', Validators.compose([Validators.required])],
    });
  }

  getAllLectures() {
    const joinedWithObjectForm$ = forkJoin({
      practicalLessons: this.educationalProgramData.isPracticalLessons
        ? this.lessonService.getAllLessonsByProgramId(
            this.educationalProgramData.id,
            LessonType.Practical
          )
        : of(null),
      laboratoryLessons: this.educationalProgramData.isLaboratoryLessons
        ? this.lessonService.getAllLessonsByProgramId(
            this.educationalProgramData.id,
            LessonType.Laboratory
          )
        : of(null),
      knowledgeControlForm: this.knowledgeControlFormService.getAll(),
      lectures: this.lessonService.getAllLessonsByProgramId(
        this.educationalProgramData.id,
        LessonType.Lecture
      ),
    });

    this.isSpinnerWork = true;
    this.loader.show();
    joinedWithObjectForm$.subscribe((result) => {
      this.practicalClasses = result.practicalLessons;
      this.laboratoryClasses = result.laboratoryLessons;
      this.knowledgeControlForms = result.knowledgeControlForm;
      this.lectures = result.lectures;
      if (this.week.lecture) {
        this.lectures.push(this.week.lecture);
      }
      this.loader.hide();
      this.isSpinnerWork = false;
    });
  }

  updateView() {
    this.dataSource.next(this.knowledgeControlFormAssesmentsFormArray.controls);
  }

  public addFromGroupControl(): void {
    this.knowledgeControlFormAssesmentsFormArray.push(
      this.generateKnowledgeControlFormAssesment()
    );
    this.updateView();
  }

  public deleteFromGroupControl(index: number): void {
    this.knowledgeControlFormAssesmentsFormArray.removeAt(index);
    this.updateView();
  }
}
