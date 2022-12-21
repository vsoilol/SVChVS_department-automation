import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateInformationBlockContent } from '../models/Request/CreateInformationBlockContent';
import { DeleteInformationBlockContent } from '../models/Request/DeleteInformationBlockContent';
import { GetBlockByNumberRequest } from '../models/Request/GetBlockByNumberRequest';
import { UpdateInformationBlockContent } from '../models/Request/UpdateInformationBlockContent';
import { AdditionalBlocksName } from '../models/Response/AdditionalBlocksName';
import { InformationBlock } from '../models/Response/InformationBlock';
import { InformationBlockWithoutContent } from '../models/Response/InformationBlockWithoutContent';

@Injectable({
  providedIn: 'root',
})
export class InformationBlockService {
  private url = environment.apiUrl + 'informationBlock';

  constructor(private http: HttpClient) {}

  public getMainBlocksByProgramId(
    programId: number
  ): Observable<InformationBlock[]> {
    return this.http.get<InformationBlock[]>(
      this.url + `/mainBlocks/${programId}`
    );
  }

  public createContent(
    createInformationBlock: CreateInformationBlockContent
  ): Observable<void> {
    return this.http.post<void>(this.url, createInformationBlock);
  }

  public updateContent(
    updateInformationBlock: UpdateInformationBlockContent
  ): Observable<void> {
    return this.http.put<void>(this.url, updateInformationBlock);
  }

  public deleteContent(
    deleteInformationBlock: DeleteInformationBlockContent
  ): Observable<void> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append(
      'educationalProgramId',
      deleteInformationBlock.educationalProgramId
    );
    httpParams = httpParams.append(
      'informationBlockId',
      deleteInformationBlock.informationBlockId
    );

    return this.http.delete<void>(this.url, { params: httpParams });
  }

  public getLastBlocksByProgramId(
    programId: number
  ): Observable<InformationBlock[]> {
    return this.http.get<InformationBlock[]>(
      this.url + `/lastBlocks/${programId}`
    );
  }

  public getEditableBlocksByProgramId(
    programId: number
  ): Observable<InformationBlock[]> {
    return this.http.get<InformationBlock[]>(
      this.url + `/editableBlocks/${programId}`
    );
  }

  public getNotChoosenBlocksByProgramId(
    programId: number
  ): Observable<InformationBlockWithoutContent[]> {
    return this.http.get<InformationBlockWithoutContent[]>(
      this.url + `/notChoosenBlocks/${programId}`
    );
  }

  public getTemplatesByInformationBlockId(
    informationBlockId: number
  ): Observable<string[]> {
    return this.http.get<string[]>(
      this.url + `/templates/${informationBlockId}`
    );
  }

  public getBlockByNumber(
    request: GetBlockByNumberRequest
  ): Observable<InformationBlock> {
    let httpParams = new HttpParams();

    httpParams = httpParams.append(
      'educationalProgramId',
      request.educationalProgramId
    );
    httpParams = httpParams.append('number', request.number);

    return this.http.get<InformationBlock>(this.url + `/byNumber`, {
      params: httpParams,
      withCredentials: true,
    });
  }

  public getAdditionalBlocksNameByProgramId(
    educationalProgramId: number
  ): Observable<AdditionalBlocksName[]> {
    return this.http.get<AdditionalBlocksName[]>(
      this.url + `/blocksName/${educationalProgramId}`
    );
  }
}
