using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JC.Samples.Web.Api.Errors
{
    /// <summary>
    /// Handles model-state validation automatically.
    /// </summary>
    public class ApiValidationActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Return immediately, if the model-state is valid.
            if (actionContext.ModelState.IsValid) return;

            // Get the model-state entries.
            var modelStateEntries = actionContext.ModelState.Where(s => s.Value.Errors.Count > 0).ToArray();
            var errorMessage      = "A Validation Error occurred.";

            ICollection<ValidationError> validationErrors = null;

            if (modelStateEntries.Any())
            {
                if (modelStateEntries.Count() == 1 && 
                    modelStateEntries.First().Value.Errors.Count == 1 && 
                    modelStateEntries.First().Key == string.Empty)
                {
                    // Make the error message specific, if there is only one entry and no key.
                    errorMessage = modelStateEntries.First().Value.Errors.First().ErrorMessage;
                }
                else
                {
                    errorMessage     = "See ValidationErrors for details.";
                    validationErrors = new List<ValidationError>();

                    foreach (var modelStateEntry in modelStateEntries)
                    {
                        foreach (var modelStateError in modelStateEntry.Value.Errors)
                        {
                            string modelStateErrorMessage = modelStateError.ErrorMessage;

                            if (string.IsNullOrEmpty(modelStateErrorMessage))
                            {
                                modelStateErrorMessage = "Invalid model supplied.";
                            }

                            // Add a Validation Error for each model-state error.
                            var error = new ValidationError
                            {
                                Name        = modelStateEntry.Key,
                                Description = modelStateErrorMessage
                            };

                            validationErrors.Add(error);
                        }
                    }
                }
            }

            // Pass the model Validation Errors to the central Exception Handler.
            throw new ApiException(HttpStatusCode.BadRequest, ApiErrorCode.ResourceInvalid, errorMessage, validationErrors: validationErrors);
        }
    }
}