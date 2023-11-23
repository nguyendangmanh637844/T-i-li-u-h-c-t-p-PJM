import product.IShape;

public class Main {
    public static void main(String[] args) {
        ShapeFactory factory = ShapeFactory.getInstance();
        IShape circle = factory.createShape(ShapeEnum.CIRCLE);
        circle.draw();
        IShape rectangle = factory.createShape(ShapeEnum.RECTANGLE);
        rectangle.draw();
    }
}