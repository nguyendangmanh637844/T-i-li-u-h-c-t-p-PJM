chú ý: Khi tạo entity thì nhớ implements Serializable để có thể đẩy lên redis
ví dụ: public class User implements Serializable {
    private static final long serialVersionUID = 1L;
	//Các thuộc tính khác
    }