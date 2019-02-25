import { Component } from '@angular/core';


import * as $ from 'jquery';

class Item {
  purchase: string;
  done: boolean;
  price: number;

  constructor(purchase: string, price: number) {

    this.purchase = purchase;
    this.price = price;
    this.done = false;
  }
}

@Component({
  selector: 'startenum-app',
  template: `<div class="page-header">
    <h1> Список покупок 3</h1>
</div>
<div class="panel">
    <div class="form-inline">
        <div class="form-group">
            <div class="col-md-8">
                <input class="form-control" [(ngModel)]="text" placeholder = "Название" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-6">
                <input type="number" class="form-control" [(ngModel)]="price" placeholder="Цена" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-8">
                <button class="btn btn-default" (click)="addItem(text, price)">Добавить</button>
            </div>
        </div>
    </div>
    <table class="table table-striped">
        <thead>
        <tr>
            <th>Предмет</th>
            <th>Цена</th>
            <th>Куплено</th>
        </tr>
        </thead>
        <tbody>
        <tr *ngFor="let item of items">
            <td>{{item.purchase}}</td>
            <td>{{item.price}}</td>
            <td><input type="checkbox" [(ngModel)]="item.done" /></td>
        </tr>
        </tbody>
    </table>
    
    <button id="testBtn" class="test-btn" (click)="show()">Удалить</button>
</div>

<div class="modal" id="myModal">
    <div class="modal-header">
        <button class="close" data-dismiss="modal">×</button>
        <h3>Заголовок</h3>
    </div>
    <div class="modal-body">
        <p>Текст…</p>
    </div>
    <div class="modal-footer">
        <a href="#" class="btn">Закрыть</a>
        <a href="#" class="btn btn-primary">Сохранить</a>
    </div>
</div>`
})
export class AppComponentTest {

  public price:number;
  public text: string;
  items: Item[] =
  [
    { purchase: "Хлеб", done: false, price: 15.9 },
    { purchase: "Масло", done: false, price: 60 },
    { purchase: "Картофель", done: true, price: 22.6 },
    { purchase: "Сыр", done: false, price: 310 }
  ];
  addItem(text: string, price: number): void {

    if (text == null || text == undefined || text.trim() == "")
      return;
    if (price == null || price == undefined)
      return;
    this.items.push(new Item(text, price));

    $("#testBtn").hide();

  }

  show() {
    $('#myModal').modal();
  }
}

