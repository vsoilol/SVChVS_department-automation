import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { SpinnerService } from 'src/app/lib/main/spinner/spinner.service';
import { CONFIG } from 'src/app/lib/tinymce/config';
import { MAIN_HTML } from 'src/app/lib/tinymce/mainHtml';
import { GetBlockByNumberRequest } from 'src/app/models/Request/GetBlockByNumberRequest';
import { UpdateInformationBlockContent } from 'src/app/models/Request/UpdateInformationBlockContent';
import { InformationBlock } from 'src/app/models/Response/InformationBlock';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { InformationBlockService } from 'src/app/services/information-block.service';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { MatSnackBar } from '@angular/material/snack-bar';

import * as ckeditor from '../../../../../../../ckeditor5-build-inline/build/ckeditor';
import { IDeactivateComponent } from 'src/app/helpers/guards/dirty-check-guard';
import { MatDialog } from '@angular/material/dialog';
import { SaveInformationBlockComponent } from '../../dialogs/save-information-block/save-information-block.component';
import { map, mergeMap, switchMap } from 'rxjs/operators';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';

@Component({
  selector: 'app-information-block',
  templateUrl: './information-block.component.html',
  styleUrls: ['./information-block.component.scss'],
})
export class InformationBlockComponent implements OnInit, IDeactivateComponent {
  @ViewChild('myEditor') myEditor: any;

  public model = {
    editorData: 'Данные отсутствуют',
  };

  public Editor = ckeditor;

  public isEdit: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  private mainHtml = MAIN_HTML;

  private blockNumber: string;

  public informationBlock: InformationBlock;

  public config: any = CONFIG;

  public mainSpinner: string = 'mainSpinner';

  get htmlCodeWithCustomStyles(): string {
    const startIndex = this.mainHtml.indexOf('<body><br>') + 10;
    let html = this.getArticleContent();
    return this.insert(this.mainHtml, startIndex, html);
  }

  private getArticleContent() {
    if (this.myEditor && this.myEditor.editorInstance) {
      return this.myEditor.editorInstance.getData();
    }

    return '';
  }

  public onChange({ editor }: ChangeEvent) {
    if (!this.isEdit.value) {
      this.isEdit.next(true);
    }
  }

  constructor(
    private informationBlockService: InformationBlockService,
    private educationalProgram: EducationalProgramDataService,
    public spinnerService: SpinnerService,
    private route: ActivatedRoute,
    private _snackBar: MatSnackBar,
    public dialog: MatDialog,
    private loader: LoaderService
  ) {}

  canExit(): Observable<boolean> {
    if (!this.isEdit.value) {
      return of(true);
    }

    const dialogRef = this.dialog.open(SaveInformationBlockComponent, {
      width: '250px',
    });

    return dialogRef.afterClosed().pipe(
      mergeMap((isSave) => {
        if (!isSave) {
          return of(true);
        } else {
          this.loader.show();

          const updateContentRequest: UpdateInformationBlockContent = {
            educationalProgramId: this.informationBlock.educationalProgramId,
            informationBlockId: this.informationBlock.id,
            content: this.htmlCodeWithCustomStyles,
          };
          return this.informationBlockService
            .updateContent(updateContentRequest)
            .pipe(
              map((_) => {
                console.log(_);
                this.loader.hide();
                this.openSnackBar('Данные обновлены', 'Ок');
                return true;
              })
            );
        }
      })
    );
  }

  ngOnInit(): void {
    this.route.params.subscribe((routeParams) => {
      this.blockNumber = routeParams.number;

      this.isEdit.next(false);

      this.getInformationBlock();
    });
  }

  public getInformationBlock() {
    this.spinnerService.showSpinner(this.mainSpinner);

    const request: GetBlockByNumberRequest = {
      educationalProgramId: this.educationalProgram.id,
      number: this.blockNumber,
    };

    this.informationBlockService.getBlockByNumber(request).subscribe((_) => {
      this.informationBlock = _;
      this.spinnerService.hideSpinner(this.mainSpinner);

      this.setHtmlToTinyMce();
    });
  }

  public isContentEmpty(html: string): boolean {
    const content = this.getBodyFromHtmlCode(html);
    return this.isBlank(content);
  }

  private isBlank(str): boolean {
    return !str || /^\s*$/.test(str);
  }

  public getBodyFromHtmlCode(html: string): string {
    const startIndex = html.indexOf('<body><br>') + 10;
    const endIndex = html.indexOf('</body>');

    return html.substring(startIndex, endIndex);
  }

  private setHtmlToTinyMce(): void {
    const startIndex = this.informationBlock.content.indexOf('<body><br>') + 10;
    const endIndex = this.informationBlock.content.indexOf('</body>');

    let html = this.informationBlock.content.substring(startIndex, endIndex);

    const content = this.getBodyFromHtmlCode(html);

    if (!this.isBlank(content)) {
      this.model.editorData = html;
    } else {
      this.model.editorData = 'Данные отсутствуют';
    }
  }

  updateInformationBlock() {
    this.isEdit.next(false);
    this.spinnerService.showSpinner(this.mainSpinner);

    const updateContentRequest: UpdateInformationBlockContent = {
      educationalProgramId: this.informationBlock.educationalProgramId,
      informationBlockId: this.informationBlock.id,
      content: this.htmlCodeWithCustomStyles,
    };
    this.informationBlockService
      .updateContent(updateContentRequest)
      .subscribe((_) => {
        this.spinnerService.hideSpinner(this.mainSpinner);
        this.getInformationBlock();
        this.openSnackBar('Данные обновлены', 'Ок');
      });
  }

  private insert(str, index, value) {
    return str.substr(0, index) + value + str.substr(index);
  }

  public openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 3000,
    });
  }
}
