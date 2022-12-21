import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  faPencilAlt,
  faTrash,
  faCheck,
  faTimes,
} from '@fortawesome/free-solid-svg-icons';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject, forkJoin } from 'rxjs';
import { flatMap, map, toArray } from 'rxjs/operators';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { CreateEvaluationTool } from 'src/app/models/Request/CreateEvaluationTool';
import { DeleteEvaluationTool } from 'src/app/models/Request/DeleteEvaluationTool';
import { EvaluationTool } from 'src/app/models/Response/EvaluationTool';
import { EvaluationToolType } from 'src/app/models/Response/EvaluationToolType';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { EvaluationToolService } from 'src/app/services/evaluation-tool.service';

@Component({
  selector: 'app-evaluation-tools',
  templateUrl: './evaluation-tools.component.html',
  styleUrls: ['./evaluation-tools.component.scss'],
})
export class EvaluationToolsComponent implements OnInit {
  form: FormGroup;
  public isDisable: boolean = false;

  displayedColumns: string[] = ['evaluationToolTypes', 'setNumber', 'action'];

  faPencil = faPencilAlt;
  faTrash = faTrash;
  faCheck = faCheck;
  faTimes = faTimes;

  public dataSource = new BehaviorSubject<EvaluationTool[]>([]);
  private evaluationTools: EvaluationTool[];
  private evaluationToolTypeId: number;

  public evaluationToolTypes: BehaviorSubject<EvaluationToolType[]> =
    new BehaviorSubject<EvaluationToolType[]>([]);

  public evaluationToolsLayerSpinner: string = 'evaluationToolsLayerSpinner';

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    private snackBar: MatSnackBar,
    private evaluationToolService: EvaluationToolService,
    public educationalProgramData: EducationalProgramDataService,
    public spinnerService: SpinnerService
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.spinnerService.showSpinner(this.evaluationToolsLayerSpinner);

    this.getAll();
  }

  initForm() {
    this.form = this.fb.group({
      evaluationToolTypeId: ['', Validators.compose([Validators.required])],
      setNumber: ['', Validators.compose([Validators.required])],
      isEdit: [false, Validators.compose([Validators.required])],
    });
  }

  getAll() {
    const joinedWithObjectForm$ = forkJoin({
      allEvaluationTool: this.evaluationToolService
        .getAllByProgramId(this.educationalProgramData.id)
        .pipe(
          flatMap((evaluationTools) => evaluationTools),
          map((data) => {
            return {
              ...data,
              isEdit: false,
            };
          }),
          toArray()
        ),
      notChoosenEvaluationTool:
        this.evaluationToolService.getNotChoosenEvaluationTool(
          this.educationalProgramData.id
        ),
    });

    joinedWithObjectForm$.subscribe((result) => {
      this.spinnerService.hideSpinner(this.evaluationToolsLayerSpinner);

      this.evaluationTools = result.allEvaluationTool;
      this.dataSource.next(this.evaluationTools);

      this.evaluationToolTypes.next(result.notChoosenEvaluationTool);
    });
  }

  openModal(content: TemplateRef<any>, evaluationToolTypeId: number): void {
    this.evaluationToolTypeId = evaluationToolTypeId;
    this.modalService.open(content, { backdrop: 'static', centered: true });
  }

  public openSnackBar(message: string) {
    this.snackBar.open(message, 'Ок', {
      duration: 3000,
    });
  }

  cancelEditing(): void {
    this.isDisable = false;

    this.spinnerService.showSpinner(this.evaluationToolsLayerSpinner);

    this.getAll();
  }

  addEmptyEvaluationTool(): void {
    this.isDisable = true;
    const newControl: EvaluationTool = {
      setNumber: 0,
      isEdit: true,
      evaluationToolTypeId: this.evaluationToolTypes.value[0].id,
      name: '',
    };

    this.evaluationTools.push(newControl);
    this.form.patchValue(newControl);
    this.dataSource.next(this.evaluationTools);
  }

  createEvaluationTool() {
    if (this.form.valid) {
      this.isDisable = false;

      const request: CreateEvaluationTool = {
        educationalProgramId: this.educationalProgramData.id,
        evaluationToolTypeId: this.form.get('evaluationToolTypeId').value,
        setNumber: this.form.get('setNumber').value,
      };

      this.spinnerService.showSpinner(this.evaluationToolsLayerSpinner);

      this.evaluationToolService
        .createEvaluationTool(request)
        .subscribe((_) => {
          this.openSnackBar('Successfully updated !!!');
          this.getAll();
        });
    }
  }

  deleteEvaluationTool() {
    this.openSnackBar('Successfully deleted !!!');

    const request: DeleteEvaluationTool = {
      educationalProgramId: this.educationalProgramData.id,
      evaluationToolTypeId: this.evaluationToolTypeId,
    };

    this.spinnerService.showSpinner(this.evaluationToolsLayerSpinner);

    this.evaluationToolService.deleteEvaluationTool(request).subscribe((_) => {
      this.getAll();
    });
  }
}
