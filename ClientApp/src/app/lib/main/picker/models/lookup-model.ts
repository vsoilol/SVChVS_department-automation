export interface LookupModel {
  id?: string;
  name?: string;
}

export function getLookupModelsFromObjectsArray(
  array: object[],
  idPropertyName: string,
  textPropertyName: string
): LookupModel[] {
  return array.map((obj) => ({
    id: obj[idPropertyName],
    name: obj[textPropertyName],
  }));
}

export function getLookupModelsFromArray(array: object[]): LookupModel[] {
  return array.map((obj) => ({ id: obj.toString(), name: obj.toString() }));
}

export function getLookupModelsFromModelArray(
  array: object[],
  propertyName: string
): LookupModel[] {
  return array.map((obj) => ({
    id: obj[propertyName].toString(),
    name: obj[propertyName].toString(),
  }));
}
