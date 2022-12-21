import { Component, OnInit, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { faPencilAlt, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { LessonType } from 'src/app/models/Enums/LessonType';
import { CreateLessonRequest } from 'src/app/models/Request/CreateLessonRequest';
import { UpdateLessonRequest } from 'src/app/models/Request/UpdateLessonRequest';
import { LecturesBriefInfo } from 'src/app/models/Response/LecturesBriefInfo';
import { Lesson } from 'src/app/models/Response/Lesson';
import { CompetenceService } from 'src/app/services/competence.service';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { LessonService } from 'src/app/services/lesson.service';
import { EditLectureDialogComponent } from '../../dialogs/edit-lecture-dialog/edit-lecture-dialog.component';

@Component({
  selector: 'app-lectures',
  templateUrl: './lectures.component.html',
  styleUrls: ['./lectures.component.scss'],
})
export class LecturesComponent implements OnInit {
  public lectures: BehaviorSubject<LecturesBriefInfo[]> = new BehaviorSubject<
    LecturesBriefInfo[]
  >([]);

  public faPencil = faPencilAlt;
  public faTrash = faTrash;

  public mainLecturesLayerSpinner: string = 'lecturesLayerSpinner';

  public displayedColumns: string[] = [
    'number',
    'name',
    'hours',
    'competences',
    'action',
  ];

  public lectureId: number;

  constructor(
    private modalService: NgbModal,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private lessonService: LessonService,
    private competenceService: CompetenceService,
    private educationalProgramDataService: EducationalProgramDataService,
    private spinner: NgxSpinnerService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  public getAll(): void {
    this.spinnerService.showSpinner(this.mainLecturesLayerSpinner);

    this.lessonService
      .getAllLecturesByProgramId(this.educationalProgramDataService.id)
      .subscribe((lessons) => {
        this.lectures.next(lessons);
        this.spinnerService.hideSpinner(this.mainLecturesLayerSpinner);
      });
  }

  public enableEdit(lecture: LecturesBriefInfo = null) {
    let id: number;

    if (!lecture) {
      id = 0;
    } else {
      id = lecture.id;
    }

    const dialogRef = this.dialog.open(EditLectureDialogComponent, {
      data: id,
      minWidth: '550px',
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result: Lesson) => {
      if (!result) {
        return;
      }

      this.spinnerService.showSpinner(this.mainLecturesLayerSpinner);

      if (result.id === 0) {
        const createRequest: CreateLessonRequest = {
          lesson: result,
          lessonType: LessonType.Lecture,
          competencesId: result.competencesId,
          educationalProgramId: this.educationalProgramDataService.id,
        };

        this.lessonService
          .createLesson(createRequest)
          .subscribe((_) => this.getAll());
      } else {
        const updateRequest: UpdateLessonRequest = {
          lesson: result,
          lessonType: LessonType.Lecture,
          competencesId: result.competencesId,
        };

        this.lessonService
          .updateLesson(updateRequest)
          .subscribe((_) => this.getAll());
      }
    });
  }

  openModal(content: TemplateRef<any>, lectureId: number): void {
    this.lectureId = lectureId;
    this.modalService.open(content, { backdrop: 'static', centered: true });
  }

  deleteLecture() {
    this.spinnerService.showSpinner(this.mainLecturesLayerSpinner);

    this.lessonService.deleteLesson(this.lectureId).subscribe((_) => {
      this.openSnackBar('Successfully deleted !!!');
      this.getAll();
    });
  }

  public openSnackBar(message: string) {
    this.snackBar.open(message, 'Ок', {
      duration: 3000,
    });
  }
}
