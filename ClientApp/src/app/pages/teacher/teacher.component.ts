import { Component } from '@angular/core';

@Component({
  selector: 'app-teacher',
  styleUrls: ['teacher.component.scss'],
  template: `
    <div class="teacher__container">
      <router-outlet></router-outlet>
    </div>
  `,
})
export class TeacherComponent {}
