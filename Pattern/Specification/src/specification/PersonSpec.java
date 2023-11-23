package specification;

import model.Person;

public class PersonSpec implements ISpecification<Person> {
    @Override
    public boolean isSatisfiedBy(Person obj) {
        if (obj.getAddress() == null || obj.getAddress().isEmpty()) {
            return false;
        }
        if (obj.getName() == null || obj.getName().isEmpty()) {
            return false;
        }
        if (obj.getAge() <= 0) {
            return false;
        }
        return true;
    }
}
