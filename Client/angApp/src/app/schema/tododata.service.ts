import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http' 
import { TodoModel } from "../schema/todomodel";

@Injectable({
  providedIn: 'root'
})
export class TododataService {
  readonly connectionSting: string = "http://localhost:7000/todoapi/todo"; //Server API URL
  todoList: TodoModel[]; //List of unfinished Todo`s (isDone: false)
  finishedList: TodoModel[]; //List of finished Todo`s (isDone: true)
  constructor(private http: HttpClient) { }

  getTodoNotes(){
    this.http.get(this.connectionSting)
    .toPromise()
    .then(res => this.todoList = res as TodoModel[]);
  }

  getFinishedTodoNotes(){
    this.http.get(this.connectionSting + "/done")
    .toPromise()
    .then(res => this.finishedList = res as TodoModel[]);
  }

  createTodoNote(model: TodoModel){
    const todoModel = { taskName: model.taskName, isDone: false};
    return this.http.post(this.connectionSting + "/add", todoModel)
  }

  updateTodo(id:number, model: TodoModel){
    const todoModel = { id: model.id, taskName: model.taskName, isDone: true };
    return this.http.put(this.connectionSting + "/" + id, todoModel)
  }

  deleteTodo(id: number){
    return this.http.delete(this.connectionSting + "/delete/" + id);
  }
}
