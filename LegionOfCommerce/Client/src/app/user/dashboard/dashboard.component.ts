import { Component, OnInit } from '@angular/core';
import { DashboardPage } from '@app/core/models/dashboard-page';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  currentPage: DashboardPage;
  PageType = DashboardPage;

  constructor() {}

  ngOnInit() {}

  onPageChange($event) {
    this.currentPage = $event.page;
  }
}
