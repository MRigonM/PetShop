using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetShop.Domain.Common;

namespace PetShop.Extensions;

public static class ValidationExtensions
{
    public static void AddErrorsToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }

    public static void AddErrorsToModelState<T>(this Result<T> result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(string.Empty, error.Description);
        }
    }
}