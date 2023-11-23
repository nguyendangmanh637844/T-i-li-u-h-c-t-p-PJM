package IUnitOfWork;

import repository.IRepository;

import java.util.ArrayList;
import java.util.List;

public class PersonUOW implements IUnitOfWork {
    private static PersonUOW instance;
    private static List<IRepository<?, ?>> personRepository;

    private PersonUOW() {
    }

    public static PersonUOW getInstance() {
        if (instance == null) {
            instance = new PersonUOW();
        }
        if (personRepository == null) {
            personRepository = new ArrayList<>();
        }
        return instance;
    }


    @Override
    public void commit() {
        System.out.printf("commit");
    }

    @Override
    public void rollback() {
        System.out.printf("rollback");
    }

    @Override
    public <T, ID> void registerRepository(IRepository<T, ID> repository) {
        personRepository.add(repository);
    }

    @Override
    public <T, ID> void unRegisterRepository(IRepository<T, ID> repository) {
        personRepository.remove(repository);
    }
}
