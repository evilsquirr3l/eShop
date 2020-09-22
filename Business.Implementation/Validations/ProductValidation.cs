using Business.Models;

namespace Business.Implementation.Validations
{
    public static class ProductValidation
    {
        public static void ValidateProduct(ProductDto productDto)
        {
            if (productDto.Category == null)
            {
                throw new ValidationException("Category can't be null.");
            }

            if (string.IsNullOrEmpty(productDto.Name))
            {
                throw new ValidationException("Name can't be empty.");
            }

            if (productDto.Price < 0)
            {
                throw new ValidationException("Price can't be less than 0.");
            }

            if (string.IsNullOrEmpty(productDto.ImageUrl))
            {
                throw new ValidationException("Please provide image url.");
            }
        }
    }
}