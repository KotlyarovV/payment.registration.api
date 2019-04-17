using AutoMapper;
using AutoMapper.Configuration;

namespace Payment.Registration.App.Builders
{
    public abstract class AbstractBuilder<TSource, TDestination>
    {
        private readonly IMapper mapper;

        protected abstract void CreateMapping(IMappingExpression<TSource, TDestination> cfg);
        
        protected AbstractBuilder()
        {
            var cfg = new MapperConfigurationExpression();
            CreateMapping(cfg.CreateMap<TSource, TDestination>());
            mapper = new Mapper(new MapperConfiguration(cfg));
        }

        public virtual TDestination Build(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }

        public virtual void Map(TSource source, TDestination destination)
        {
            mapper.Map(source, destination);
        }
    }
    
}