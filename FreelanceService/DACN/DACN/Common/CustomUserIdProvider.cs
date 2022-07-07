﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Common
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            // your logic to fetch a user identifier goes here.

            // for example:

            //var userId = MyCustomUserClass.FindUserId(request.User.Identity.Name);
            var userId = request.User.Identity.Name;
            return userId.ToString();
        }
    }
}