import { Component, OnInit } from '@angular/core';
import { Order } from '../models/Order';
import { State } from '../models/State';
import { OrderService } from './app-order-list.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './app-order-list.component.html',
  styleUrls: ['./app-order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];

  states = Object.keys(State).filter(f => !isNaN(Number(f)))
    .map(key => ({ value: key, label: State[Number(key)] }));

  constructor(private orderService: OrderService) {

  }

  ngOnInit() {
    this.orders = this.orderService.getOrdersMock();
    let _orders = this.orderService.getOrders().subscribe((data: Order[]) => {
      console.log(data);

      this.orders = data;
    }, err => {
      console.log(err.message);
    },);
  }
  changeOrderState(event: Order) {
    this.orderService.updateOrderStatus(event).subscribe(data => {
      console.log(data);

    }, err => {
      console.log(err.message);
    },);
  }
  
}
