import product.Circle;
import product.IShape;
import product.Rectangle;

public class ShapeFactory {
    private ShapeFactory() {

    }

    public static ShapeFactory instance;

    public static ShapeFactory getInstance() {
        if (instance == null) {
            instance = new ShapeFactory();
        }
        return instance;
    }

    public IShape createShape(ShapeEnum shape) {
        switch (shape) {
            case CIRCLE -> {
                return new Circle();
            }
            case RECTANGLE -> {
                return new Rectangle();
            }
            default -> {
                return null;
            }
        }
    }
}
