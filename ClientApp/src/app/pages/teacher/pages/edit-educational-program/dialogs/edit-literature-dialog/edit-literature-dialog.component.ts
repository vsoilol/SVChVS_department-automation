import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Literature } from 'src/app/models/Response/Literature';

@Component({
  selector: 'app-edit-literature-dialog',
  templateUrl: './edit-literature-dialog.component.html',
  styleUrls: ['./edit-literature-dialog.component.scss'],
})
export class EditLiteratureDialogComponent implements OnInit {
  public form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<EditLiteratureDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public literature: Literature,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  private initForm() {
    this.form = this.fb.group({
      description: [
        this.literature.description,
        Validators.compose([Validators.required]),
      ],
      recommended: [
        this.literature.recommended,
        Validators.compose([Validators.required]),
      ],
      setNumber: [
        this.literature.setNumber,
        Validators.compose([Validators.required]),
      ],
    });
  }

  closeDialog() {
    if (!this.form.valid) {
      return;
    }

    this.literature.description = this.form.get('description').value;
    this.literature.recommended = this.form.get('recommended').value;
    this.literature.setNumber = this.form.get('setNumber').value;

    this.dialogRef.close(this.literature);
  }
}
