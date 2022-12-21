import { Component, OnInit } from '@angular/core';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { IndicatorWithLevels } from 'src/app/models/Response/IndicatorWithLevels';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { IndicatorService } from 'src/app/services/indicator.service';

@Component({
  selector: 'app-competence-formation-levels',
  templateUrl: './competence-formation-levels.component.html',
  styleUrls: ['./competence-formation-levels.component.scss'],
})
export class CompetenceFormationLevelsComponent implements OnInit {
  public competenceFormationLevelsSpinner = 'competenceFormationLevelsSpinner';

  public indicators: IndicatorWithLevels[];

  constructor(
    public educationalProgramData: EducationalProgramDataService,
    public spinnerService: SpinnerService,
    private indicatorService: IndicatorService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  public getAll(): void {
    this.spinnerService.showSpinner(this.competenceFormationLevelsSpinner);

    this.indicatorService
      .getIndicatorWithLevelsByProgramId(this.educationalProgramData.id)
      .subscribe((_) => {
        this.indicators = _;
        this.spinnerService.hideSpinner(this.competenceFormationLevelsSpinner);
      });
  }
}
