import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { LoaderService } from '../lib/main/loader/loader.service';
import { Role } from '../models/Response/User';
import { AuthenticationService } from './authentication.service';
import { WindowsCanvasService } from './windows-canvas.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public error: '';

  constructor(
    public router: Router,
    private authenticationService: AuthenticationService,
    private loaderService: LoaderService,
    private route: ActivatedRoute,
    private windowCanvas: WindowsCanvasService
  ) {}

  redirectToPage(): void {
    const user = this.authenticationService.userValue;

    if (user) {
      switch (user.role) {
        case Role.Admin: {
          this.windowCanvas.isShow.next(true);
          this.router.navigate(['/admin']).then(() => {
            this.windowCanvas.isShow.next(false);
          });
          break;
        }
        case Role.Teacher: {
          this.windowCanvas.isShow.next(true);
          this.router.navigate(['/teacher']).then(() => {
            this.windowCanvas.isShow.next(false);
          });
          break;
        }
        case Role.DepartmentHead: {
          this.windowCanvas.isShow.next(true);
          this.router.navigate(['/departmentHead']).then(() => {
            this.windowCanvas.isShow.next(false);
          });
          break;
        }
      }
    }
  }

  onSubmit(email: string, password: string) {
    this.authenticationService
      .login(email, password)
      .pipe(first())
      .subscribe({
        next: () => {
          this.redirectToPage();
        },
        error: (error) => {
          this.error = error;
        },
      });
  }
}
