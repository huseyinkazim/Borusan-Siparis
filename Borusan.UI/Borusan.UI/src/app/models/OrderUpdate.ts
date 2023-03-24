import { State } from "./State";

export class OrderUpdate {
  CustomerOrderNo: string;
  Status: State;
  ChangeDate: Date;
}