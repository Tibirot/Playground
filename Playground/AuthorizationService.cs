using System;
using System.Collections.Generic;
using System.Text;

namespace Playground
{
    public class AuthorizationService: IAuthorizationService
    {
        public bool IsAuthorized(Box boxUnderTest)
        {
            return true;
        }
    }
}
