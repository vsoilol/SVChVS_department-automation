import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { UpdateCompetenceFormationLevelRequest } from 'src/app/models/Request/UpdateCompetenceFormationLevelRequest';
import { CompetenceFormationLevel } from 'src/app/models/Response/CompetenceFormationLevel';
import { EvaluationToolType } from 'src/app/models/Response/EvaluationToolType';
import { CompetenceFormationLevelService } from 'src/app/services/competence-formation-level.service';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { EvaluationToolService } from 'src/app/services/evaluation-tool.service';
import { EditWeekDialogComponent } from '../edit-week-dialog/edit-week-dialog.component';

@Component({
  selector: 'app-edit-competence-formation-level',
  templateUrl: './edit-competence-formation-level.component.html',
  styleUrls: ['./edit-competence-formation-level.component.scss'],
})
export class EditCompetenceFormationLevelComponent implements OnInit {
  public form: FormGroup;

  public evaluationToolTypes: EvaluationToolType[];

  public isLoaderWork: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<EditWeekDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public competenceFormationLevel: CompetenceFormationLevel,
    private fb: FormBuilder,
    private evaluationToolService: EvaluationToolService,
    public educationalProgramData: EducationalProgramDataService,
    private competenceFormationLevelService: CompetenceFormationLevelService,
    private loader: LoaderService
  ) {}

  ngOnInit(): void {
    this.getNeededData();
    this.initForm();
  }

  public closeDialog(): void {
    if (!this.form.valid) {
      return;
    }

    const updateRequest: UpdateCompetenceFormationLevelRequest = {
      id: this.competenceFormationLevel.id,
      factualDescription: this.form.get('factualDescription').value,
      learningOutcomes: this.form.get('learningOutcomes').value,
      evaluationToolTypeIds: this.form.get('evaluationToolTypeIds').value,
    };

    this.loader.show();
    this.competenceFormationLevelService
      .updateCompetenceFormationLevel(updateRequest)
      .subscribe((_) => {
        this.loader.hide();
        this.dialogRef.close(true);
      });
  }

  private getNeededData(): void {
    this.isLoaderWork = true;
    this.loader.show();

    this.evaluationToolService
      .getAllEvaluationToolTypeByProgramId(this.educationalProgramData.id)
      .subscribe((_) => {
        this.evaluationToolTypes = _;

        this.isLoaderWork = false;
        this.loader.hide();
      });
  }

  private initForm(): void {
    this.form = this.fb.group({
      factualDescription: [
        this.competenceFormationLevel.factualDescription || '',
        Validators.compose([Validators.required]),
      ],
      learningOutcomes: [
        this.competenceFormationLevel.learningOutcomes || '',
        Validators.compose([Validators.required]),
      ],
      evaluationToolTypeIds: [
        this.competenceFormationLevel.evaluationToolTypes.map((_) => _.id) ||
          '',
        Validators.compose([Validators.required]),
      ],
    });
  }
}
