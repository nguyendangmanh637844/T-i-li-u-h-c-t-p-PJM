import command.ICommand;
import command.Light;
import command.TurnOffLightCommand;
import command.TurnOnLightCommand;

public class Main {
    public static void main(String[] args) {
        //Đối tượng
        Light light = new Light();
        //Hai câu lệnh
        ICommand turnOn = new TurnOnLightCommand(light);
        ICommand turnOff = new TurnOffLightCommand(light);
        //Người thực hiện lệnh
        Invoker invoker = new Invoker();

        invoker.setCommand(turnOn);
        invoker.invoke();
        invoker.setCommand(turnOff);
        invoker.invoke();
    }
}