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
  formUserValue: User = new User();
  userAuthCookie: string;
  isImageLoading: boolean;
  switchInfoToForm: boolean = true;
  userProfileImage: any;
  inputImage: File;
  constructor(private service: UserAuthService) { }

  ngOnInit() {
    this.userAuthCookie = this.service.getCookieValue();
    this.getUserImage();
    this.getCurrentUser();
  }

  //Switch from user info to update form
  switchToForm(){
    this.switchInfoToForm = !this.switchInfoToForm;
    if(!this.switchInfoToForm)
      this.formUserValue = this.user;
  }

  //Gets user info
  getCurrentUser(){
    this.service.findUserById(this.userAuthCookie).subscribe(res => this.user = res as User);
  }

  //Update user info
  updateUserInfo(model: User){
    this.service.updateUser(model).subscribe(res => {
      this.user = res as User;
      this.formUserValue = new User();
      this.switchToForm();
    });
  }

  //Converts from blob to image
  createImageFromBlobType(image: Blob){
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      this.userProfileImage = reader.result;
    }, false);

    if(image){
      reader.readAsDataURL(image);
    }
  }

  //Sen request to server to get an inage in Blob type
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

  //Converts uploaded picture to file type
  convertToFile(event){
    this.inputImage = <File>event.target.files[0];
  }

  //Update User image
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
