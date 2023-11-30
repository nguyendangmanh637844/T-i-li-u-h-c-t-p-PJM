package com.demo.spring.services;

import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface ISevice<T, ID> {
    void addPerson(T person);
    void updatePerson(T person);
    void deletePerson(T person);
    T getPerson(ID id);
    List<T> getAll();
}
