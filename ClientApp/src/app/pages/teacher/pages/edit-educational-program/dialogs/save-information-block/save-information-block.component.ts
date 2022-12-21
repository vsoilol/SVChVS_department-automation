import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-save-information-block',
  templateUrl: './save-information-block.component.html',
  styleUrls: ['./save-information-block.component.scss']
})
export class SaveInformationBlockComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<SaveInformationBlockComponent>) { }

  ngOnInit(): void {
  }
}
