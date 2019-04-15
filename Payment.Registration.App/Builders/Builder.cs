using AutoMapper;

namespace Payment.Registration.App.Builders
{
    public class Builder<TSource, TDestination> : AbstractBuilder<TSource, TDestination>, IBuilder<TSource, TDestination>
    {
        protected override void CreateMapping(IMappingExpression<TSource, TDestination> cfg)
        {
        }
    }
}