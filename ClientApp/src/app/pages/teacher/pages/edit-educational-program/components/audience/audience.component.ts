import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';
import { AudienceBriefInfo } from 'src/app/models/Response/AudienceBriefInfo';
import { UpdateAudiences } from 'src/app/models/Request/UpdateAudiences';
import { AudienceService } from 'src/app/services/audience.service';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';

@Component({
  selector: 'app-audience',
  templateUrl: './audience.component.html',
  styleUrls: ['./audience.component.scss'],
})
export class AudienceComponent implements OnInit {
  public audienceLayerSpinner: string = 'audienceLayerSpinner';

  public isEdit: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public audiences: AudienceBriefInfo[];

  public checkedAudiences: { completed: boolean; info: AudienceBriefInfo }[];

  constructor(
    private audienceService: AudienceService,
    private educationalProgram: EducationalProgramDataService,
    private educationalProgramService: EducationalProgramService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.getAudiencesInfo();
  }

  private getAudiencesInfo(): void {
    this.spinnerService.showSpinner(this.audienceLayerSpinner);

    this.audienceService
      .getAllByProgramId(this.educationalProgram.id)
      .subscribe((_) => {
        this.audiences = _;
        this.spinnerService.hideSpinner(this.audienceLayerSpinner);
      });
  }

  public editAudiences(): void {
    this.spinnerService.showSpinner(this.audienceLayerSpinner);

    this.audienceService.getAll().subscribe((_) => {
      this.checkedAudiences = _.map((audience) => {
        const isInclude: boolean = this.audiences.some(
          (e) => e.id === audience.id
        );
        return { completed: isInclude, info: audience };
      });
      this.spinnerService.hideSpinner(this.audienceLayerSpinner);
      this.isEdit.next(true);
    });
  }

  public updateAudiencesInfo(): void {
    this.spinnerService.showSpinner(this.audienceLayerSpinner);
    const ids = this.checkedAudiences
      .filter((_) => _.completed)
      .map((_) => _.info.id);

    const request: UpdateAudiences = {
      audienceIds: ids,
      educationalProgramId: this.educationalProgram.id,
    };

    this.educationalProgramService
      .updateAudiencesInfo(request)
      .subscribe((_) => {
        this.spinnerService.hideSpinner(this.audienceLayerSpinner);
        this.isEdit.next(false);
        this.getAudiencesInfo();
      });
  }
}
