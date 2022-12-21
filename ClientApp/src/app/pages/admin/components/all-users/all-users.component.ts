import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.scss'],
})
export class AllUsersComponent implements OnInit {
  heroes = [1, 2, 3, 4, 5, 6];

  constructor() {}

  ngOnInit(): void {}
}
