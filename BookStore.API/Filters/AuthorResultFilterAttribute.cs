using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Filters
{
    public class AuthorResultFilterAttribute : ResultFilterAttribute
    {
        private IMapper _mapper = null;
        public override async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if(resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }
            resultFromAction.Value = _mapper.Map<Models.AuthorDto>(resultFromAction.Value);
            await next();
        }
    }
}
