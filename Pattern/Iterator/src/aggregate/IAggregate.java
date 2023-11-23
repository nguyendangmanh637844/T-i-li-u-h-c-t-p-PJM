package aggregate;

import iterator.IInterator;

public interface IAggregate {
    IInterator createIterator();
}
