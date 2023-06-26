Chức năng của các khối:
Khối nguồn: Cung cấp nguồn hoạt động cho toàn bộ hệ thống
Khối cảm biến: Thu thập dữ liệu từ môi trường xung quang và gửi dữ liệu đó đến khối xử lý trung tâm.
Khối xử lý trung tâm: Điều khiển các quá trình giao tiếp, nhận và xử lý dữ liệu từ khối cảm biến và gửi dữ liệu đến khối server.
Khối server: Nhận dữ liệu từ khối xử lý trung tâm và hiển thị thông tin, đồng thời đưa ra cảnh báo cho người dùng quan sát.
Khối broker: Máy chủ trung gian giữa các thiết bị truyền nhận.
Khối OpenAI: Nhận dữ liệu từ webserver và trả lại lời khuyên.
Nguyên lý hoạt động của hệ thống
Nguyên lý hoạt động của hệ thống dự án được mô tả như sau: Hệ thống sử dụng Esp32 để thu thập dữ liệu từ các cảm biến thông qua các chân dữ liệu. Sau đó, Esp32 nén dữ liệu thành gói tin và sử dụng giao thức MQTT để gửi gói tin đến webserver.
Tại webserver, gói tin được nhận và giải nén để khôi phục lại dữ liệu ban đầu. Sau đó, dữ liệu được chuyển đến quy trình xử lý và lưu trữ thông tin tương ứng. Quá trình xử lý này có thể bao gồm các bước như phân tích, tính toán, lưu trữ vào cơ sở dữ liệu, và xử lý logic khác tùy theo yêu cầu của dự án.
Cuối cùng, thông tin được trả về cho người dùng thông qua giao diện người dùng của ứng dụng web. Các thông tin hiển thị có thể được trình bày dưới dạng các biểu đồ, bảng dữ liệu hoặc các hiển thị tương tác khác để cung cấp thông tin một cách trực quan và dễ hiểu cho người dùng.
Như vậy, dự án kết hợp giữa việc thu thập dữ liệu từ các cảm biến, sử dụng giao thức MQTT để truyền dữ liệu, xử lý và lưu trữ thông tin tại webserver, và cuối cùng cung cấp các thông tin hiển thị cho người dùng. Qua đó, dự án mang lại giải pháp toàn diện và tiện ích trong việc giám sát và quản lý dữ liệu từ các cảm biến.
