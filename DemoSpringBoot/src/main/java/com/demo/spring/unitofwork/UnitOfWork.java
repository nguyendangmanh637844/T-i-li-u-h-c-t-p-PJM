package com.demo.spring.unitofwork;

import com.demo.spring.repositories.IRepository;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
@Component
public class UnitOfWork implements IUnitOfWork {
    private List<IRepository<?, ?>> repositories = new ArrayList<>();

    @Override
    public void register(IRepository<?, ?> repository) {
        repositories.add(repository);
    }

    @Override
    public void unRegister(IRepository<?, ?> repository) {
        repositories.remove(repository);
    }

    @Override
    public void commit() {
        for (IRepository<?, ?> repository : repositories) {
            repository.commit();
        }
    }

    @Override
    public void rollback() {
        for (IRepository<?, ?> repository : repositories) {
            repository.rollback();
        }
    }
}
