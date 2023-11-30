package com.demo.spring.repositories;

import com.demo.spring.models.Person;
import org.springframework.stereotype.Repository;

import java.util.List;
@Repository
public interface IRepository <T,ID>{
    List<T> getAll();
    T get(ID id);
    T add(T t);
    T update(T t);
    T delete(T t);
    void commit();
    void rollback();
}
