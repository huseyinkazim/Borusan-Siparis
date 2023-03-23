# Borusan.Api

1)Siparis Durum Sorgulama

http://localhost:5245/Orders/GetOrderState [GET]
Body->row->Json
{
  "customerOrderNo": "1"
}
2)Siparis Durum Güncelleme

http://localhost:5245/Orders/SetOrderState [POST]
Body->row->Json
{
  "customerOrderNo": "1",
  "status": 1,
  "changeDate": "2023-03-23T22:20:50.140Z"
}

3)Sipariş Listesi Ekleme
http://localhost:5245/Orders/PostOrder [POST]
Body->row->Json
[
  {
    "customerOrderNo": "2",
    "sourceAddress": "sourceAddress",
    "destinationAddress": "destinationAddress",
    "quantity": 1,
    "quantityUnit": 0,
    "weight": 10,
    "weightUnit": 0,
    "materialCode": "materialCode",
    "materialName": "materialName",
    "note": "note",
    "customerCode": "1",
    "changeDate": "2023-03-23T22:32:12.204Z"
  },
  {
    "customerOrderNo": "3",
    "sourceAddress": "sourceAddress",
    "destinationAddress": "destinationAddress",
    "quantity": 1,
    "quantityUnit": 0,
    "weight": 10,
    "weightUnit": 0,
    "materialCode": "materialCode2",
    "materialName": "materialName2",
    "note": "note",
    "customerCode": "1",
    "changeDate": "2023-03-23T22:32:12.204Z"
  }
]
