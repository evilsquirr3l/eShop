using Business.Models;

namespace Business.Implementation.Validations
{
    public static class CategoryValidation
    {
        public static void ValidateCategory(CategoryDto categoryDto)
        {
            if (string.IsNullOrEmpty(categoryDto.Name))
            {
                throw new ValidationException("Name can't be empty.");
            }

            if (string.IsNullOrEmpty(categoryDto.Description))
            {
                throw new ValidationException("Description can't be empty");
            }
        }
    }
}