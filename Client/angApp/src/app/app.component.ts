import { Component, OnInit, HostListener } from '@angular/core';
import { UserAuthService } from "./schema/user-auth.service";
import { NgForm } from "@angular/forms";
import { User } from "./schema/usermodel";
import { CookieService } from 'ngx-cookie-service';
import { checkAndUpdateBinding } from '@angular/core/src/view/util';


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
