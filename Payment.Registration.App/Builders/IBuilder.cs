namespace Payment.Registration.App.Builders
{
    public interface IBuilder<TSource, TDestination>
    {
        TDestination Build(TSource source);
    }

    public interface IBuilder<TSource1, TSource2, TDestination>
    {
        TDestination Build(TSource1 source1, TSource2 source);
    }
}