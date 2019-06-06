import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CookieService } from "ngx-cookie-service";
import { TodoModel } from "../schema/todomodel";

@Injectable({
  providedIn: 'root'
})
export class TododataService {
  readonly connectionSting: string = "http://localhost:7000/todoapi/todo"; //Server API URL
  todoList: TodoModel[]; //List of unfinished Todo`s (isDone: false)
  finishedList: TodoModel[]; //List of finished Todo`s (isDone: true)
  constructor(private http: HttpClient, private cook: CookieService) { }

  getUserCookie(){
    return this.cook.get("authCook");
  }

  getTodoNotes(cookieValue: string){
    this.http.get(this.connectionSting + "/" + cookieValue)
    .toPromise()
    .then(res => this.todoList = res as TodoModel[]);
  }

  getFinishedTodoNotes(cookieValue: string){
    this.http.get(this.connectionSting + "/done/" + cookieValue)
    .toPromise()
    .then(res => this.finishedList = res as TodoModel[]);
  }

  createTodoNote(model: TodoModel, cookieValue: string){
    const todoModel = { taskName: model.taskName, isDone: false, userId: cookieValue };
    return this.http.post(this.connectionSting + "/add", todoModel)
  }

  updateTodo(id:number, model: TodoModel, cookieValue: string){
    const todoModel = { id: model.id, taskName: model.taskName, isDone: true, userId: cookieValue };
    return this.http.put(this.connectionSting + "/" + id, todoModel)
  }

  deleteTodo(id: number){
    return this.http.delete(this.connectionSting + "/delete/" + id);
  }
}
