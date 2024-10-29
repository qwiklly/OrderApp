# Фильтрация заказов (OrderApp)
Asp net web api приложение, предназначенное для фильтрации заказов службой доставки. 

# Основной функционал:
* Фильтрация заказов, по городу или времени заказов 
* Добавление, удаление, просмотр всех заказов
 
# Шаги для запуска проекта:
Выполните команду в консоли диспетчера пакетов для обновления базы данных:
```
update-database
```
# Дополнительные инструкции
В миграции уже создано 4 записи заказов. Для проверки работоспособности в свагере можете в поле location прописать "Moscow" или точное время доставки ( **посмотреть его можно путем выполнения Get - запроса GetAllOrders** ), затем вы получите заказы, по фильтру Москва. Также можно добавить заказ вручную ( **AddOrder**) и отфильтровать его.

![image](https://github.com/user-attachments/assets/ec3ae91d-d8a4-427b-87b9-54964180a50d)
