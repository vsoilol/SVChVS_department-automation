import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { faPencilAlt, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';
import { LiteratureService } from 'src/app/services/literature.service';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { CreateLiteratureRequest } from 'src/app/models/Request/CreateLiteratureRequest';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { UpdateLiteratureRequest } from 'src/app/models/Request/UpdateLiteratureRequest';
import { LiteratureType } from 'src/app/models/Enums/LiteratureType';
import { Literature } from 'src/app/models/Response/Literature';
import { EditLiteratureDialogComponent } from '../../dialogs/edit-literature-dialog/edit-literature-dialog.component';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ViewportScroller } from '@angular/common';
import { NgScrollbar, NgScrollbarModule } from 'ngx-scrollbar';
import {
  SmoothScrollToOptions,
  SMOOTH_SCROLL_OPTIONS,
} from 'ngx-scrollbar/smooth-scroll';
import { GetLiteraturesByTypeRequest } from 'src/app/models/Request/GetLiteraturesByTypeRequest';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';

@Component({
  selector: 'app-literatures',
  templateUrl: './literatures.component.html',
  styleUrls: ['./literatures.component.scss'],
})
export class LiteraturesComponent implements OnInit {
  public literatures: BehaviorSubject<Literature[]> = new BehaviorSubject<
    Literature[]
  >([]);

  public literatureType: LiteratureType;

  public faPencil = faPencilAlt;
  public faTrash = faTrash;

  public literatureSpinner = 'literatureSpinner';

  public displayedColumns: string[] = [
    'description',
    'recommended',
    'setNumber',
    'action',
  ];

  private literatureId: number;

  public get literatureTypeInfo(): string {
    switch (this.literatureType) {
      case LiteratureType.Main:
        return 'Основная литература';

      case LiteratureType.Additional:
        return 'Дополнительная литература';
    }
  }

  constructor(
    private modalService: NgbModal,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private literatureService: LiteratureService,
    private loader: LoaderService,
    public educationalProgramData: EducationalProgramDataService,
    public spinnerService: SpinnerService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((routeParams) => {
      this.literatureType = parseInt(routeParams.type);

      this.getAll();
    });
  }

  private getAll(): void {
    const request: GetLiteraturesByTypeRequest = {
      educationalProgramId: this.educationalProgramData.id,
      literatureType: this.literatureType,
    };

    this.spinnerService.showSpinner(this.literatureSpinner);

    this.literatureService.getLiteraturesByType(request).subscribe((_) => {
      this.literatures.next(_);

      this.spinnerService.hideSpinner(this.literatureSpinner);
    });
  }

  public enableEdit(literature: Literature = null) {
    if (!literature) {
      literature = {
        id: 0,
        description: '',
        recommended: '',
        setNumber: '',
        literatureType: this.literatureType,
      };
    }

    const dialogRef = this.dialog.open(EditLiteratureDialogComponent, {
      data: literature,
      minWidth: '550px',
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result: Literature) => {
      if (!result) {
        return;
      }

      if (result.id === 0) {
        this.createLiterature(literature);
      } else {
        this.updateLiterature(result);
      }
    });
  }

  private updateLiterature(literature: Literature): void {
    const request: UpdateLiteratureRequest = {
      description: literature.description,
      recommended: literature.recommended,
      setNumber: literature.setNumber,
      id: literature.id,
    };

    this.loader.show();
    this.literatureService.updateLiterature(request).subscribe((_) => {
      this.loader.hide();
      this.getAll();
    });
  }

  private createLiterature(literature: Literature): void {
    const request: CreateLiteratureRequest = {
      description: literature.description,
      recommended: literature.recommended,
      setNumber: literature.setNumber,
      literatureType: literature.literatureType ?? this.literatureType,
      educationalProgramId: this.educationalProgramData.id,
    };

    this.loader.show();
    this.literatureService.createLiterature(request).subscribe((_) => {
      this.loader.hide();
      this.getAll();
    });
  }

  openModal(content: TemplateRef<any>, literatureId: number): void {
    this.literatureId = literatureId;
    this.modalService.open(content, { backdrop: 'static', centered: true });
  }

  deleteLiterature() {
    this.loader.show();
    this.literatureService
      .deleteLiterature(this.literatureId)
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
