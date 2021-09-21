using System;
using System.Collections.Generic;
using System.Text;

namespace Playground
{
    public class AuthorizationService: IAuthorizationService
    {
        private bool isAuthorized;

        bool IAuthorizationService.IsAuthorized(Box boxUnderTest)
        {
            isAuthorized = true;
            if (string.IsNullOrEmpty(boxUnderTest.Color) || string.IsNullOrEmpty(boxUnderTest.Name))
            {
                isAuthorized = false;
            }

            return isAuthorized;
        }
    }
}
