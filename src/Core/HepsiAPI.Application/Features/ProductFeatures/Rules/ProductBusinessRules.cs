using HepsiAPI.Application.Exceptions;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Application.Rules;

namespace HepsiAPI.Application.Features.ProductFeatures.Rules
{
    public class ProductBusinessRules : BaseRules
    {
        private readonly IProductReadRepository _productReadRepository;

        public ProductBusinessRules(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }


        public async Task ProductTitleCanNotBeDuplicatedWhenInserted(string productTitle)
        {
            var productExists = await _productReadRepository.ExistsAsync(p => p.Title == productTitle);

            if (productExists)
            {
                throw new BusinessException("Ayni isimde iki urun olamaz");
            }
        }

        public async Task ProductExistsAsync(int id)
        {
            var productExists = await _productReadRepository.ExistsAsync(p => p.Id == id);

            if (productExists)
            {
                throw new BusinessException($"Product {id} does not exist");
            }
        }

    }
}
