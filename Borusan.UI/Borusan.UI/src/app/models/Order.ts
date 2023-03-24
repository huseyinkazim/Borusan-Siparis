import { State } from "./State";

export class Order {
  customerOrderNo: string;
  sourceAddress: string;
  destinationAddress: string;
  quantity: number;
  quantityUnit: string;
  weight: number;
  weightUnit: string;
  materialCode: string;
  note: string;
  orderStatus: State;
  
}