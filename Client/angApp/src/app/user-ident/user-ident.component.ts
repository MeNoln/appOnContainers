import { Component, OnInit } from '@angular/core';
import { UserAuthService } from "../schema/user-auth.service";
import { NgForm } from "@angular/forms";
import { User } from "../schema/usermodel";

@Component({
  selector: 'app-user-ident',
  templateUrl: './user-ident.component.html',
  styleUrls: ['./user-ident.component.css']
})
export class UserIdentComponent implements OnInit {
  verifyCookies: boolean = false;
  switchBtn: boolean = true;
  existUserMessage: string;
  user: User = new User();

  constructor(private service: UserAuthService) { }

  ngOnInit() {
  }

  switchForms(){
    this.switchBtn = !this.switchBtn;
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.form.reset();
    this.user = { _id: "",login: "", password: "", userName: "", userAge: null };
    this.existUserMessage = "";
  }

  registerUser(model: User){
    this.service.registerNewUser(model);
  }

  authUser(model: User){
    this.service.authUser(model);
  }
}
