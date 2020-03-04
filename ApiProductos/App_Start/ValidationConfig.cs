using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace ApiProductos.App_Start
{
    public static class ValidationConfig
    {
        public static Dictionary<string, string> GetErrors(this ModelStateDictionary items)
        {
            var result = new Dictionary<string, string>();

            foreach (var modelStateKey in items.Keys)
            {
                var modelStateVal = items[modelStateKey];

                foreach (var error in modelStateVal.Errors)
                {
                    result.Add(modelStateKey, error.ErrorMessage);
                }
            }

            return result;
        }
    }
}