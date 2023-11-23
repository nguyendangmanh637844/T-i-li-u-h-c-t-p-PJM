import command.ICommand;

public class Invoker {
    private ICommand command;

    public Invoker() {
    }

    public Invoker(ICommand command) {
        this.command = command;
    }

    public ICommand getCommand() {
        return command;
    }

    public void setCommand(ICommand command) {
        this.command = command;
    }
    public void invoke(){
        this.command.excecute();
    }
}
