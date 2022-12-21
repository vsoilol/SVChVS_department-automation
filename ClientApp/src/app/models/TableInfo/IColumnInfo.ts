export interface IColumnInfo {
  field: string;

  label: string;

  visible: boolean;

  propertyName?: string;

  width?: string;

  getRowStyle?: (data: any) => string;

  getRowInfo?: (data: any) => string;
}
