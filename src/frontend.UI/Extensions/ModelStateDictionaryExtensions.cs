using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace frontend.UI.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static bool IsFieldValid(this ModelStateDictionary modelStateDictionary, string fieldName)
        {
            return modelStateDictionary.GetFieldValidationState(fieldName) == ModelValidationState.Valid;
        }
    }
}
