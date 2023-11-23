package specification;

public interface ISpecification<T> {
    /**
     * Checks if the given object satisfies the condition.
     *
     * @param obj The object to be checked.
     * @return True if the object satisfies the condition, false otherwise.
     */
    boolean isSatisfiedBy(T obj); // được thỏa mãn bởi
}
