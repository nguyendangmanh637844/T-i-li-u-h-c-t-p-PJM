package factory;

import product.IShape;
import product.Rectangle;

public class RectangleFactory implements IFactory{
    @Override
    public IShape create() {
        return new Rectangle();
    }
}
