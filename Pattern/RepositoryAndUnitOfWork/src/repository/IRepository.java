package repository;

import java.util.List;

public interface IRepository<T, ID> {
    void add(T obj);

    void update(T obj);

    void delete(T obj);

    List<T> getAll();

    T getById(ID id);
}
