package command;

public class TurnOffLightCommand implements ICommand{
    private Light light;

    public TurnOffLightCommand(Light light) {
        this.light = light;
    }

    public Light getLight() {
        return light;
    }

    public void setLight(Light light) {
        this.light = light;
    }

    @Override
    public void excecute() {
        light.turnOff();
    }
}
