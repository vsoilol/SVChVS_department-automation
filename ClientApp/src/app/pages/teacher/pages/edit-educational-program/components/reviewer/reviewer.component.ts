import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { UpdateReviewer } from 'src/app/models/Request/UpdateReviewer';
import { Reviewer } from 'src/app/models/Response/Reviewer';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { ReviewerService } from 'src/app/services/reviewer.service';

@Component({
  selector: 'app-reviewer',
  templateUrl: './reviewer.component.html',
  styleUrls: ['./reviewer.component.scss'],
})
export class ReviewerComponent implements OnInit {
  public form: FormGroup;

  public reviewerLayerSpinner: string = 'reviewerLayerSpinner';

  public isEdit: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public reviewer: Reviewer;

  constructor(
    public spinnerService: SpinnerService,
    private reviewerService: ReviewerService,
    private educationalProgram: EducationalProgramDataService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getReviewerInfo();
  }

  private getReviewerInfo(): void {
    this.spinnerService.showSpinner(this.reviewerLayerSpinner);

    this.reviewerService
      .getByProgramId(this.educationalProgram.id)
      .subscribe((_) => {
        this.reviewer = _;
        this.spinnerService.hideSpinner(this.reviewerLayerSpinner);
      });
  }

  public editReviewerInfo(): void {
    this.isEdit.next(true);
    this.initForm();
  }

  public updateReviewerInfo(): void {
    this.spinnerService.showSpinner(this.reviewerLayerSpinner);

    const request: UpdateReviewer = this.form.value as UpdateReviewer;
    this.reviewerService.update(request).subscribe((_) => {
      this.spinnerService.hideSpinner(this.reviewerLayerSpinner);
      this.isEdit.next(false);
      this.getReviewerInfo();
    });
  }

  private initForm() {
    this.form = this.fb.group({
      id: [this.reviewer.id, Validators.compose([Validators.required])],
      name: [this.reviewer.name, Validators.compose([Validators.required])],
      surname: [
        this.reviewer.surname,
        Validators.compose([Validators.required]),
      ],
      patronymic: [
        this.reviewer.patronymic,
        Validators.compose([Validators.required]),
      ],
      position: [
        this.reviewer.position,
        Validators.compose([Validators.required]),
      ],
    });
  }
}
