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
  cookieValue: string;
  constructor(private service: TododataService) {}

  ngOnInit() {
    this.cookieValue = this.service.getUserCookie();
    this.refreshNotes();
  }

  //Show all done Todo`s
  openTodoHistory(){
    this.isOpen = !this.isOpen;
    if(this.isOpen == true){
      this.service.getFinishedTodoNotes(this.cookieValue);
    }
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.form.reset();
  }

  //Send GET request and recieve all unfinished Todo`s
  refreshNotes(){
    this.service.getTodoNotes(this.cookieValue);
  }

  //Send POST request to server
  addNote(model: TodoModel){
    this.service.createTodoNote(model, this.cookieValue).subscribe(res => { this.refreshNotes(); });
  }

  //Send PUT request to server
  updateNote(model: TodoModel){
    this.service.updateTodo(model.id, model, this.cookieValue).subscribe(res => { this.refreshNotes(); });
  }

  //Send DELETE request to server
  deleteNote(model: TodoModel){
    this.service.deleteTodo(model.id).subscribe(res => { this.service.getFinishedTodoNotes(this.cookieValue); } );
  }

}
