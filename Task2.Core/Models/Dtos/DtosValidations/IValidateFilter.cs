using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Models.Dtos.DtosValidations
{
    public class IValidateFilter<T> : IAsyncActionFilter where T : class
    {
        private readonly IValidator<T> _validator;
        public IValidateFilter(IValidator<T> validator)
        {
            _validator = validator;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var model = context.ActionArguments.OfType<T>().FirstOrDefault();
            if (model != null)
            {
                var result = await _validator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    var error = result.Errors.Select(e => e.ErrorMessage).ToList();

                    context.Result = new BadRequestObjectResult(new
                    {
                        isSuccessed = false,
                        StatusCode = 400,
                        Error = error

                    });
                    return;
                }
            }
            await next();
        }
    }
}
