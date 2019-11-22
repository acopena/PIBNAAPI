using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PIBNAAPI.Command.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIBNAAPI.Command.Filters
{
    public class ClubResultFilterAttributes: ResultFilterAttribute
    {
        //public override async Task OnResultExecutionAsync(ResultExecutedContext context, ResultExecutionDelegate next)
        //{
        //    var resultFromAction = context.Result as ObjectResult;
        //    if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300)
        //    {
        //        await next();
        //        return;
        //    }
        //    if (typeof(IEnumerable).IsAssignableFrom(resultFromAction.Value.GetType()))
        //    {

        //    }
        //    resultFromAction.Value = Mapper.Map<ClubModel>(resultFromAction.Value);
        //    await next();
            
        //}
    }
}
