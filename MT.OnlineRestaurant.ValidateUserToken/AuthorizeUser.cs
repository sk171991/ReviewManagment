﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Text;

namespace MT.OnlineRestaurant.ValidateUserToken
{
    public class AuthorizeUser : IAuthorizationFilter
    {
        /// <summary>
        /// Get the data like token after authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            Microsoft.Extensions.Primitives.StringValues token;
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (SkipAuthorization(controllerActionDescriptor.MethodInfo))
            {
                return;
            }
            if (filterContext.HttpContext.Request.Headers == null)
            {
                throw new Exception(@"Invalid request format");
            }
            else
            {
                filterContext.HttpContext.Request.Headers.TryGetValue("AuthToken", out token);

            }
            /*custom class to validate users*/
            ValidateUserInfo validateUser = new ValidateUserInfo();
            var IsAuthenticatedstatusCode = validateUser.ValidateToken(filterContext.HttpContext.Request.Headers["AuthToken"]);
            if (IsAuthenticatedstatusCode == System.Net.HttpStatusCode.OK)
            {
                {

                    System.Threading.Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(filterContext.HttpContext.Request.Headers["CustomerId"].ToString()), null);
                }
            }
            else
                throw new Exception(@"Invalid User");

        }

        public bool SkipAuthorization(MethodInfo methodList)
        {
            string AllowAnonymous = "Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute";
            if (methodList != null)
            {
                var actionAttributes = methodList.GetCustomAttributes(inherit: true);
                foreach (var attribute in actionAttributes)
                {
                    string type = attribute.GetType().ToString();
                    if (type == AllowAnonymous)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
