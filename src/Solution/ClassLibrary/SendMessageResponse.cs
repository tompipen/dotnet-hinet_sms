using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    internal class SendMessageResponse:ResponseBase
    {
        public SendMessageResponse(
            byte[] bytes) : base(bytes)
        {
        }

        protected override string ParseError()
        {
            switch (this.CodeKey)
            {
                case 0:
                    {
                        return null;
                    }
                case 1:
                    {
                        return "國別格式錯誤";
                    }
                case 2:
                    {
                        return "編碼格式錯誤";
                    }
                case 3:
                    {
                        return "優先權格式錯誤";
                    }
                case 4:
                    {
                        return "msg_content_len 格式錯誤";
                    }
                case 5:
                    {
                        return "msg_content_len 與 msg_content 的長度不相符";
                    }
                case 6:
                    {
                        return "接收手機號碼格式錯誤";
                    }
                case 7:
                    {
                        return "傳送型式的格式錯誤";
                    }
                case 8:
                    {
                        return "限時傳送格式錯誤";
                    }
                case 9:
                    {
                        return "預約傳送格式錯誤";
                    }
                case 10:
                    {
                        return "目前暫不開放傳送至國外";
                    }
                case 11:
                    {
                        return "系統暫時無法傳送訊息";
                    }
                case 12:
                    {
                        return "長簡訊訊息次序錯誤";
                    }
                case 13:
                    {
                        return "wap push 的 url 沒有設定";
                    }
                case 14:
                    {
                        return "wap push 的訊息內容超過 88 個 byte";
                    }
                case 15:
                    {
                        return "加值出帳HN 格式錯誤";
                    }
                case 16:
                    {
                        return "簡訊內容包含連續 9-10 碼的電話號碼，不合規定。";
                    }
                default:
                    {
                        return $"未預期的 Code {this.CodeKey:0}";
                    }
            }
        }

        public string GetMessageID()
        {
            if (this.IsError)
            {
                throw new InvalidOperationException("回傳為錯誤");
            }

            return GetContentBase();
        }
    }
}
