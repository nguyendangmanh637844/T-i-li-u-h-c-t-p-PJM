public class Singleton {
    private static Singleton instance;

    //Chặn k cho thực hiện việc khởi tạo
    private Singleton() {

    }

    public static Singleton getInstance() {
        if (instance == null) {
            instance = new Singleton();
        }
        return instance;
    }
}
