package iterator;

import aggregate.StudentList;

public class StudentIterator implements IInterator {
    private StudentList studentList;
    private int index = 0;

    public StudentIterator(StudentList studentList) {
        this.studentList = studentList;
    }

    @Override
    public boolean hasNext() {
        return index < studentList.getList().size();
    }

    @Override
    public Object next() {
        return studentList.get(index++);
    }
}
