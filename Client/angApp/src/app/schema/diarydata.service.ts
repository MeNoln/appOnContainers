import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { DiaryModel } from "./diarymodel";

@Injectable({
  providedIn: 'root'
})
export class DiarydataService {
  diaryNotes: DiaryModel[];
  readonly connectionSting: string = "http://localhost:7000/diaryapi/diary";

  constructor(private http: HttpClient) { }

  getDiaryNotes(){
    this.http.get(this.connectionSting)
    .toPromise()
    .then(res => this.diaryNotes = res as DiaryModel[]);
  }
}
