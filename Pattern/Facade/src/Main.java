public class Main {
    public static void main(String[] args) {
        // Tạo một class gộp các logic lại với nhau để khi thao tác cho dễ dàng. VD có 3 service Giao hàng, gửi hàng, kí nhận.
        //Ở mỗi kiểu giao hàng sẽ có các bước khác nhau (dùng các hàm của service)
        // Tạo ra các kiểu logic để khi dùng thì gọi
        FacadeService service = FacadeService.getInstance();
        service.giaoHangBinhThuong();
        service.giaoHangNhanh();
    }
}