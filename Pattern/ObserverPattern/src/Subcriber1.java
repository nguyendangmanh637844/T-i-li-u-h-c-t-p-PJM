public class Subcriber1 implements ISubcriber {
    @Override
    public String recieve(String message) {
        System.out.println("Subcribe1:" + message);
        return "Subcribe1:" + message;
    }
}
