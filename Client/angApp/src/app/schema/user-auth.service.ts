import { Injectable } from '@angular/core';
import { CookieService } from "ngx-cookie-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { User } from "./usermodel";

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {
  user: User;
  readonly connectionString: string = "http://localhost:7000/identapi/auth";
  constructor(private cook: CookieService, private http: HttpClient) {}

  checkCookies(){
    return this.cook.check("authCook");
  }

  getCookieValue(){
    return this.cook.get("authCook");
  }

  deleteCookie(){
    this.cook.delete("authCook");
  }

  registerNewUser(model: User){
    return this.http.post(this.connectionString + "/reg", model)
    .subscribe(res => {
      if(res == "exist"){
        this.user = res as User;
      }
      else{
        this.user = res as User;
        this.cook.set("authCook", this.user._id);
      };
    });
  }

  authUser(model: User){
    let params: HttpParams = new HttpParams()
      .set("Login", model.login)
      .set("Password", model.password);

    this.http.get(this.connectionString + "/log", {params} )
    .subscribe(res => {this.user = res as User; if(res != null) this.cook.set("authCook", this.user._id)});
  }

  findUserById(_id: string){
    return this.http.get(this.connectionString + "/find/" + _id);
  }

}
