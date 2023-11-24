package pooling;

import java.util.ArrayList;
import java.util.List;

public class ObjectPool<T> implements IPooling<T> {
    private List<T> pool = new ArrayList<>();
    private int poolSize = 100;
    private Class<T> objectClass;

    public ObjectPool(Class<T> objectClass) {
        this.objectClass = objectClass;
    }

    @Override
    public T createObject() {
        try {
            return objectClass.getDeclaredConstructor().newInstance();
        } catch (Exception e) {
            return null;
        }
    }

    @Override
    public T acquire() {
        if (pool.isEmpty()) {
            return createObject();
        }
        return pool.remove(0);
    }

    @Override
    public void release(T t) {
        pool.add(t);
    }

    @Override
    public void clear() {
        pool.clear();
    }

    @Override
    public int getPoolSize() {
        return pool.size();
    }

    @Override
    public void setPoolSize(int size) {
        poolSize = size;
    }

    @Override
    public void prepopulate(int count) {
        if(count <= 0) return;
        for (int i = 0; i < count; i++) {
            if (pool.size() > poolSize) break;
            pool.add(createObject());
        }
    }
}
