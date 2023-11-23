public class Subcriber2 implements ISubcriber {
    @Override
    public String recieve(String message) {
        System.out.println("Subcribe2:" + message);
        return "Send mail to: " + message;
    }
}
