import { Component, OnInit } from '@angular/core';
import { UserAuthService } from "../schema/user-auth.service";
import { User } from "../schema/usermodel";

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {
  user: User;
  constructor(private service: UserAuthService) { }

  ngOnInit() {
    this.user = this.service.user;
  }

}
