using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 帳號密碼檢查
    /// </summary>
    internal class AuthenticateRequest:RequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">用戶識別碼：HN號碼(八碼)</param>
        /// <param name="password">用戶密碼</param>
        public AuthenticateRequest(
            string userId,
            string password
            ):base()
        {
            const int UserIdLength = 8;

            const int PasswordMaxLength = 8;

            if (userId.Length != UserIdLength)
            {
                throw new ArgumentOutOfRangeException("userId", $"用戶識別碼應為八碼");
            }

            if (password.Length > PasswordMaxLength)
            {
                throw new ArgumentOutOfRangeException("password", $"用戶密碼最多八碼");
            }

            this.Type = RequestType.Authenticate;

            this.Set = new[] { userId, password };

        }
    }
}
