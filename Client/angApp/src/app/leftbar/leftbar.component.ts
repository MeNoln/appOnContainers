import { Component, OnInit } from '@angular/core';
import { UserAuthService } from "../schema/user-auth.service";
import { User } from "../schema/usermodel";

@Component({
  selector: 'app-leftbar',
  templateUrl: './leftbar.component.html',
  styleUrls: ['./leftbar.component.css']
})
export class LeftbarComponent implements OnInit{
  user: User = new User();
  cookieValue: string;

  constructor(private service: UserAuthService){}

  ngOnInit(){
    this.getCurrentUser();
  }

  getCurrentUser(){
    this.cookieValue = this.service.getCookieValue();
    this.service.findUserById(this.cookieValue).subscribe(res => this.user = res as User);
  }

  quitFromApp(){
    this.service.deleteCookie();
  }
}
