import { Component, OnInit, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { faPencilAlt, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { CreateMethodicalRecommendationRequest } from 'src/app/models/Request/CreateMethodicalRecommendationRequest';
import { MethodicalRecommendation } from 'src/app/models/Response/MethodicalRecommendation';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { MethodicalRecommendationService } from 'src/app/services/methodical-recommendation.service';
import { EditMethodicalRecommendationComponent } from '../../dialogs/edit-methodical-recommendation/edit-methodical-recommendation.component';

@Component({
  selector: 'app-methodical-recommendations',
  templateUrl: './methodical-recommendations.component.html',
  styleUrls: ['./methodical-recommendations.component.scss'],
})
export class MethodicalRecommendationsComponent implements OnInit {
  public methodicalRecommendations: BehaviorSubject<
    MethodicalRecommendation[]
  > = new BehaviorSubject<MethodicalRecommendation[]>([]);

  public faPencil = faPencilAlt;
  public faTrash = faTrash;

  public displayedColumns: string[] = ['content', 'link', 'action'];

  public methodicalRecommendationId: number;

  public methodicalRecommendationSpinner = 'methodicalRecommendationSpinner';

  constructor(
    private modalService: NgbModal,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private methodicalRecommendationService: MethodicalRecommendationService,
    public educationalProgramData: EducationalProgramDataService,
    private loader: LoaderService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  public getAll(): void {
    this.spinnerService.showSpinner(this.methodicalRecommendationSpinner);

    this.methodicalRecommendationService
      .getMethodicalRecommendationByProgramId(this.educationalProgramData.id)
      .subscribe((_) => {
        this.methodicalRecommendations.next(_);

        this.spinnerService.hideSpinner(this.methodicalRecommendationSpinner);
      });
  }

  public enableEdit(methodicalRecommendation: MethodicalRecommendation = null) {
    if (!methodicalRecommendation) {
      methodicalRecommendation = {
        id: 0,
        content: '',
        link: '',
      };
    }

    const dialogRef = this.dialog.open(EditMethodicalRecommendationComponent, {
      data: methodicalRecommendation,
      minWidth: '550px',
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result: MethodicalRecommendation) => {
      if (!result) {
        return;
      }

      if (result.id === 0) {
        this.createMethodicalRecommendation(result);
      } else {
        this.updateMethodicalRecommendation(result);
      }
    });
  }

  private updateMethodicalRecommendation(
    methodicalRecommendation: MethodicalRecommendation
  ): void {
    this.loader.show();
    this.methodicalRecommendationService
      .updateMethodicalRecommendation(methodicalRecommendation)
      .subscribe((_) => {
        this.loader.hide();
        this.getAll();
      });
  }

  private createMethodicalRecommendation(
    methodicalRecommendation: MethodicalRecommendation
  ): void {
    const request: CreateMethodicalRecommendationRequest = {
      link: methodicalRecommendation.link,
      content: methodicalRecommendation.content,
      educationalProgramId: this.educationalProgramData.id,
    };

    this.loader.show();
    this.methodicalRecommendationService
      .createMethodicalRecommendation(request)
      .subscribe((_) => {
        this.loader.hide();
        this.getAll();
      });
  }

  openModal(
    content: TemplateRef<any>,
    methodicalRecommendationId: number
  ): void {
    this.methodicalRecommendationId = methodicalRecommendationId;
    this.modalService.open(content, { backdrop: 'static', centered: true });
  }

  deleteMethodicalRecommendation() {
    this.loader.show();
    this.methodicalRecommendationService
      .deleteMethodicalRecommendation(this.methodicalRecommendationId)
      .subscribe((_) => {
        this.loader.hide();
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
