using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DepartmentAutomation.Web.ResponseProblemDetails
{
    /// <summary>
    /// A <see cref="ProblemDetails"/> for validation errors.
    /// </summary>
    public class FluentValidationProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationProblemDetails"/> class.
        /// Initializes a new instance of <see cref="FluentValidationProblemDetails"/>.
        /// </summary>
        public FluentValidationProblemDetails()
        {
            Title = "One or more validation errors occurred.";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationProblemDetails"/> class.
        /// Initializes a new instance of <see cref="FluentValidationProblemDetails"/> using the specified <paramref name="modelState"/>.
        /// </summary>
        /// <param name="modelState"><see cref="ModelStateDictionary"/> containing the validation errors.</param>
        public FluentValidationProblemDetails(ModelStateDictionary modelState)
            : this()
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        Errors.Add(key, new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }

                        Errors.Add(key, errorMessages);
                    }
                }
            }

            string GetErrorMessage(ModelError error)
            {
                return string.IsNullOrEmpty(error.ErrorMessage) ?
                    "The input was not valid." :
                    error.ErrorMessage;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationProblemDetails"/> class.
        /// Initializes a new instance of <see cref="FluentValidationProblemDetails"/> using the specified <paramref name="validationFailures"/>.
        /// </summary>
        /// <param name="validationFailures">The validation errors.</param>
        public FluentValidationProblemDetails(IEnumerable<ValidationFailure> validationFailures)
            : this()
        {
            foreach (var propertyFailures in validationFailures.GroupBy(x => x.PropertyName))
            {
                Errors[propertyFailures.Key] = propertyFailures.Select(x => x.ErrorMessage).ToArray();
            }
        }

        /// <summary>
        /// Gets the validation errors associated with this instance of <see cref="FluentValidationProblemDetails"/>.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>(StringComparer.Ordinal);
    }
}