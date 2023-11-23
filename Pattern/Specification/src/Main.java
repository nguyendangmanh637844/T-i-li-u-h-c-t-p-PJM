import model.Person;
import specification.ISpecification;
import specification.PersonSpec;

import java.security.spec.NamedParameterSpec;

public class Main {
    public static void main(String[] args) {
        Person person = new Person();
//        person.setName("John");
        person.setAddress("123 Main St");
        person.setAge(25);

        ISpecification<Person> nameSpec = new PersonSpec();
        boolean isValid = nameSpec.isSatisfiedBy(person);
        System.out.printf(isValid ? "Valid" : "Invalid");
    }
}