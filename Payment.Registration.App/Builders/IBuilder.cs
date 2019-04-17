namespace Payment.Registration.App.Builders
{
    public interface IBuilder<TSource, TDestination>
    {
        TDestination Build(TSource source);

        void Map(TSource source, TDestination destination);
    }
    
    public interface IBuilder<TSource1, TSource2, TDestination>
    {
        TDestination Build(TSource1 source, TSource2 files);
    }

    public interface IMapper<TSource1, TSource2, TDestination>
    {
        TDestination Map(TSource1 paymentDto, TSource2 positions, TDestination destination);
    }
    
    public interface IBuilder<TSource1, TSource2, TSource3, TDestination>
    {
        TDestination Build(TSource1 source, TSource2 source2, TSource3 source3);
    }
}