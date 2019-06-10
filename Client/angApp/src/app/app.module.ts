import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router'
import { DiarydataService } from "./schema/diarydata.service"
import { TododataService } from "./schema/tododata.service"
import { UserAuthService } from "./schema/user-auth.service";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LeftbarComponent } from './leftbar/leftbar.component';
import { HomeComponent } from './home/home.component';
import { DiaryComponent } from './diary/diary.component';
import { TodoComponent } from './todo/todo.component';
import { CookieService } from "ngx-cookie-service";
import { UserIdentComponent } from './user-ident/user-ident.component';
import { UserprofileComponent } from './userprofile/userprofile.component';


//Application routes
const appRouts: Routes = [ { path: '', component: HomeComponent}, { path: 'profile', component: UserprofileComponent},
{ path: 'diary', component: DiaryComponent}, { path: 'todo', component: TodoComponent}];

@NgModule({
  declarations: [
    AppComponent,
    LeftbarComponent,
    HomeComponent,
    DiaryComponent,
    TodoComponent,
    UserIdentComponent,
    UserprofileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRouts),
    HttpClientModule,
    FormsModule
  ],
  providers: [DiarydataService, TododataService, UserAuthService, CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
