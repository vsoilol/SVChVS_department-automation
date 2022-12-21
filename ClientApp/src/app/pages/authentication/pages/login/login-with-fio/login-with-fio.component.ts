import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { LoginService } from 'src/app/services/login.service';
import { PasswordValidation } from 'src/app/validators/validators';

@Component({
  selector: 'app-login-with-fio',
  templateUrl: './login-with-fio.component.html',
  styleUrls: ['./login-with-fio.component.scss', '../login.component.scss'],
})
export class LoginWithFioComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;

  public isPasswordShow: boolean = false;

  faEye = faEye;
  faEyeSlash = faEyeSlash;

  toggleShow() {
    this.isPasswordShow = !this.isPasswordShow;
  }

  public error: '';

  constructor(
    public router: Router,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private loginService: LoginService,
    private authenticationService: AuthenticationService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      patronymic: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }

  get loginFormControl() {
    return this.loginForm.controls;
  }

  onSubmit() {
    if (!this.loginForm.valid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loaderService.show();
    this.authenticationService
      .loginByFullName(
        this.loginFormControl.name.value.replace(/\s/g, ''),
        this.loginFormControl.surname.value.replace(/\s/g, ''),
        this.loginFormControl.patronymic.value.replace(/\s/g, ''),
        this.loginFormControl.password.value
      )
      .subscribe({
        next: () => {
          this.loaderService.hide();
          this.loginService.redirectToPage();
        },
        error: (error) => {
          this.loaderService.hide();
          this.error = error.error.errors[0];
        },
      });
  }
}
