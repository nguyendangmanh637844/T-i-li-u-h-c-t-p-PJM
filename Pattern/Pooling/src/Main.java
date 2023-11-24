import pooling.ObjectPool;

public class Main {
    public static void main(String[] args) {
        ObjectPool<String> pool = new ObjectPool<>(String.class);
        pool.setPoolSize(10);
        pool.prepopulate(5);
        String string = pool.acquire();
        System.out.println("String" + string);
        pool.release("Hello");
        System.out.println(pool.getPoolSize());
        pool.prepopulate(5);
        System.out.println(pool.getPoolSize());
        pool.clear();
        System.out.println(pool.getPoolSize());
    }
}