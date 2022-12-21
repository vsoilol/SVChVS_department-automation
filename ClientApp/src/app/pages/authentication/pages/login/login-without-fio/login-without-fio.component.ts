import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { of } from 'rxjs';
import { catchError, first } from 'rxjs/operators';
import { LoaderService } from 'src/app/lib/main/loader/loader.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login-without-fio',
  templateUrl: './login-without-fio.component.html',
  styleUrls: ['./login-without-fio.component.scss', '../login.component.scss'],
})
export class LoginWithoutFioComponent implements OnInit {
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

  emailRegex =
    /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(this.emailRegex),
      ]),
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
      .login(
        this.loginFormControl.email.value,
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
