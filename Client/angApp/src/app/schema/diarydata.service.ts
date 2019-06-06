import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { CookieService } from "ngx-cookie-service";
import { DiaryModel } from "./diarymodel";

@Injectable({
  providedIn: 'root'
})
export class DiarydataService {
  diaryNotes: DiaryModel[]; //List of all diary notes
  readonly connectionSting: string = "http://localhost:7000/diaryapi/diary"; //Server API URL

  constructor(private http: HttpClient, private cook: CookieService) { }

  getUserCookie(){
    return this.cook.get("authCook");
  }

  getDiaryNotes(cookieValue: string){
    this.http.get(this.connectionSting + "/" + cookieValue)
    .toPromise()
    .then(res => this.diaryNotes = res as DiaryModel[]);
  }

  addDiaryNote(model: DiaryModel, cookieValue: string){
    const diaryModel = { date: new Date().toLocaleString(),
                         dayMark: model.dayMark, dayDescription: model.dayDescription, userId: cookieValue };
    return this.http.post(this.connectionSting + "/add", diaryModel);
  }

  updateDiaryNote(id: number, model: DiaryModel, cookieValue: string){
    const diaryModel = { id: model.id, date: new Date().toLocaleString() + "(changed)",
                          dayMark: model.dayMark, dayDescription: model.dayDescription, userId: cookieValue };
    return this.http.put(this.connectionSting + "/" + id, diaryModel);
  }

  deleteDiaryNote(id: number){
    return this.http.delete(this.connectionSting + "/delete/" + id);
  }
}
