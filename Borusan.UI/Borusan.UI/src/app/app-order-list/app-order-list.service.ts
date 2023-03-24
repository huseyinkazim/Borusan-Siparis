import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../models/Order';
import { OrderUpdate } from '../models/OrderUpdate';
import { State } from '../models/State';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  ordersMock: Order[] = [
    { customerOrderNo: '1', sourceAddress: 'Istanbul', destinationAddress: 'Ankara', quantity: 5, quantityUnit: 'Koli', weight: 10, weightUnit: 'Kg', materialCode: '123', note: 'Urgent', orderStatus: State.SiparisAlindi },
    { customerOrderNo: '2', sourceAddress: 'Izmir', destinationAddress: 'Bursa', quantity: 3, quantityUnit: 'Koli', weight: 7, weightUnit: 'Kg', materialCode: '456', note: '', orderStatus: State.SiparisAlindi },
    { customerOrderNo: '3', sourceAddress: 'Antalya', destinationAddress: 'Adana', quantity: 2, quantityUnit: 'Adet', weight: 2, weightUnit: 'Kg', materialCode: '789', note: '', orderStatus: State.YolaCikti },
  ];
  constructor(private httpClient: HttpClient) {
  }

  getOrdersMock(): Order[] {
    return this.ordersMock;

  }

  updateOrderStatusMock(order: Order, newStatus: string): void {
    debugger;
    // Burada müşteri statü entegrasyon servisini çağırmak gerekir.
  }



  url: string = "http://localhost:5245/Orders/";


  getOrders(): Observable<Order[]> {

    return this.httpClient
      .get<Order[]>(this.url + "GetAllOrders");
  }

  updateOrderStatus(order: Order): Observable<any> {
    let option = {
      headers: this.getCustomHeaders()
    };

    let orderUpdate: OrderUpdate = new OrderUpdate();

    orderUpdate.CustomerOrderNo = order.customerOrderNo;
    orderUpdate.Status = order.orderStatus;
    orderUpdate.ChangeDate = new Date();

    const serializedBody = JSON.stringify(orderUpdate, (key, value) => {
      if (key === 'Status') {
        return Number(value);
      }
      return value;
    });
    
    return this.httpClient.post(this.url + "SetOrderState", serializedBody, option);
  }
  getCustomHeaders(): HttpHeaders {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Api-Key', 'xxx');
    return headers;
  }
}