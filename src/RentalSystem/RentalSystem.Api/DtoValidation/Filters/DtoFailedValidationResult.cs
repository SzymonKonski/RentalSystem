using System.Collections.Generic;

namespace RentalSystem.Api.DtoValidation.Filters
{
    public record DtoFailedValidationResult
    {
        public int StatusCode { get; } = 400;
        public IList<string> ModelValidationErrors { get; }

        public DtoFailedValidationResult(IList<string> modelValidationErrors) => (ModelValidationErrors) = (modelValidationErrors);
    }
}
