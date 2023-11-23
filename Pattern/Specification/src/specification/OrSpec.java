package specification;

import java.util.List;

public class OrSpec<T> implements ISpecification<T> {
    private List<ISpecification<T>> specifications;

    public List<ISpecification<T>> getSpecifications() {
        return specifications;
    }

    public void setSpecifications(List<ISpecification<T>> specifications) {
        this.specifications = specifications;
    }

    public OrSpec(List<ISpecification<T>> specifications) {
        this.specifications = specifications;
    }

    @Override
    public boolean isSatisfiedBy(T obj) {
        return specifications.stream().anyMatch(specification -> specification.isSatisfiedBy(obj));
    }
}
