import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { DiaryModel } from "./diarymodel";

@Injectable({
  providedIn: 'root'
})
export class DiarydataService {
  diaryNotes: DiaryModel[]; //List of all diary notes
  readonly connectionSting: string = "http://localhost:7000/diaryapi/diary"; //Server API URL

  constructor(private http: HttpClient) { }

  getDiaryNotes(){
    this.http.get(this.connectionSting)
    .toPromise()
    .then(res => this.diaryNotes = res as DiaryModel[]);
  }

  addDiaryNote(model: DiaryModel){
    const diaryModel = { date: new Date().toLocaleString(), dayMark: model.dayMark, dayDescription: model.dayDescription };
    return this.http.post(this.connectionSting + "/add", diaryModel)
  }

  updateDiaryNote(id: number, model: DiaryModel){
    const diaryModel = { id: model.id, date: new Date().toLocaleString() + "(changed)",
                          dayMark: model.dayMark, dayDescription: model.dayDescription };
    return this.http.put(this.connectionSting + "/" + id, diaryModel);
  }

  deleteDiaryNote(id: number){
    return this.http.delete(this.connectionSting + "/delete/" + id);
  }
}
