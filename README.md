# WeatherApp
Website này hiển thị real-time các thông số của cảm biến được gửi từ module ESP32 thông qua giao thức MQTT.  Kết hợp với API của OpenAI để đưa ra cảnh báo cho người dùng ứng với thông số nhận được từ cảm biến. Thống kế dữ liệu theo ngày, theo tuần, theo giờ

[I. Mở đầu]

[II. Tổng quan]
- [1. Đưa ra bài toán]
- [2. Giải pháp thiết kế]

[III. Công nghệ sử dụng]
- [1. Công nghệ phần mềm]
  - [1.1. ASP.NET core 6]
  - [1.2. SQL Server]
  - [1.3. Javascrip]
  - [1.4. HTML CSS]
- [2. Công nghệ phần cứng]
  - [2.1. Kit ESP32]
  - [2.2. DHT22]
  - [2.3. MQ135]
  - [2.4. Rain]
  - [2.5. Lora]

[IV. Xây dựng và thiết kế]
- [1. Thiết kế phần cứng]
- [2. Thiết kế phần mềm]
- [3. Thực nghiệm]

[V. Đánh giá]

===========================
## I. Mở đầu
Mục tiêu của đề tài là nghiên thiết kế và xây dựng một trạm khí tượng sử dụng vi điều khiển ESP32. Trạm khí tượng này sẽ được thiết kế để đo các thông số như nhiệt độ, độ ẩm, chất lượng không khí và mưa. Dữ liệu thu thập được sẽ được hiển thị trên một web server đồng thời đưa ra cảnh báo cho người dùng quan sát.

## II. Tổng quan
### 1. Đưa ra bài toán 
Trong thời đại công nghệ số hiện nay, Internet of Things (IoT) đang trở thành xu hướng phát triển chính trong lĩnh vực kỹ thuật điện tử. Với sự phát triển của các thiết bị IoT, việc thu thập và xử lý dữ liệu từ các thiết bị trở nên dễ dàng hơn bao giờ hết. Trong đó, trạm khí tượng là một trong những ứng dụng phổ biến của IoT, giúp giám sát và thu thập dữ liệu về thời tiết.

Mục tiêu của đề tài là nghiên thiết kế và xây dựng một trạm khí tượng sử dụng vi điều khiển ESP32. Trạm khí tượng này sẽ được thiết kế để đo các thông số như nhiệt độ, độ ẩm, chất lượng không khí và mưa. Dữ liệu thu thập được sẽ được hiển thị trên một web server đồng thời đưa ra cảnh báo cho người dùng quan sát.

### 2. Giải pháp thiết kế
Ta sẽ tiến hành thiết kế mô hình Trạm Khí Tượng có tác dụng thu thập dữ liệu cảm biến hiển thị lên webserver và cảnh báo thời tiết cho người sử dụng, ta sử dụng các module cảm biến DHT22, MQ-135, mưa để thu thập dữ liệu xong đó gửi dữ liệu tổng hợp đến ESP32. Dữ liệu thu thập được từ môi trường sẽ được hiển thị và kèm theo cảnh báo lên web server đồng thời lưu trữ dữ liệu đó. 
<img src="https://drive.google.com/file/d/1iyYZQ-990KJo9zVlUwLc5xtSB6Yc-HTi/view?usp=sharing">

## III. Công nghệ sử dụng
### 1. ASP.NET Core 6
Đối với ASP.NET Core 6, tôi thực hiện tạo ra các API phục vụ cho Front-End. Trong project này, Tôi sử dụng: 

#### Entity Framework Core
Framework này giúp tôi có thể làm việc với cơ sở dữ liệu cụ thể là SQL server. Cung cấp một cách dễ dàng và linh hoạt để ánh xạ đối tượng vào cơ sở dữ liệu và thực hiện các thao tác truy vấn dữ liệu.

#### OpenAI
Thư viện này hiện đang sử dụng OpenAI's GPT3-API. Trong dự án này tôi dựa vào các thông số cảm biến sau đó đặt câu hỏi dựa trên những thông số đó và gửi cho OpenAI thông qua API OpenAI. Sau khi nhận được câu hỏi thì nó sẽ trả về cho tôi 1 chuỗi các thông tin cần thiết.

### 2. SQL Server
Nó được thiết kế để quản lý, lưu trữ và truy xuất dữ liệu trong dự án. Ở đây tôi, xây dựng 2 bảng dữ liệu: Dữ liệu theo ngày, Dữ liệu theo giờ. Những thông tin sẽ được xử lý và phân chia theo từng bảng dữ liệu. 

### 3. Javascrip
Thực hiện call API từ Back-End sau đó render dữ liệu bằng thư viện axios. 

### 4. HTML, CSS
Thực hiện xây dựng layout và đồ họa cho trang web.

## II. Xây dựng và thiết kế
<img src="https://drive.google.com/file/d/1BaY2wxhQ_r7cxOPl8wbak5KOzs28hyI1/view?usp=sharing">
<img src="https://drive.google.com/file/d/1NW-0Cx1mK3gZECJQ4uI68MjvY0i7nmTJ/view?usp=sharing">
