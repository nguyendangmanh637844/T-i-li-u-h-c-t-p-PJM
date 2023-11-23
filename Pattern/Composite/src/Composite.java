import java.util.ArrayList;
import java.util.List;

public class Composite implements IComponent {
    private List<IComponent> composite = new ArrayList<>();
    private String name;

    public Composite(String name) {
        this.name = name;
    }

    public void addLeaf(IComponent leaf) {
        this.composite.add(leaf);
    }

    public void removeLeaf(IComponent leaf) {
        this.composite.remove(leaf);
    }

    @Override
    public String getName() {
        for (IComponent leaf : composite) {
            name += "/" + leaf.getName();
        }
        return name;
    }
}
