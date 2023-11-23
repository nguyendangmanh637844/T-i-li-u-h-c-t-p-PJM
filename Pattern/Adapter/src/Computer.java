public class Computer {
    private ITarget target;
    public Computer(ITarget target){
        this.target = target;
    }
    public void connect(){
        target.connect();
    }
}
