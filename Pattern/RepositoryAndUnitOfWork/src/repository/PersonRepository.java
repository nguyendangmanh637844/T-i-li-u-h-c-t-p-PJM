package repository;

import model.Person;

import java.util.List;

public class PersonRepository implements IRepository<Person, Integer> {
    private static PersonRepository instance;

    private PersonRepository() {
    }

    public static PersonRepository getInstance() {
        if (instance == null) {
            instance = new PersonRepository();
        }
        return instance;
    }

    private List<Person> personList;

    public void setInstance(PersonRepository instance) {
        this.instance = instance;
    }

    public void setPersonList(List<Person> personList) {
        this.personList = personList;
    }


    @Override
    public void add(Person obj) {
        personList.add(obj);
    }

    @Override
    public void update(Person obj) {
        personList.add(obj);
    }

    @Override
    public void delete(Person obj) {
        personList.remove(obj);
    }

    @Override
    public List<Person> getAll() {
        return personList;
    }

    @Override
    public Person getById(Integer integer) {
        if (integer >= personList.size()) {
            return null;
        }
        return personList.get(integer);
    }
}
