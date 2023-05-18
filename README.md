# WeatherApp
Website này hiển thị real-time các thông số của cảm biến được gửi từ module ESP32 thông qua giao thức MQTT.  Kết hợp với API của OpenAI để đưa ra cảnh báo cho người dùng ứng với thông số nhận được từ cảm biến. Thống kế dữ liệu theo ngày, theo tuần, theo giờ

I. Mở đầu

II. Tổng quan
- 1. Đưa ra bài toán
- 2. Giải pháp thiết kế

III. Công nghệ sử dụng
- 1. Công nghệ phần mềm
  - 1.1. ASP.NET core 6
  - 1.2. SQL Server
  - 1.3. Javascrip
  - 1.4. HTML CSS
- 2. Công nghệ phần cứng
  - 2.1. Kit ESP32
  - 2.2. DHT22
  - 2.3. MQ135
  - 2.4. Rain
  - 2.5. Lora

IV. Xây dựng và thiết kế
- 1. Thiết kế phần cứng
- 2. Thiết kế phần mềm
- 3. Thực nghiệm

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
<img src="https://lh3.googleusercontent.com/C0UBGd6P2lemsL0wv8sFeJD_JbPGhPN5SMFzuse0mUee7z7zFKn0vZSsj4XbWL1Ux6cd9QXsaSj9lHNSNU2QAJRBiOJV6n986tezaT1Grvcr36CxCjiVSWSpriFsxah_oZq9PpmKXQ91yiv7GP8HI59GZiPx1DNK11xx5VuWbxwN7Iqiej7AsqOHXs66RfUx4kyOccXM2dFv-XAi1uQ-So8jpuECZ9of09SOldZayKwq8O4aA3Ru5banoLAXZE2-J1OnyJVCfmGJpJgw6rRTQ5YsGA_uMyEJq8QhdnYGBonEdyiP8orLDx2T5NCHMNGQFprb9FUHOqwVkD2yGyCu4EiaPCaNj5YhPBv60QJKq9t27sowVh0mfKAuLta_EU9CfC5yHIXA7FjTOsCE-o5ioS086cbbor0UpCE1bBArt-3fNU4fgbpk8wBV43UwdrncsOmtKFvqDUwWvsCacciJIHQDYneqfW0yINgWJoyqnytfUYTuG7_TiWhzIlIadQmiuleEJl9UOsuZZyP_OjNDfv-1eYRZb_hYN_M7UPj6brZyEjRvTVX3KLF5BaCkdSHnPxrgeKbeNCsEHchib9Ms3K9Eu49HOsC4HSAT2DLgno49u27FJM16WPVqRZp_YVDrhAe-CBAUQJe-PDMaTTceADMhrrOsll02h7120N_Zu_s9xiGfxEXGdzCUldybOYQCftuTbiMfkfrLKaLuQP8Lu9c-IjI7oJ7kyTlCoScyPIEkoz5-BDtt1mcVdKu71PsJzYbtQai3g4CPOP--tjob7XyMaZerukBfEts3KHwVYfQkIqCapGFFpyjXM5H_YbwAvLoVNs-A-w7qFSl6GxiTtlLeQ2UC1_RU5JxfN9sUO_zLEgJCB4C17TWSln1eWMXpkvHIeiwz2RmOAjhFNlliK98XSuSs80SPr9Zd0kRTaN1Kc3QeWUPOD32UC5MkFKRd9DbeqqupA5_TSTbFyf4E5QQx2MnVZGfVqOqgGadD03US_N8dOZcDjkAKGkZuILiYqNuuqLtd7Yseyh_9aoa8gh5nh0Q=w767-h321-s-no?authuser=3">

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

