import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { LiteratureType } from 'src/app/models/Enums/LiteratureType';
import { Status } from 'src/app/models/Enums/Status';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { InformationBlockService } from 'src/app/services/information-block.service';
import { ChangeProgramStatusRequest } from 'src/app/models/Request/ChangeProgramStatusRequest';
import { EducationalProgram } from 'src/app/models/Response/EducationalProgram';
import { AdditionalBlocksName } from 'src/app/models/Response/AdditionalBlocksName';

@Component({
  selector: 'app-edit-educational-program',
  templateUrl: './edit-educational-program.component.html',
  styleUrls: ['./edit-educational-program.component.scss'],
})
export class EditEducationalProgramComponent implements OnInit {
  public educationalProgram: EducationalProgram;

  public additionalBlocksName: AdditionalBlocksName[];

  public get literatureType(): typeof LiteratureType {
    return LiteratureType;
  }

  constructor(
    private route: ActivatedRoute,
    private educationalProgramService: EducationalProgramService,
    private loader: LoaderService,
    private informationBlockService: InformationBlockService,
    private educationalProgramDataService: EducationalProgramDataService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.educationalProgramDataService.id = parseInt(
      this.route.snapshot.paramMap.get('id')
    );

    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });

    this.getData();
  }

  private getData(): void {
    this.loader.show();
    const joinedWithObjectForm$ = forkJoin({
      educationalProgram: this.educationalProgramService.getById(
        this.educationalProgramDataService.id
      ),
      additionalBlocksName:
        this.informationBlockService.getAdditionalBlocksNameByProgramId(
          this.educationalProgramDataService.id
        ),
    });

    joinedWithObjectForm$.subscribe((result) => {
      this.additionalBlocksName = result.additionalBlocksName;

      this.educationalProgramDataService.isLaboratoryLessons =
        result.educationalProgram.isLaboratoryLessons;
      this.educationalProgramDataService.isPracticalLessons =
        result.educationalProgram.isPracticalLessons;
      this.educationalProgramDataService.disciplineId =
        result.educationalProgram.disciplineId;

      this.educationalProgram = result.educationalProgram;
      this.loader.hide();

      if (this.educationalProgram.status === Status.NotStarted) {
        this.changeStatus(Status.InProgress);
      }
    });
  }

  private changeStatus(status: Status) {
    const request: ChangeProgramStatusRequest = {
      id: this.educationalProgram.id,
      status: status,
    };

    this.educationalProgramService.changeStatus(request).subscribe((_) => {
      this.loader.hide();
    });
  }

  public updateEductionalProgram() {
    this.router.navigate(['/']);
  }

  public navigate(url: string, data?: any): void {
    if (data !== null && data !== undefined) {
      this.router.navigate([url, data], { relativeTo: this.route });
    } else {
      this.router.navigate([url], { relativeTo: this.route });
    }
  }
}
