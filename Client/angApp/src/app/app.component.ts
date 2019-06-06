import { Component, OnInit, HostListener } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'appOnContainer';
  verifyCookie: boolean = false;
  
  constructor(private cook: CookieService){}


  ngOnInit(){
    this.verifyCookie = this.cook.check("authCook");
  }

  @HostListener('click')
  check(){
    this.verifyCookie = this.cook.check("authCook");
  }
}
