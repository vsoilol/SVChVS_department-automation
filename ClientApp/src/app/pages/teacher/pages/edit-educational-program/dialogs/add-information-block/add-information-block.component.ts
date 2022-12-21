import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InformationBlockWithoutContent } from 'src/app/models/Response/InformationBlockWithoutContent';

@Component({
  selector: 'app-add-information-block',
  templateUrl: './add-information-block.component.html',
  styleUrls: ['./add-information-block.component.scss'],
})
export class AddInformationBlockComponent implements OnInit {
  public chooseBlock: InformationBlockWithoutContent;

  constructor(
    public dialogRef: MatDialogRef<AddInformationBlockComponent>,
    @Inject(MAT_DIALOG_DATA) public data: InformationBlockWithoutContent[]
  ) {}

  ngOnInit(): void {}

  onNoClick(): void {
    this.dialogRef.close(this.chooseBlock);
  }

  functionToChangeValue(event: any) {
    this.chooseBlock = event;
  }
}
