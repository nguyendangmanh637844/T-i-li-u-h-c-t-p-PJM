import java.util.Arrays;

public class QuickSort implements ISort {
    @Override
    public int[] sort(int[] data) {
        int[] sortedData = Arrays.copyOf(data, data.length);
        Arrays.sort(sortedData);
        return sortedData;
    }
}
