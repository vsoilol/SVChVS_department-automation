import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  faCheck,
  faPencilAlt,
  faTimes,
  faTrash,
} from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { LessonType } from 'src/app/models/Enums/LessonType';
import { CreateLessonRequest } from 'src/app/models/Request/CreateLessonRequest';
import { UpdateLessonRequest } from 'src/app/models/Request/UpdateLessonRequest';
import { Lesson } from 'src/app/models/Response/Lesson';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
  selector: 'app-laboratory-lessons',
  templateUrl: './laboratory-lessons.component.html',
  styleUrls: ['./laboratory-lessons.component.scss'],
})
export class LaboratoryLessonsComponent implements OnInit {
  form: FormGroup;
  public isDisable: boolean = false;

  displayedColumns: string[] = ['number', 'name', 'hours', 'action'];

  faPencil = faPencilAlt;
  faTrash = faTrash;
  faCheck = faCheck;
  faTimes = faTimes;

  public dataSource = new BehaviorSubject<Lesson[]>([]);
  private lessons: Lesson[];
  private lessonId: number;

  public laboratoryLayerSpinner: string = 'laboratoryLayerSpinner';

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    private snackBar: MatSnackBar,
    private lessonService: LessonService,
    private educationalProgramData: EducationalProgramDataService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.spinnerService.showSpinner(this.laboratoryLayerSpinner);

    this.getAll();
  }

  getAll() {
    this.lessonService
      .getAllLessonsByProgramId(
        this.educationalProgramData.id,
        LessonType.Laboratory
      )
      .subscribe((laboratories) => {
        this.lessons = laboratories.map((data) => {
          return {
            ...data,
            isEdit: false,
          };
        });
        this.dataSource.next(this.lessons);

        this.spinnerService.hideSpinner(this.laboratoryLayerSpinner);
      });

    this.initForm();
  }

  initForm() {
    this.form = this.fb.group({
      id: '',
      name: ['', Validators.compose([Validators.required])],
      number: ['', Validators.compose([Validators.required])],
      hours: ['', Validators.compose([Validators.required])],
      isEdit: [false, Validators.compose([Validators.required])],
    });
  }

  openModal(content: TemplateRef<any>, lessonId: number): void {
    this.lessonId = lessonId;
    this.modalService.open(content, { backdrop: 'static', centered: true });
  }

  enableEditing(item: Lesson) {
    this.isDisable = true;
    item.isEdit = true;
    this.form.patchValue(item);
  }

  public openSnackBar(message: string) {
    this.snackBar.open(message, 'Ок', {
      duration: 3000,
    });
  }

  cancelEditing(): void {
    this.isDisable = false;

    this.spinnerService.showSpinner(this.laboratoryLayerSpinner);
    this.getAll();
  }

  addEmptyLesson(): void {
    this.isDisable = true;
    const newControl: Lesson = {
      id: 0,
      name: '',
      number: this.lessons.length + 1,
      hours: 0,
      isEdit: true,
    };

    this.lessons.push(newControl);
    this.form.patchValue(newControl);
    this.dataSource.next(this.lessons);
  }

  updateLesson() {
    if (this.form.valid) {
      this.isDisable = false;

      if (this.form.get('id').value === 0) {
        const createRequest: CreateLessonRequest = {
          lesson: this.form.value,
          lessonType: LessonType.Laboratory,
          educationalProgramId: this.educationalProgramData.id,
        };

        this.spinnerService.showSpinner(this.laboratoryLayerSpinner);

        this.lessonService.createLesson(createRequest).subscribe((_) => {
          this.openSnackBar('Successfully updated !!!');
          this.getAll();
        });
      } else {
        const updateRequest: UpdateLessonRequest = {
          lesson: this.form.value,
          lessonType: LessonType.Laboratory,
        };

        this.spinnerService.showSpinner(this.laboratoryLayerSpinner);

        this.lessonService.updateLesson(updateRequest).subscribe((_) => {
          this.openSnackBar('Successfully updated !!!');
          this.getAll();
        });
      }
    }
  }

  deleteLesson() {
    this.spinnerService.showSpinner(this.laboratoryLayerSpinner);

    this.lessonService.deleteLesson(this.lessonId).subscribe((_) => {
      this.openSnackBar('Successfully deleted !!!');
      this.getAll();
    });
  }
}
