package aggregate;

import iterator.IInterator;
import iterator.StudentIterator;
import model.Student;

import java.util.List;

public class StudentList implements IAggregate {
    private List<Student> list;

    public StudentList(List<Student> list) {
        this.list = list;
    }

    public List<Student> getList() {
        return list;
    }

    public Student get(int index) {
        return list.get(index);
    }

    public void setList(List<Student> list) {
        this.list = list;
    }

    @Override
    public IInterator createIterator() {
        return new StudentIterator(this);
    }
}
