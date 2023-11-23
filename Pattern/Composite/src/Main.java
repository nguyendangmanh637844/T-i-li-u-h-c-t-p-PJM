public class Main {
    public static void main(String[] args) {
        IComponent folder = new Leaf("folder1");
        Composite dictionary = new Composite("dictionary");
        dictionary.addLeaf(folder);
        Composite volumn = new Composite("volume");
        volumn.addLeaf(dictionary);
        System.out.printf(volumn.getName());
    }
}