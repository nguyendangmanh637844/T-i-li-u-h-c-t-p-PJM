import factory.CircleFactory;
import factory.IFactory;
import factory.RectangleFactory;
import product.Circle;
import product.IShape;

public class Main {
    public static void main(String[] args) {
        IFactory circleFactory = new CircleFactory();
        IShape circle = circleFactory.create();
        circle.getName();
        IFactory rectangleFactory = new RectangleFactory();
        IShape rectangle = rectangleFactory.create();
        rectangle.getName();
    }
}