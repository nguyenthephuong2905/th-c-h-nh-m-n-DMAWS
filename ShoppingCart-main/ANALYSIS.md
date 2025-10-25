# Phân Tích Công Nghệ Cho Hệ Thống Shopping Cart

## Câu hỏi 1: Phân tích vấn đề từ góc độ nhà phát triển phần mềm (5 điểm)

### Lựa chọn công nghệ: **ASP.NET Core Web API + Entity Framework Core + Frontend Web Application**

### 3 Lý do chính cho việc lựa chọn công nghệ này:

#### 1. **Khả năng mở rộng và xử lý tải cao (Scalability)**

Từ biểu đồ Shopping Chart, ta thấy nhu cầu đặt hàng tăng đột biến vào tháng 11 (Black Friday - 12 đơn hàng) so với các tháng khác (2-3 đơn hàng). Điều này cho thấy:

- **Vấn đề:** Hệ thống cần xử lý lượng traffic tăng đột biến gấp 4-6 lần trong các sự kiện đặc biệt.
- **Giải pháp:** ASP.NET Core được thiết kế với kiến trúc async/await, cho phép xử lý hàng nghìn request đồng thời mà không bị block. Web API có thể dễ dàng scale horizontally (thêm server) khi cần.
- **Lợi ích:** Tiết kiệm chi phí vận hành vì chỉ cần tăng tài nguyên trong các tháng cao điểm, giảm trong các tháng thấp điểm.

#### 2. **Giảm chi phí xây dựng và bảo trì (Cost Efficiency)**

- **Chi phí xây dựng:** 
  - Code First Model giúp tạo database tự động từ code, giảm thời gian phát triển 30-40%.
  - Entity Framework Core cung cấp ORM mạnh mẽ, giảm thời gian viết SQL thủ công.
  - RESTful API cho phép tái sử dụng backend cho nhiều platform (web, mobile app) trong tương lai.

- **Chi phí vận hành:**
  - .NET Core là open-source và cross-platform (Windows, Linux, macOS), linh hoạt trong việc chọn hosting.
  - Có thể deploy lên cloud (Azure, AWS) hoặc on-premise tùy nhu cầu.

- **Chi phí bảo trì:**
  - Code structure rõ ràng với separation of concerns (Model, Controller, Service).
  - Dễ dàng test và debug với built-in dependency injection.
  - Long-term support từ Microsoft (LTS versions).

#### 3. **Tính linh hoạt và khả năng tích hợp (Flexibility & Integration)**

Theo yêu cầu, công ty cần "increase revenue and profits" - điều này có nghĩa hệ thống cần mở rộng tính năng trong tương lai:

- **Web API Architecture:** Cho phép frontend và backend tách biệt, dễ dàng:
  - Thêm mobile app (iOS, Android) sử dụng chung API
  - Tích hợp với hệ thống bên thứ ba (payment gateway, shipping, CRM)
  - Xây dựng dashboard analytics riêng

- **Entity Framework Core:**
  - Dễ dàng thay đổi database (SQL Server, PostgreSQL, MySQL) mà không cần viết lại code
  - Migration system giúp quản lý phiên bản database một cách có tổ chức
  - Support cho các pattern phức tạp (repository, unit of work)

- **Frontend độc lập:**
  - Có thể thay đổi công nghệ frontend (React, Vue, Angular) mà không ảnh hưởng backend
  - SEO-friendly nếu cần thiết
  - Progressive Web App (PWA) ready

### Kết luận

Với bài toán xây dựng hệ thống shopping cart cho chuỗi cửa hàng lớn, việc lựa chọn ASP.NET Core Web API + Entity Framework Core là tối ưu vì:
- **Xử lý được tải cao** trong các mùa cao điểm (Black Friday)
- **Tiết kiệm chi phí** xây dựng, vận hành và bảo trì
- **Linh hoạt mở rộng** cho các yêu cầu kinh doanh trong tương lai

---

## Thiết kế Database

**Database Name:** OrderSystem

**Table Name:** OrderTbl

| Field Name | Data Type | Description |
|------------|-----------|-------------|
| OrderId | int (PK, Identity) | Mã đơn hàng (auto-increment) |
| ItemCode | string | Mã sản phẩm |
| ItemName | string | Tên sản phẩm |
| ItemQty | int | Số lượng |
| OrderDelivery | DateTime | Thời gian giao hàng |
| OrderAddress | string | Địa chỉ giao hàng |
| PhoneNumber | string | Số điện thoại liên hệ |
| CreatedDate | DateTime | Ngày tạo đơn |
| UpdatedDate | DateTime | Ngày cập nhật |

---

*Document được tạo cho bài tập Aptech DMAWS - Shopping Cart System*

