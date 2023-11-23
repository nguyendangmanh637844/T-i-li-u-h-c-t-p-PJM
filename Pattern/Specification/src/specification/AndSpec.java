package specification;

import java.util.List;

public class AndSpec<T> implements ISpecification<T> {
    private List<ISpecification<T>> specifications;

    public AndSpec(List<ISpecification<T>> specifications) {
        this.specifications = specifications;
    }

    public List<ISpecification<T>> getSpecifications() {
        return specifications;
    }

    public void setSpecifications(List<ISpecification<T>> specifications) {
        this.specifications = specifications;
    }

    @Override
    public boolean isSatisfiedBy(T obj) {
        return specifications.stream().allMatch(specification -> specification.isSatisfiedBy(obj));
    }
}
