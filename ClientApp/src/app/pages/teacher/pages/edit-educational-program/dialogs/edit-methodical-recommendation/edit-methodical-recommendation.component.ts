import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MethodicalRecommendation } from 'src/app/models/Response/MethodicalRecommendation';

@Component({
  selector: 'app-edit-methodical-recommendation',
  templateUrl: './edit-methodical-recommendation.component.html',
  styleUrls: ['./edit-methodical-recommendation.component.scss'],
})
export class EditMethodicalRecommendationComponent implements OnInit {
  public form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<EditMethodicalRecommendationComponent>,
    @Inject(MAT_DIALOG_DATA)
    public methodicalRecommendation: MethodicalRecommendation,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  private initForm() {
    this.form = this.fb.group({
      content: [
        this.methodicalRecommendation.content,
        Validators.compose([Validators.required]),
      ],
      link: [
        this.methodicalRecommendation.link,
        Validators.compose([Validators.required]),
      ],
    });
  }

  closeDialog() {
    if (!this.form.valid) {
      return;
    }

    this.methodicalRecommendation.content = this.form.get('content').value;
    this.methodicalRecommendation.link = this.form.get('link').value;

    this.dialogRef.close(this.methodicalRecommendation);
  }
}
