package command;

public class TurnOnLightCommand implements ICommand{
    private Light light;

    public TurnOnLightCommand(Light light) {
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
        light.turnOn();
    }
}
