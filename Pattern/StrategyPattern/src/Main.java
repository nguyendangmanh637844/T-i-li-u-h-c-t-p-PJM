import java.util.Arrays;

public class Main {
    public static void main(String[] args) {
        int[] data = {1, 8, 4, 7, 5, 3};
        Sorter sorter = new Sorter(new BubbleSort());
        int[] sortedData = sorter.sort(data);
        System.out.println("Dữ liệu sau khi sắp xếp bằng Bubble Sort: " + Arrays.toString(sortedData));

        sorter.setSorter(new QuickSort());
        sortedData = sorter.sort(data);

        sorter.setSorter(new ASort());
        sortedData = sorter.sort(data);

        System.out.println("Dữ liệu sau khi sắp xếp bằng Quick Sort: " + Arrays.toString(sortedData));

//        Dùng khi muốn tách biệt các phần thay đổi mà không ảnh hưởng tới chỗ sử dụng.
//        VD: Muốn sắp xếp nhưng khi thì sắp xếp theo buble khi thì sắp xếp theo quick sort

//        Bước 1: Tạo một interface để đại diện (ISort). Tạo các class implement interface này(BubbleSort, QuickSort)
//        Bước 2: Tạo một class Context (Sorter)chứa contructor, getter, setter (để thay đổi class implement khi cần và phương thức của interface để sử dụng.
//                Khi sử dụng sẽ sử dụng class này là chính.
//        Bước 3: Khởi tạo class context và sử dụng.
    }
}
