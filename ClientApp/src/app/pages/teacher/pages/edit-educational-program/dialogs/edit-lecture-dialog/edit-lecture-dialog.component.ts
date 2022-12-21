import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { forkJoin, of } from 'rxjs';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { Competence } from 'src/app/models/Response/Competence';
import { LectureDto } from 'src/app/models/Response/Lecture';
import { CompetenceService } from 'src/app/services/competence.service';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
  selector: 'app-edit-lecture-dialog',
  templateUrl: './edit-lecture-dialog.component.html',
  styleUrls: ['./edit-lecture-dialog.component.scss'],
})
export class EditLectureDialogComponent implements OnInit {
  public form: FormGroup;

  public competences: Competence[];

  public lecture: LectureDto;

  isSpinnerWork: boolean;
  lecturesLayerSpinner: 'lecturesLayerSpinner';

  constructor(
    public dialogRef: MatDialogRef<EditLectureDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public lectureId: number,
    private fb: FormBuilder,
    private competenceService: CompetenceService,
    private educationalProgramDataService: EducationalProgramDataService,
    private loader: LoaderService,
    private lessonService: LessonService
  ) {}

  ngOnInit(): void {
    const joinedWithObjectForm$ = forkJoin({
      competences: this.competenceService.getAllByProgramId(
        this.educationalProgramDataService.id
      ),
      lecture:
        this.lectureId !== 0
          ? this.lessonService.getLectureById(this.lectureId)
          : of(null),
    });

    this.loader.show();
    joinedWithObjectForm$.subscribe((result) => {
      this.competences = result.competences;

      if (!result.lecture) {
        this.lecture = {
          id: 0,
          number: 0,
          name: '',
          hours: 0,
          content: '',
          competencesId: [],
        };
      } else {
        this.lecture = result.lecture;
      }

      this.initForm();
      this.loader.hide();
    });
  }

  private initForm() {
    this.form = this.fb.group({
      name: [this.lecture.name, Validators.compose([Validators.required])],
      content: [
        this.lecture.content,
        Validators.compose([Validators.required]),
      ],
      competences: [
        this.lecture.competencesId,
        Validators.compose([Validators.required]),
      ],
      hours: [
        this.lecture.hours,
        Validators.compose([
          Validators.required,
          Validators.min(0),
          Validators.max(4),
        ]),
      ],
    });
  }

  closeDialog() {
    if (!this.form.valid) {
      return;
    }

    this.lecture.name = this.form.get('name').value;
    this.lecture.content = this.form.get('content').value;
    this.lecture.competencesId = this.form.get('competences').value;
    this.lecture.hours = this.form.get('hours').value;

    this.dialogRef.close(this.lecture);
  }
}
