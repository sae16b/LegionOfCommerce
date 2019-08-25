import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DashboardPage } from '@app/core/models/dashboard-page';

@Component({
  selector: 'app-dashboard-sidebar',
  templateUrl: './dashboard-sidebar.component.html',
  styleUrls: ['./dashboard-sidebar.component.scss']
})
export class DashboardSidebarComponent implements OnInit {
  @Output() pageChange = new EventEmitter();

  currentPage: DashboardPage;
  PageType = DashboardPage;

  constructor() {}

  ngOnInit() {
    this.changePage(DashboardPage.ADDING_ITEM);
  }

  changePage(page: DashboardPage) {
    this.currentPage = page;
    const obj = {
      event: 'dashboard page changed',
      page
    };
    this.pageChange.emit(obj);
  }

  isPage(page: DashboardPage) {
    return page === this.currentPage;
  }
}
