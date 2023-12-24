package com.demo.spring.repositories;

import lombok.extern.log4j.Log4j;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
public class DemoRepository<T, ID> implements IRepository<T, ID> {
    private List<T> list = new ArrayList<>();


    @Override
    public List<T> getAll() {
        return list;
    }

    @Override
    public T get(ID id) {
        try {
            int index = (int) id;
            if (index >= list.size()) {
                return null;
            }
            return list.get(index);
        } catch (Exception e) {
            return null;
        }
    }

    @Override
    public T add(T t) {
        list.add(t);
        return t;
    }

    @Override
    public T update(T t) {
        return t;
    }

    @Override
    public T delete(T t) {
        list.remove(t);
        return t;
    }

    @Override
    public void commit() {

    }

    @Override
    public void rollback() {

    }
}
