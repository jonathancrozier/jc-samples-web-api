using JC.Samples.Web.Api.Errors.Extensions;
using JC.Samples.Web.Api.Extensions;

namespace JC.Samples.Web.Api.Errors
{
    /// <summary>
    /// Provides methods for creating API error-related objects.
    /// </summary>
    public class ApiErrorFactory
    {
        #region Methods

        /// <summary>
        /// Creates an API Problem Details model from the specified API Exception.
        /// </summary>
        /// <param name="apiException">The API Exception to create the model from</param>
        /// <returns>An API Problem Details model</returns>
        public static ApiProblemDetails Create(ApiException apiException)
        {
            var instance = new ApiErrorInstanceUrn(apiException);

            return new ApiProblemDetails
            {
                Title            = apiException.HttpStatus.ToString().ToDisplayFormat(),
                Status           = (int)apiException.HttpStatus,
                Detail           = apiException.ErrorMessage ?? apiException.HttpStatus.GetDefaultMessage(),
                Instance         = instance.ToString(),
                Code             = instance.ErrorCodeString,
                Category         = instance.ErrorCategoryString,
                ValidationErrors = apiException.ValidationErrors
            };
        }
        
        #endregion
    }
}