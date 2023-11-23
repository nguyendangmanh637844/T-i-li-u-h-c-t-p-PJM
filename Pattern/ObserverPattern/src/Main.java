public class Main {
    public static void main(String[] args)
    {
        Pulisher pulisher = new Pulisher();
        Subcriber1 subcriber1 = new Subcriber1();
        Subcriber2 subcriber2 = new Subcriber2();
        pulisher.addSubcriber(subcriber1);
        pulisher.addSubcriber(subcriber2);
        pulisher.notify("hello");
    }
}