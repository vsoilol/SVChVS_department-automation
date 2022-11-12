import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.scss'],
})
export class ForbiddenComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}

  goToHome(): void {
    this.router.navigate(['/authentication']);
  }
}
