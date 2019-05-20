import { Component, OnInit } from '@angular/core';
import { DiaryModel } from "../schema/diarymodel";
import { DiarydataService } from "../schema/diarydata.service";
import { NgForm } from "@angular/forms"

@Component({
  selector: 'app-diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.css']
})
export class DiaryComponent implements OnInit {
  diary: DiaryModel = new DiaryModel();
  constructor(private service: DiarydataService) { }

  ngOnInit() {
    this.refreshNotes();
    this.resetForm();
  }

  //Send GET request to recieve data
  refreshNotes(){
    this.service.getDiaryNotes();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.form.reset();
    this.diary = { id: 0, date: "", dayMark: "", dayDescription: ""};
  }

  //Fill form with picked model
  populateForm(model: DiaryModel){
    this.diary = Object.assign({}, model);
  }

  //Decide what to do with recieved from form data
  checkId(model: DiaryModel){
    if(model.id == 0)
      this.addNote(model);
    else
      this.updateNote(model);
  }

  //Send POST request to server
  addNote(model: DiaryModel){
    this.service.addDiaryNote(model).subscribe(res => { this.refreshNotes(); });
  }

  //Send PUT request to server
  updateNote(model: DiaryModel){
    this.service.updateDiaryNote(model.id, model).subscribe(res => { this.refreshNotes(); });
  }

  //Send DELETE request to server
  deleteNote(model: DiaryModel){
    this.service.deleteDiaryNote(model.id).subscribe(res => { this.refreshNotes(); });
  }
}