namespace Payment.Registration.App.Builders
{
    public interface IBuilder<TSource, TDestination>
    {
        TDestination Build(TSource source);
    }

    public interface IBuilder<TSource1, TSource2, TDestination>
    {
        TDestination Build(TSource1 source, TSource2 files);
    }
    
    public interface IBuilder<TSource1, TSource2, TSource3, TDestination>
    {
        TDestination Build(TSource1 source, TSource2 source2, TSource3 source3);
    }
}