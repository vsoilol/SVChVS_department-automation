import { Component } from '@angular/core';

import { MENU_ITEMS } from './admin-menu';

@Component({
  selector: 'app-admin',
  styleUrls: ['admin.component.scss'],
  template: ` <router-outlet></router-outlet> `,
})
export class AdminComponent {
  menu = MENU_ITEMS;
  users: { name: string; title: string }[] = [
    { name: 'Carla Espinosa', title: 'Nurse' },
    { name: 'Bob Kelso', title: 'Doctor of Medicine' },
    { name: 'Janitor', title: 'Janitor' },
    { name: 'Perry Cox', title: 'Doctor of Medicine' },
    { name: 'Ben Sullivan', title: 'Carpenter and photographer' },
  ];
  item: {
    name: string;
    designation: string;
    buttons: { icon: string; content: string; link: string }[];
  } = {
    name: 'Сука',
    designation: 'Пиздец',
    buttons: [
      { icon: 'supervisor_account', content: 'Пользователи', link: '' },
    ],
  };
}
