import { Component, OnInit } from '@angular/core';
import { UserAuthService } from "../schema/user-auth.service";
import { User } from "../schema/usermodel";

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {
  user: User = new User();
  userAuthCookie: string;
  isImageLoading: boolean;
  userProfileImage: any;
  inputImage: File;
  constructor(private service: UserAuthService) { }

  ngOnInit() {
    this.userAuthCookie = this.service.getCookieValue();
    this.getUserImage();
    this.getCurrentUser();
  }

  getCurrentUser(){
    this.service.findUserById(this.userAuthCookie).subscribe(res => this.user = res as User);
  }

  createImageFromBlobType(image: Blob){
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      this.userProfileImage = reader.result;
    }, false);

    if(image){
      reader.readAsDataURL(image);
    }
  }

  getUserImage(){
    this.isImageLoading = true;
    this.service.getUserImageFromServer(this.userAuthCookie)
    .subscribe(data => {
      this.createImageFromBlobType(data);
      this.isImageLoading = false;
    }, error => {
      this.isImageLoading = false;
      console.log(error);
    });
  }

  convertToFile(event){
    this.inputImage = <File>event.target.files[0];
  }

  updateUserImage(){
    const data = new FormData();
    data.append("id", this.userAuthCookie);
    data.append("uploadedImage", this.inputImage);
    this.service.sendImage(data).subscribe(res => {
      this.createImageFromBlobType(res);
      this.isImageLoading = false;
    }, error => {
      this.isImageLoading = false;
      console.log(error);
    });
  }
}
