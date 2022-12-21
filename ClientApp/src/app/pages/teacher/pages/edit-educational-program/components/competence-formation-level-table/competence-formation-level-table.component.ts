import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { faPencilAlt, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { FormationLevel } from 'src/app/models/Enums/FormationLevel';
import { Competence } from 'src/app/models/Response/Competence';
import { CompetenceFormationLevel } from 'src/app/models/Response/CompetenceFormationLevel';
import { CompetenceFormationLevelService } from 'src/app/services/competence-formation-level.service';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { EditCompetenceFormationLevelComponent } from '../../dialogs/edit-competence-formation-level/edit-competence-formation-level.component';

@Component({
  selector: 'app-competence-formation-level-table',
  templateUrl: './competence-formation-level-table.component.html',
  styleUrls: ['./competence-formation-level-table.component.scss'],
})
export class CompetenceFormationLevelTableComponent implements OnInit {
  @Output()
  public getAll = new EventEmitter<any>();

  faPencil = faPencilAlt;
  faTrash = faTrash;

  public _dataSource: BehaviorSubject<CompetenceFormationLevel[]> =
    new BehaviorSubject<CompetenceFormationLevel[]>([]);

  @Input()
  public set dataSource(val: CompetenceFormationLevel[]) {
    this._dataSource.next(val);
  }

  private levels: { formationLevel: FormationLevel; title: string }[] = [
    {
      formationLevel: FormationLevel.Threshold,
      title: 'Пороговый уровень',
    },
    {
      formationLevel: FormationLevel.Advanced,
      title: 'Продвинутый уровень',
    },
    {
      formationLevel: FormationLevel.High,
      title: 'Высокий уровень',
    },
  ];

  displayedColumns: string[] = [
    'formationLevel',
    'factualDescription',
    'learningOutcomes',
    'evaluationToolTypes',
    'action',
  ];

  constructor(
    private dialog: MatDialog,
    public educationalProgramData: EducationalProgramDataService,
    private competenceFormationLevelService: CompetenceFormationLevelService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {}

  public getFormationLevelType(formationLevel: FormationLevel): string {
    return this.levels.find((_) => _.formationLevel == formationLevel).title;
  }

  enableEditing(data: CompetenceFormationLevel) {
    const dialogRef = this.dialog.open(EditCompetenceFormationLevelComponent, {
      data: data,
      minWidth: '550px',
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.getAll.emit();
      }
    });
  }
}
