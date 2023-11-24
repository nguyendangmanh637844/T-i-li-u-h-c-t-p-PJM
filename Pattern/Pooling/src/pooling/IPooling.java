package pooling;

public interface IPooling<T> {
    T createObject();

    T acquire(); //Lấy ra obj

    void release(T t); // Tra ve obj

    void clear(); // Xóa các obj trong pool

    int getPoolSize(); // Kiem tra so luong obj trong pool

    void setPoolSize(int size); // Cap nhat so luong obj trong pool

    void prepopulate(int count); // Fill pool với một số lượng đối tượng
}