## IV. Xây dựng và thiết kế
<img src="https://lh3.googleusercontent.com/U2H9R5U7Dqwtsb7XlDhHHlqerUkKe0GvFbCTVl2Bzj6xX22NFGmoeIP3vIUUNxiJlJduSHSqW6rXyMtsUGkoxe-AiovtlTuK9tQ0uJG3roA6U4iwxtY5lUf7QQejlcu92_v_JvEriX7tWIXpilli4R0e1--eBscGH0AXrFdIA_fLRWQldU06cVMNJLIu2UYGJ6sPetD1BpOhHdkJq7aa-4yQYPtKrw7QdJExltST8XseaRaXLZ7iW8gZszt586H2Z1ldr0jJ8NEVrdccq9tHMNu_z2U3K0QATClhH6fHiR0Z2oTTlR4rZ99arPTArpYi_X6hyjQv_C9aQpknCaZtGzBkyrEvyT3XZnvOqaL0SFheIgCX9EKZzrPN5_rR-hssrcaXQ7pRoL_SqpMA9hvY8ljBIzspadkJHDaUHmgnAWiGgahjb6S0F9bjVleihw_9OieCuVfmVe6bdYywMhadhXLSpYpjqtdOfGlKiIAUaUFzuFG234Bp6N3oxjApCZS1lSf-GovBjSWRIdlPYZhxHyhSUCngLmtWT8shlifr_DO_-7YtgEaf292Gk6RFTGXVkgwkY5Ml2xHMqoTvIkfyO25AnfjtLrqCMXbQAv7x48dLxoAYGvJVTzYS-AAynO2w3lLdorJCKJGViM0GWxLFatKLzmRS7TEcmTw45KiurxnmoA6k_YAp0pOdw7X-XurYbonCrfV8vF8V-7bY98XugfIT9nb7CMzMzW7ZhlajcrgPZLJ4k2DXtqr8SUCjFJm70R4ou3xFUuoNR_XhAw7kckv_sdHpa3zV_gbs97UMRfuNx2W-XifA-Mm_yWZWy_wvjtsPJxnKPtB-pYAU2X0JabiKiDz-yGu5CmA0Y424EO8fdtWvtWYzgyVHaTz8KW3WSJHiuIdP6PFcQTEM1TvK107RlpPNtEA_lCYWw74alewUim2JjwbHlhbffdEtIbvVQNOvRSiU3a2DzvOrX89RYt_TtaoWn0zAeLqWJ-OCMj2fS5ypAnd8cobxjI03gUw6ZflULTHFFyGG_Kf5YLQ_WIH7r_U=w1916-h893-s-no?authuser=3">
<img src="https://lh3.googleusercontent.com/9LvMdkNBtqUkQkvfFqf5VFjAKbqHDWONbtcfdl_8ZizNJBOU3rrMx9bUkUPNhdGXf1UsMud8blT2mjGj64jQaqGHiz9UIoYd7KWiEpUXmaihLl5Pj7Svo75tw07oIFh5au39M-rTEAe-PxI4FF-luGGc_ajtt4ydueTibIZctBSBKittqtMb-9XOTmnX0V4HFL3GiVQ3s3UUowZb-SdysRB_5iRRsA3wkR-zTPu-GwWUZfLEaTHFCPBa-L3RBhcMsjcS0l4Sy1iIEj36jhJl70m33IlssfBpp3FffRA8YiDPpd-K093kYb1kMCsC77rSO1phGalXhFqMfllMSlaEjzHoFgovimYoMRIC2d9_DEuw9JKGa7W2nhrvYxOZrnLiwiCnYXjxknxR3aTsWCH0iSOux6aumDxR-YSxFE0yvTr6ex6R7FniRPJc2cMi9GLHdvQ_036nHs9jGUEROeaG1brogCQ5WOEWQRDontUWB6O_-Xwb8FTic5bmmUxHjA_Y16LmlJvXL3JpMAn-1VKdr12ssP1o7Az5pQCZUEym2D-fXMLMl7UdJkTxLojSdLBObcpXnquGp8cIKafJOj1V-59l2ieRwmtVrkRXR3tGSRTfCtaeL2pPZ3MFuesqhbjx65GDq6W9ULZP5XcYUQpjWSHW_BmSXJdzCfbPNfTNdi22NbvlMDscyYfXV-dvY5gOF8TUNYgqMu5vcDN_dMZAaWJoHpkIOIJqkpUOHt1DXANdnbFa5-7ESN88ZCh_Rt_vK4BCk045RiyjydUh9L-6QLkFv1-hdxC1j9AHrVbu5YGFNW520FRBdIJUkhLy009x83ra7msSXxmI-WjM7QNOfz4_3Vr0KNveuU2kbiyOFLSTjKAk6U93quBn7U4-LhoNO6nHPhFSk4-cHOnTbDE-W2PTxNnSlP8niKSXC_CMO7mdWE0CmOfX5wVKz2P3J3oO6F7u2EK3Gr5Xf49LkDPVfQfO0z8h5fBkyP-46dB1mFJMAm_nyIk6D2k_CjOT4EaHQaCo1mKUGt-iuN3r7QTh8S17AlQ=w1917-h887-s-no?authuser=3">
