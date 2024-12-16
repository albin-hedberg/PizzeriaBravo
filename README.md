# PizzeriaBravo - Clean Code Labb 2, Grupp Bravo

### OrderService API Endpoints

#### Get All Orders
- **Endpoint:** `GET /api/orders`
- **Description:** Retrieves all orders.
- **Response:**
  - `200 OK` with a list of orders.
  - `404 Not Found` if no orders are found.

#### Get Order By ID
- **Endpoint:** `GET /api/orders/{id}`
- **Description:** Retrieves an order by its ID.
- **Parameters:**
  - `id` (Guid): The ID of the order.
- **Response:**
  - `200 OK` with the order details.
  - `404 Not Found` if the order is not found.

#### Get Orders By Customer ID
- **Endpoint:** `GET /api/orders/customer/{id}`
- **Description:** Retrieves all orders for a specific customer.
- **Parameters:**
  - `id` (Guid): The ID of the customer.
- **Response:**
  - `200 OK` with a list of orders.
  - `404 Not Found` if no orders are found for the customer.

#### Get Orders By Status
- **Endpoint:** `GET /api/orders/status/{status}`
- **Description:** Retrieves all orders with a specific status.
- **Parameters:**
  - `status` (OrderStatus): The status of the orders.
- **Response:**
  - `200 OK` with a list of orders.
  - `404 Not Found` if no orders are found with the specified status.

#### Create Order
- **Endpoint:** `POST /api/orders`
- **Description:** Creates a new order.
- **Request Body:** 
  - `Order` object containing the order details.
- **Response:**
  - `201 Created` with the created order details.
  - `400 Bad Request` if the order creation fails.

#### Update Order Status
- **Endpoint:** `PUT /api/orders/{id}/status/{status}`
- **Description:** Updates the status of an order.
- **Parameters:**
  - `id` (Guid): The ID of the order.
  - `status` (OrderStatus): The new status of the order.
- **Response:**
  - `200 OK` with the updated order details.
  - `404 Not Found` if the order is not found.

#### Cancel Order
- **Endpoint:** `PUT /api/orders/{id}/`
- **Description:** Updates the status of an order to cacelled.
- **Parameters:**
  - `id` (Guid): The ID of the order.
- **Response:**
  - `200 OK` with the updated order details.
  - `404 Not Found` if the order is not found.

#### Delete Order
- **Endpoint:** `DELETE /api/orders/{id}`
- **Description:** Deletes an order by its ID.
- **Parameters:**
  - `id` (Guid): The ID of the order.
- **Response:**
  - `200 OK` if the order is successfully deleted.
  - `404 Not Found` if the order is not found.
