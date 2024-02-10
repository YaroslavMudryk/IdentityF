﻿using IdentityF.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IdentityF.Core.Handlers
{
    public abstract class BaseEndpointHandler
    {
        public virtual void ValidateModel<TModel>(TModel model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            var errors = new Dictionary<string, IEnumerable<string>>();

            foreach (var error in validationResults)
            {
                if (errors.ContainsKey(error.MemberNames.First()))
                {
                    errors[error.MemberNames.First()] = errors[error.MemberNames.First()].Append(error.ErrorMessage);
                }
                else
                {
                    errors.Add(error.MemberNames.First(), new List<string>() { error.ErrorMessage });
                }
            }

            if (!isValid)
                throw new FailedValidationException(errors);
        }
    }
}
