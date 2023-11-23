import model.Person;
import repository.IRepository;
import repository.PersonRepository;
import IUnitOfWork.PersonUOW;

import java.util.ArrayList;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        //Tạo repository và unit of work
        PersonRepository personRepository = PersonRepository.getInstance();
        PersonUOW personUOW = PersonUOW.getInstance();
        personUOW.registerRepository(personRepository);

        List<Person> personList = new ArrayList<>();

        personRepository.setPersonList(personList);
        personRepository.add(new Person("name1", 1, "address1"));
        personRepository.add(new Person("name2", 2, "address2"));
        personRepository.add(new Person("name3", 3, "address3"));
        personRepository.add(new Person("name4", 4, "address4"));
        personRepository.add(new Person("name5", 5, "address5"));
        personRepository.add(new Person("name6", 6, "address6"));
        personRepository.add(new Person("name7", 7, "address7"));
        personRepository.add(new Person("name8", 8, "address8"));
        personRepository.add(new Person("name9", 9, "address9"));
        personRepository.add(new Person("name10", 10, "address10"));
        personRepository.add(new Person("name11", 11, "address11"));
        personRepository.add(new Person("name12", 12, "address12"));
        personRepository.add(new Person("name13", 13, "address13"));
        personRepository.add(new Person("name14", 14, "address14"));
        personRepository.add(new Person("name15", 15, "address15"));

        System.out.println(personRepository.getAll().toString());
        personUOW.commit();
    }
}