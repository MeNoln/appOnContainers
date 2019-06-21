import { Component, OnInit } from '@angular/core';
import { UserAuthService } from "../schema/user-auth.service";
import { User } from "../schema/usermodel";

@Component({
  selector: 'app-leftbar',
  templateUrl: './leftbar.component.html',
  styleUrls: ['./leftbar.component.css']
})
export class LeftbarComponent implements OnInit{
  cookieValue: string;

  constructor(private service: UserAuthService){}

  ngOnInit(){
  }

  quitFromApp(){
    this.service.deleteCookie();
  }
}
