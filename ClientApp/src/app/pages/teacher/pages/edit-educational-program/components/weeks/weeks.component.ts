import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { GetWeeksByModuleNumberRequest } from 'src/app/models/Request/GetWeeksByModuleNumberRequest';
import { UpdateWeekRequest } from 'src/app/models/Request/UpdateWeekRequest';
import { Week } from 'src/app/models/Response/Week';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { WeekService } from 'src/app/services/week.service';
import { EditWeekDialogComponent } from '../../dialogs/edit-week-dialog/edit-week-dialog.component';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

@Component({
  selector: 'app-weeks',
  templateUrl: './weeks.component.html',
  styleUrls: ['./weeks.component.scss'],
})
export class WeeksComponent implements OnInit {
  @Input()
  public semesterId: number;

  @Input()
  public moduleNumber: number;

  displayedColumns: string[] = ['knowledgeControlForm', 'maxMark'];

  public modulesNumber: number[];

  public isLaboratoryClasses: boolean;
  public isPracticalClasses: boolean;

  public weeks: Week[];

  public isSpinnerWork: boolean = false;
  public weeksLayerSpinner: string = 'weeksLayerSpinner';

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    public educationalProgramData: EducationalProgramDataService,
    private weekService: WeekService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.isSpinnerWork = true;
    this.spinner.show(this.weeksLayerSpinner);
    this.getAll();
  }

  getAll() {
    const request: GetWeeksByModuleNumberRequest = {
      educationalProgramId: this.educationalProgramData.id,
      semesterId: this.semesterId,
      moduleNumber: this.moduleNumber,
    };

    this.weekService.getWeeksByModuleNumber(request).subscribe((_) => {
      this.weeks = _;
      this.spinner.hide(this.weeksLayerSpinner);
      this.isSpinnerWork = false;
    });
  }

  enableEditing(week: Week) {
    const dialogRef = this.dialog.open(EditWeekDialogComponent, {
      data: week,
      minWidth: '550px',
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result: UpdateWeekRequest) => {
      if (result) {
        this.isSpinnerWork = true;
        this.spinner.show(this.weeksLayerSpinner);
        this.weekService.updateWeek(result).subscribe((_) => {
          this.openSnackBar('Successfully update !!!');
          this.getAll();
        });
      }
    });
  }

  public openSnackBar(message: string) {
    this.snackBar.open(message, 'Ок', {
      duration: 3000,
    });
  }
}
