using AutoMapper;
using GoatEdu.Core.CustomEntities;

namespace GoatEdu.API.Mapping;

public class PagedListTypeConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
{
    private readonly IMapper _mapper;
    
    public PagedListTypeConverter(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var items = _mapper.Map<List<TDestination>>(source);
        return new PagedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}