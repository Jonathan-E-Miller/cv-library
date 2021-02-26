import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  cookieValue: string;
  text: string;
  constructor(private cookieService: CookieService, private router: Router) { }

  ngOnInit(): void {
    this.text = "Hello World";
    const cookieExists: boolean = this.cookieService.check('test');

    if (!cookieExists) {
      this.router.navigate(['/login']);
    }
  }
}
