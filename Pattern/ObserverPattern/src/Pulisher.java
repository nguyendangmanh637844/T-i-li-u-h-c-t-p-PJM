import java.util.ArrayList;
import java.util.List;

public class Pulisher {
    List<ISubcriber> subcribers;

    public Pulisher() {
        this.subcribers = new ArrayList<>();
    }

    public Pulisher(List<ISubcriber> subcribers) {
        this.subcribers = subcribers;
    }

    public void addSubcriber(ISubcriber subcriber) {
        this.subcribers.add(subcriber);
    }

    public void removeSubcriber(ISubcriber subcriber) {
        this.subcribers.remove(subcriber);
    }

    public List<ISubcriber> getSubcribers() {
        return subcribers;
    }

    public void setSubcribers(List<ISubcriber> subcribers) {
        this.subcribers = subcribers;
    }

    public void notify(String message) {
        for (ISubcriber subcriber : this.subcribers) {
            subcriber.recieve(message);
        }
    }
}
