import java.util.Arrays;

public class BubbleSort implements ISort{
    @Override
    public int[] sort(int[] data) {
        int[] sortedData = Arrays.copyOf(data, data.length);
        int n = sortedData.length;
        boolean swapped;
        do {
            swapped = false;
            for (int i = 1; i < n; i++) {
                if (sortedData[i - 1] > sortedData[i]) {
                    int temp = sortedData[i - 1];
                    sortedData[i - 1] = sortedData[i];
                    sortedData[i] = temp;
                    swapped = true;
                }
            }
        } while (swapped);
        return sortedData;
    }
}
