import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Constants
import { apis } from '../constants/apis';
import { environment } from 'src/environments/environment';

// Models
import { TodoModel } from '../models/todo.model';
import { TodoStatusModel } from '../models/todo-status.model';

@Injectable({
  providedIn: 'root'
})
export class TodoListService {

  // ------------
  // Properties
  // ------------
  private apiTodos = `${environment.apiUrl}/${apis.todos}`;
  private baseApiUrl = environment.apiUrl;

  // ------------
  // Constructor
  // ------------
  constructor(private http: HttpClient) { }

  // ------------
  // Methods
  // ------------
  getList() {
    // const url = 'https://localhost:44308/api/todos';   // hardcoded api url
    // const url = environment.apiUrl + '/todos';         // after putting apiUrl inside environment settings file
    // const url = `${environment.apiUrl}/todos`;         // using template string instead of concatenating
    const apiTodos = `${environment.apiUrl}/${apis.todos}`;    // after taking out endpointstring and placing it in constants apis.ts file
    return this.http.get<any>(apiTodos);
  }

  addItem(model: TodoModel) {
    return this.http.post<TodoModel>(this.apiTodos, model);
  }

  updateStatus(model: TodoStatusModel) {
    const url = `${this.baseApiUrl}/${apis.todosStatusUpdate(model.id)}`;
    return this.http.put(url, model);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiTodos}/${id}`);
  }


}
