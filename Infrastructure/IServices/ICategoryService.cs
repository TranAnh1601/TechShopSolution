using Infrastructure.Models.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetCategories();
    }
}
