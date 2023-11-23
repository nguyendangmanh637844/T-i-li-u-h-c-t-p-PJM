package IUnitOfWork;

import repository.IRepository;

public interface IUnitOfWork {
    void commit();
    void rollback();
    <T, ID> void registerRepository(IRepository<T, ID> repository);
    <T, ID> void unRegisterRepository(IRepository<T, ID> repository);
}
