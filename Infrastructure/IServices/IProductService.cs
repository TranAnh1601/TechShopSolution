using Infrastructure.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    public interface IProductService
    {
        List<ProductViewModel> SearchProduct(ProductFilterModel filter);
    }
}
