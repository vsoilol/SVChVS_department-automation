export enum DocumentType {
  Word = 0,
  Excel = 1,
}

export interface DocumentInfo {
  documentType: DocumentType;
  contentType: string;
  fileExtension: string;
}

export const DOCUMENTS_INFO: DocumentInfo[] = [
  {
    documentType: DocumentType.Word,
    contentType:
      'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
    fileExtension: '.docx',
  },
];
