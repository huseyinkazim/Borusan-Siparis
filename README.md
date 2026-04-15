# Borusan Lojistik – Order Management System (Case Study)

A .NET Core & Angular-based logistics order management system developed as a case study for handling order intake, validation, and status tracking workflows in enterprise logistics scenarios.

---

## 📌 Project Overview

This project simulates a real-world logistics integration scenario where external customers send order data to a central system. The system validates, stores, and tracks orders while providing status updates through API endpoints.

Key capabilities:

* Order intake via REST API
* Validation of incoming requests
* Duplicate order prevention per customer
* Automatic material creation if not exists
* Order status tracking workflow
* Status update integration endpoint
* Operational UI for order management

---

## 🏗️ Architecture Overview

```
[ Angular UI ]
      |
      v
[ ASP.NET Core Web API ]
      |
      v
[ Application Layer ]
      |
      v
[ Domain Layer (Business Rules) ]
      |
      v
[ Infrastructure Layer ]
      |
      v
[ SQL Server Database ]
```

---

## 🔄 API Flow

### 1. Order Creation Flow

```
Client (POST /api/orders)
        |
        v
Validate Request
        |
        +--> Duplicate Order Check (Customer + Order No)
        |
        +--> Material Exists?
                | Yes -> Continue
                | No  -> Create Material
        |
        v
Create Order (Status = "Sipariş Alındı")
        |
        v
Return Response
        |
        v
{ 
  CustomerOrderNo,
  SystemOrderNo,
  Status (0/1),
  ErrorMessage
}
```

---

### 2. Status Update Flow

```
Angular UI / API Call
        |
        v
Update Order Status
        |
        v
Persist Status Change
        |
        v
Call External Integration Endpoint
(http://api.xx.com/statu)
        |
        v
Send Payload:
{
  CustomerOrderNo,
  Status,
  ChangeDate
}
```

---

## 📦 Domain Model

### Order

* Id (SystemOrderNo)
* CustomerCode
* CustomerOrderNo
* FromAddress
* ToAddress
* Quantity
* QuantityUnit (Adet, Koli, Paket, Palet)
* Weight
* WeightUnit (Kg, Ton)
* MaterialCode
* MaterialName
* Note
* Status
* CreatedAt

---

### Material

* Id
* MaterialCode
* MaterialName

---

### OrderStatus (Enum)

* Sipariş Alındı
* Yola Çıktı
* Dağıtım Merkezinde
* Dağıtıma Çıktı
* Teslim Edildi
* Teslim Edilemedi

---

## 🗄️ Database Schema

### Orders Table

| Column          | Type     | Description           |
| --------------- | -------- | --------------------- |
| Id              | int (PK) | System Order Id       |
| CustomerCode    | varchar  | Customer identifier   |
| CustomerOrderNo | varchar  | External order number |
| FromAddress     | varchar  | Origin                |
| ToAddress       | varchar  | Destination           |
| Quantity        | decimal  | Quantity              |
| QuantityUnit    | varchar  | Unit                  |
| Weight          | decimal  | Weight                |
| WeightUnit      | varchar  | Unit                  |
| MaterialCode    | varchar  | Material code         |
| MaterialName    | varchar  | Material name         |
| Note            | text     | Optional note         |
| Status          | int      | Order status          |
| CreatedAt       | datetime | Creation date         |

---

### Materials Table

| Column       | Type     | Description |
| ------------ | -------- | ----------- |
| Id           | int (PK) | Material Id |
| MaterialCode | varchar  | Unique code |
| MaterialName | varchar  | Name        |

---

## 🌐 API Endpoints

### 📥 Create Orders (Batch Supported)

```
POST /api/orders
```

Supports multiple orders in one request.

**Request Example:**

```json
[
  {
    "customerCode": "C001",
    "customerOrderNo": "ORD123",
    "fromAddress": "Istanbul",
    "toAddress": "Ankara",
    "quantity": 10,
    "quantityUnit": "Adet",
    "weight": 50,
    "weightUnit": "Kg",
    "materialCode": "M001",
    "materialName": "Laptop",
    "note": "Fragile"
  }
]
```

---

### 🔄 Update Order Status

```
PUT /api/orders/{id}/status
```

---

### 📡 External Status Integration

```
POST http://api.xx.com/statu
```

Payload:

```json
{
  "customerOrderNo": "ORD123",
  "status": "Yola Çıktı",
  "changeDate": "2026-04-15T10:00:00"
}
```

---

## 🖥️ Angular UI Features

* Order listing screen
* Status display per order
* Editable status field (only status editable)
* Real-time update trigger for status changes

---

## ⚙️ Business Rules

* CustomerOrderNo must be unique per CustomerCode
* New orders default status: **Sipariş Alındı**
* Material is auto-created if not exists
* Batch order creation supported
* Status change triggers external API call

---

## 🧪 Testing

* Postman collection used for API validation
* Batch order scenarios tested
* Duplicate order validation tested
* Status integration call simulated

---

## 📌 Tech Stack

### Backend

* .NET Core Web API
* Entity Framework Core
* SQL Server

### Frontend

* Angular

### Optional Enhancements (Not fully implemented)

* Serilog + MongoDB logging
* RabbitMQ async processing

---

## 🚀 Summary

This project demonstrates a real-world logistics order processing workflow including validation, integration-ready APIs, and operational UI management. It is designed as a case study for enterprise backend and full-stack development scenarios.
