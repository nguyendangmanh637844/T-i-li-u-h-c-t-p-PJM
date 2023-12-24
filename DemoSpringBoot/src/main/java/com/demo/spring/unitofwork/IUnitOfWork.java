package com.demo.spring.unitofwork;

import com.demo.spring.repositories.IRepository;
import org.springframework.stereotype.Component;

@Component
public interface IUnitOfWork {
    void register(IRepository<?, ?> repository);

    void unRegister(IRepository<?, ?> repository);

    void commit();

    void rollback();
}
