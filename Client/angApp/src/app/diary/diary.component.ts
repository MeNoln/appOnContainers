import { Component, OnInit } from '@angular/core';
import { DiaryModel } from "../schema/diarymodel";
import { DiarydataService } from "../schema/diarydata.service";

@Component({
  selector: 'app-diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {


  constructor(private service: DiarydataService) { }

  ngOnInit() {
    this.service.getDiaryNotes();
  }

  

}
