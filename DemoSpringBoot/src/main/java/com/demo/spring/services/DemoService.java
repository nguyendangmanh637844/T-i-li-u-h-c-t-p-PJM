package com.demo.spring.services;

import com.demo.spring.repositories.IRepository;
import com.demo.spring.unitofwork.IUnitOfWork;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.List;

@Component("demoService")
public class DemoService<T, ID> implements ISevice<T, ID> {
    private IRepository<T, ID> _personRepo;
    private IUnitOfWork _uow;

    @Autowired
    public DemoService(IRepository<T, ID> personRepo, IUnitOfWork uow) {
        _personRepo = personRepo;
        _uow = uow;
        _uow.register(_personRepo);
    }

    public void addPerson(T person) {
        _personRepo.add(person);
        _uow.commit();
    }

    public void updatePerson(T person) {
        _personRepo.update(person);
        _uow.commit();
    }

    public void deletePerson(T person) {
        _personRepo.delete(person);
        _uow.commit();
    }

    public T getPerson(ID id) {
        return _personRepo.get(id);
    }

    public List<T> getAll() {
        return _personRepo.getAll();
    }
}
