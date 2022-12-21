export enum Status {
  NotStarted = 0,
  InProgress = 1,
  Done = 2,
  OnCorrection = 3,
  NotExist = 4,
}

export namespace Status {
  export function toString(mode: Status): string {
    return Status[mode];
  }

  export function parse(mode: string): Status {
    return Status[mode];
  }

  export const STATUS_STYLES = new Map([
    [Status.NotStarted, 'not-started'],
    [Status.InProgress, 'in-progress'],
    [Status.Done, 'done'],
    [Status.OnCorrection, 'on-correction'],
    [Status.NotExist, 'not-exist'],
  ]);

  export const STATUS_DESCRIPTION = new Map([
    [Status.NotStarted, 'Не начата работа'],
    [Status.InProgress, 'В процессе'],
    [Status.Done, 'Сделано'],
    [Status.OnCorrection, 'На исправлении'],
    [Status.NotExist, 'Не существует'],
  ]);

  export function toKeyValuePair(): { number: Status; description: string }[] {
    return [
      { number: Status.NotStarted, description: 'Не начата работа' },
      { number: Status.InProgress, description: 'В процессе' },
      { number: Status.Done, description: 'Сделано' },
      { number: Status.OnCorrection, description: 'На исправлении' },
      { number: Status.NotExist, description: 'Не существует' },
    ];
  }
}
