using System.Text;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TenGymServices.Shared.Core.Utils;

namespace TenGymServices.Shared.Core.Extentions
{
    public static class ValidationRequestExtention
    {
        public static void ValidateRequest<TRequest, TValidator>(this TRequest request)
           where TRequest : IRequest
           where TValidator : AbstractValidator<TRequest>, new()
        {
            var validator = new TValidator();
            var results = validator.Validate(request);

            Validate(results);
        }

        public static void ValidateRequest<TRequest, TDto, TValidator>(this TRequest request)
            where TDto : class
            where TRequest : IRequest<TDto>
            where TValidator : AbstractValidator<TRequest>, new()
        {
            var validator = new TValidator();
            var results = validator.Validate(request);

            Validate(results);
        }

        private static void Validate(ValidationResult results)
        {
            if (!results.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in results.Errors)
                {
                    sb.AppendLine($"{failure.PropertyName}: {failure.ErrorMessage}");
                }

                throw new ValidatorExeption(sb.ToString());
            }
        }
    }
}