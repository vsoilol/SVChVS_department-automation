import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BehaviorSubject } from 'rxjs';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import {
  getLookupModelsFromObjectsArray,
  LookupModel,
} from 'src/app/lib/main/picker/models/lookup-model';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { UpdateDisciplineTeachersRequest } from 'src/app/models/Request/UpdateDisciplineTeachersRequest';
import { AdditionalDisciplineInfo } from 'src/app/models/Response/AdditionalDisciplineInfo';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DisciplineService } from 'src/app/services/discipline.service';
import { TeacherService } from 'src/app/services/teacher.service';

@Component({
  selector: 'app-additional-discipline-info',
  templateUrl: './additional-discipline-info.component.html',
  styleUrls: ['./additional-discipline-info.component.scss'],
})
export class AdditionalDisciplineInfoComponent implements OnInit {
  public form: FormGroup;

  public discipline: AdditionalDisciplineInfo;

  public teachers: LookupModel[];

  public selectedTeachers: LookupModel[];

  public spinnerName: string = 'additionalDisciplineInfoSpinner';

  public isEdit: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  get teachersSelectedValue(): number[] {
    return this.form.get('teachers').value as number[];
  }

  constructor(
    public dialogRef: MatDialogRef<AdditionalDisciplineInfoComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { discipline: AdditionalDisciplineInfo; disciplineId: number },
    public loaderService: LoaderService,
    private disciplineService: DisciplineService,
    private teacherService: TeacherService,
    private authenticationService: AuthenticationService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.discipline = this.data.discipline;
    this.initForm();
  }

  private getData() {
    this.teacherService
      .getTeachersFullNameByDisciplineId(this.data.disciplineId)
      .subscribe((_) => {
        this.discipline.teachers = _;
        this.initForm();

        this.spinnerService.hideSpinner(this.spinnerName);
      });
  }

  private initForm() {
    this.form = new FormGroup({
      teachers: new FormControl(),
    });

    this.selectedTeachers = getLookupModelsFromObjectsArray(
      this.discipline.teachers,
      'id',
      'fullName'
    );
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public editTeachersDiscipline(): void {
    this.spinnerService.showSpinner(this.spinnerName);
    this.teacherService
      .getTeachersByDepartmentId(
        this.authenticationService.userValue.departmentId
      )
      .subscribe((_) => {
        this.teachers = getLookupModelsFromObjectsArray(_, 'id', 'fullName');

        this.isEdit.next(true);
        this.spinnerService.hideSpinner(this.spinnerName);
      });
  }

  public updateTeachersDiscipline(): void {
    const updateRequest: UpdateDisciplineTeachersRequest = {
      disciplineId: this.data.disciplineId,
      teachersId: this.teachersSelectedValue,
    };

    this.spinnerService.showSpinner(this.spinnerName);
    this.disciplineService
      .updateDisciplineTeachers(updateRequest)
      .subscribe(() => {
        this.isEdit.next(false);
        this.getData();
      });
  }
}
