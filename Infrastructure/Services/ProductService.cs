using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.IServices;
using Infrastructure.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly TechShopDbContext _context;
        public ProductService(TechShopDbContext context)
        {
            _context = context;
        }
      
        public List<ProductViewModel> SearchProduct(ProductFilterModel filter)
        {
            //tao list
            var productViewModels = new List<ProductViewModel>();
            var products = _context.Products.AsQueryable();
            var categories = _context.Categories.AsQueryable();
            var images = _context.ProductImages.AsQueryable();
            var reviews = _context.Reviews.AsQueryable();
            // join cate
            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.Id
                         select new ProductViewModel
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Price = p.Price,
                             DiscountPrice = p.DiscountPrice,
                             Description = p.Description,
                             Detail = p.Detail,
                             CategoryName = c.Name
                         };
            //filter
            // check filter.CategoryId != null && đổi CategoryId sang kiểu Guid
            if (!string.IsNullOrEmpty(filter.CategoryId) && Guid.TryParse(filter.CategoryId, out Guid categoryId))
            {
                result = result.Where(s => s.CategoryId == categoryId);
            }
            // check Price có Value
            if (filter.Price.HasValue)
            {
                result = result.Where(s => s.Price == filter.Price.Value);
            }
            // check keyword
            if (!string.IsNullOrEmpty(filter.KeyWord))
            {
                result = result.Where(s => s.Name.Equals(filter.KeyWord) || s.CategoryName.Equals(filter.KeyWord));
            }
            // xx tăng theo giá
            if (filter.SortBy.Equals(SortEnum.Price))
            {
                result = result.OrderBy(s => s.Price);
            }
            else // xx tăng theo tên
            {
                result = result.OrderBy(s => s.Name);
            }
            // index - 1 * size. nếu index = 1 thì lấy 9 sp, index = 2 thì lấy sp thứ 10 -> 18
            productViewModels = result.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            // nếu có ảnh 
            foreach (var item in productViewModels)
            {
                // lấy ra ảnh đàu tiên  nếu k thì trả về null, ? có thể null và trả về nul
                var image = images.FirstOrDefault(s => s.ProductId == item.Id)?.ImageLink;
                // nếu ảnh rỗng thì .Empty còn k thì ra image
                item.Image = string.IsNullOrEmpty(image) ? string.Empty : image;

                var rating = reviews.Where(s => s.ProductId == item.Id).Max(s => s.Rating);
                //nếu có thì lấy value còn k thì cho = 0 rồi ẩn đi
                item.Rating = rating;
            }
            return productViewModels;
        }
    }
}
