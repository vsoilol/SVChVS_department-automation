import {
  FormGroup,
  FormControl,
  Validators,
  FormGroupDirective,
  NgForm,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

export const EmailValidation = [Validators.required];
export const PasswordValidation = [
  Validators.required,
  Validators.minLength(6),
  Validators.maxLength(24),
];

export class RepeatPasswordEStateMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    return (
      control &&
      control.parent.get('password').value !==
        control.parent.get('passwordAgain').value &&
      control.dirty
    );
  }
}

export function RepeatPasswordValidator(group: FormGroup) {
  const password = group.controls.password.value;
  const passwordConfirmation = group.controls.passwordAgain.value;

  return password === passwordConfirmation ? null : { passwordsNotEqual: true };
}

export const identityRevealedValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const password = control.get('password');
  const passwordConfirmation = control.get('passwordAgain');

  return password &&
    passwordConfirmation &&
    password.value !== passwordConfirmation.value
    ? { passwordsNotEqual: true }
    : null;
};
