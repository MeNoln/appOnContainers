import { Component, OnInit } from '@angular/core';
import { TododataService } from "../schema/tododata.service";
import { TodoModel } from "../schema/todomodel";
import { NgForm } from "@angular/forms";

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {
  todo: TodoModel = new TodoModel();
  isOpen: boolean = false;
  constructor(private service: TododataService) {}

  ngOnInit() {
    this.refreshNotes();
  }

  openTodoHistory(){
    this.isOpen = !this.isOpen;
    if(this.isOpen == true){
      this.service.getFinishedTodoNotes();
    }
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.form.reset();
  }

  refreshNotes(){
    this.service.getTodoNotes();
  }

  addNote(model: TodoModel, form: NgForm){
    this.service.createTodoNote(model).subscribe(res => { this.refreshNotes(); });
  }

  updateNote(model: TodoModel){
    this.service.updateTodo(model.id, model).subscribe(res => { this.refreshNotes(); });
  }

}
