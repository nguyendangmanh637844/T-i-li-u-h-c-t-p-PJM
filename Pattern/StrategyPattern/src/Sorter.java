public class Sorter {
    private ISort sorter;

    public Sorter(ISort sorter) {
        this.sorter = sorter;
    }

    public ISort getSorter() {
        return sorter;
    }

    public void setSorter(ISort sorter) {
        this.sorter = sorter;
    }

    public int[] sort(int[] data){
        return sorter.sort(data);
    }
}
