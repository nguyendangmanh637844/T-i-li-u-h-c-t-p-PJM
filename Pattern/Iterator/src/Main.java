import aggregate.StudentList;
import iterator.IInterator;
import model.Student;

import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        List<Student> students = new ArrayList<>();
        students.add(new Student(1, "John"));
        students.add(new Student(2, "Jane"));
        students.add(new Student(3, "Jim"));
        students.add(new Student(4, "Jill"));
        students.add(new Student(5, "Jack"));
        StudentList studentList = new StudentList(students);
        IInterator studentIterator = studentList.createIterator();
        while (studentIterator.hasNext()){
            System.out.println(studentIterator.next().toString());
        }
    }
}