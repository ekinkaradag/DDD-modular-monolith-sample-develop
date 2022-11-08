using System.Linq;

namespace IoCore.SharedReadKernel.Product
{
    internal static class ProductSpecifications
    {
        public static IQueryable<T> That<T>(this IQueryable<T> queryable) where  T : class
        {
            return queryable;
        }

        public static IQueryable<Product>AreApprovedForUse(this IQueryable<Product> products)
        {
            return products.Where(product => product.Status == "APPROVED_FOR_USE");
        }

        public static IQueryable<Product>AreDraft(this IQueryable<Product> products)
        {
            return products.Where(requestForChange => requestForChange.Status == "DRAFT");
        }
        
        public static IQueryable<Product>AreDeprecated(this IQueryable<Product> products)
        {
            return products.Where(requestForChange => requestForChange.Status == "DEPRECATED");
        }
    }
}