using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    internal class AuthenticateResponse:ResponseBase
    {
        public AuthenticateResponse(
            byte[] bytes):base(bytes)
        {
        }

        protected override string ParseError()
        {
            switch(this.CodeKey)
            {
                case 0:
                    {
                        return null;
                    }
                case 1:
                    {
                        return "密碼錯誤";
                    }
                case 2:
                    {
                        return "帳號不存在";
                    }
                case 3:
                    {
                        return "超過允許的最大連線數目";
                    }
                case 4:
                    {
                        return "帳號狀態不正確或已退租";
                    }
                case 5:
                    {
                        return "無法取得帳號資料";
                    }
                case 6:
                    {
                        return "無法取得密碼資料";
                    }
                case 7:
                    {
                        return "暫時無法檢查帳號/密碼";
                    }
                default:
                    {
                        return $"未預期的 Code {this.CodeKey:0}";
                    }
            }
        }
    }
}
