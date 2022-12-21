export enum Role {
  Admin = 1,
  Teacher = 2,
  DepartmentHead = 3,
  EducationDepartmentOfficial = 4,
  SimpleUser = 5,
}

export interface User {
  id: string;

  email: string;

  role: Role | null;

  jwtToken?: string;

  firstName: string;

  surname: string;

  patronymic: string;

  departmentId: number | null;
}
