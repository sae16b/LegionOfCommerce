import { Component, OnInit } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    console.log('navbar init12');
    this.authService.getUserInfo().subscribe(user => {
      console.log(user);
    });
  }

  get isAuth() {
    return this.authService.isAuth;
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }
}
