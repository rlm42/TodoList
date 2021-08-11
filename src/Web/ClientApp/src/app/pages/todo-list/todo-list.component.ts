import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { finalize } from 'rxjs/operators';

// Services
import { TodoListService } from 'src/app/services/todo-list.service';

// Constants
import { text } from 'src/app/constants/text';
import { icons } from 'src/app/constants/icons';
import { TodoModel } from 'src/app/models/todo.model';
import { TodoStatusModel } from 'src/app/models/todo-status.model';


@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {

  // Fields
  icons = icons;
  t = text;
  todoList: TodoModel[] = [];
  loadingText = '';



  // Initial Design using FormGroup and FormControl
  // formAddTodoItem = new FormGroup({
  //   name : new FormControl('')
  // });

  // Using FormBuilder

  formAddTodoItem = this.fb.group({
    name: ['']
  });

  // Constructor
  constructor(private todoListSvc: TodoListService, private titleSvc: Title, private fb: FormBuilder) {
    this.titleSvc.setTitle(text.TodoList);
  }

  // Event Methods

  ngOnInit() {
    this.loadTodoList();
  }

  onKeydown_addItem_name() {
    this.addItem();
  }

  onClick_addItem() {
    this.addItem();
  }

  onClick_toggleItem(item: TodoModel) {
    this.updateItemStatus(new TodoStatusModel({ id: item.id, isDone: !item.isDone }));
  }

  onClick_deleteItem(id: number) {
    this.deleteItem(id);
  }


  // onSubmit() {
  //   const model = new TodoModel(this.formAddTodoItem.value);
  //   console.log(model);
  // }


  // CRUD Methods

  addItem() {

    const model = new TodoModel(this.formAddTodoItem.value);

  
    this.todoListSvc.addItem(model)
    .subscribe(
      (addedItem) => {
        this.formAddTodoItem.reset();

        // newly added item is added to list instead of calling serverside again to reload the list
        this.todoList.push(addedItem);
      },
      (error) => {
        console.log(error);
      });
  }

  loadTodoList() {

    this.loadingText = text.LoadingItems;
    this.todoListSvc.getList()
    .pipe(finalize(() => { this.loadingText = ''; }))
    .subscribe(
      // On Success
      (r) => {
        this.todoList = r;
      },
      // On error
      (error) => {
        console.log(error);
      }
    );
  }

  updateItemStatus(model: TodoStatusModel) {

    this.todoListSvc.updateStatus(model).subscribe(
      // on success
      () => {

      // update item status on clientside list, after successfull update on serverside
      const item = this.todoList.find(itm => itm.id === model.id);
      item.isDone = model.isDone;
      },
      // on error
      (error) => {
        console.log(error);
      });
  }

  deleteItem(id: number) {
    this.todoListSvc.delete(id).subscribe(
      // on success
      () => {

        // remove item from the clientside list if successfully removed on serverside
        const idx = this.todoList.findIndex(item => item.id === id);
        this.todoList.splice(idx, 1);
      },
      // on error
      (error) => {
        console.log(error);
      });
  }


}
