using Business.Models;

namespace Business.Implementation.Validations
{
    public static class CartValidation
    {
        public static void ValidateCart(CartDto cartDto)
        {

            if (cartDto.TotalPrice < 0)
            {
                throw new ValidationException("Price can't be of negative value");
            }
        }
    }
}