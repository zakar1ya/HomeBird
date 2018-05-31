using AutoMapper;

namespace HomeBird.Common.Extensions
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreOther<TSource, TDest>(this IMappingExpression<TSource, TDest> mapperExpression)
        {
            mapperExpression.ForAllOtherMembers(u => u.Ignore());

            return mapperExpression;
        }
    }
}
