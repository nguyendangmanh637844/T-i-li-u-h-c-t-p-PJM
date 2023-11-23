package factory;

import product.Circle;
import product.IShape;

public class CircleFactory implements IFactory {
    @Override
    public IShape create() {
        return new Circle();
    }
}
