using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 接收訊息
    /// </summary>
    internal abstract class ResponseBase
    {
        const int SetBytesLength = 80;

        const int ContentBytesLength = 160;

        public const int BytesLength = 4 + SetBytesLength + ContentBytesLength;

        /// <summary>
        /// 以 byte 陣列產生接收訊息
        /// </summary>
        /// <param name="bytes">byte 陣列</param>
        protected ResponseBase(
            byte[] bytes)
        {
            if(bytes.Length < BytesLength)
            {
                throw new ArgumentOutOfRangeException(
                    "bytes",
                    $"byte 陣列長度不得小於 {BytesLength}");
            }

            this.CodeKey = bytes[0];

            this.Coding = ResponseCoding.GetByKey(bytes[1]);

            int setLength = Convert.ToInt32(bytes[2]);

            this.ContentLength = Convert.ToInt32(bytes[3]);

            var set = new List<string>();

            using (var ms = new System.IO.MemoryStream())
            {
                for (var i = 4; i < 4 + setLength; i++)
                {
                    if(bytes[i] == 0)
                    {
                        set.Add(
                            this.Coding.Encoding.GetString(ms.ToArray())
                            );

                        ms.SetLength(0);
                    }
                    else
                    {
                        ms.WriteByte(bytes[i]);
                    }
                }
            }

            this.Set = set;

            this.ContentBytes = new byte[ContentBytesLength];

            Array.Copy(bytes, 4 + SetBytesLength, this.ContentBytes, 0, this.ContentLength);

            this.ParseGlobalError();
        }

        public byte CodeKey
        {
            get;
            private set;
        }

        public bool IsError
        {
            get;
            private set;
        }

        public string ErrorReason
        {
            get;
            private set;
        }

        public string GetErrorContent()
        {
            if (!this.IsError)
            {
                throw new InvalidOperationException("回傳非錯誤");
            }

            return GetContentBase();
        }

        protected string GetContentBase()
        {
            return this.Coding.Encoding.GetString(
                            this.ContentBytes,
                            0,
                            this.ContentLength);
        }

        protected ResponseCoding Coding
        {
            get;
            private set;
        }

        protected IEnumerable<string> Set
        {
            get;
            private set;
        }

        protected int ContentLength
        {
            get;
            private set;
        }

        protected byte[] ContentBytes
        {
            get;
            private set;
        }

        void ParseGlobalError()
        {
            switch(this.CodeKey)
            {
                case 30:
                    {
                        this.ErrorReason = "傳送的訊息長度有誤";
                        break;
                    }
                case 31:
                    {
                        this.ErrorReason = "網路傳輸發生錯誤";
                        break;
                    }
                case 32:
                    {
                        this.ErrorReason = "訊息種類無法辨識";
                        break;
                    }
                case 40:
                    {
                        this.ErrorReason = "系統內部錯誤(資料庫錯誤)";
                        break;
                    }
                case 41:
                    {
                        this.ErrorReason = "系統內部錯誤";
                        break;
                    }
                case 50:
                    {
                        this.ErrorReason = "尚未經過密碼檢查就發送";
                        break;
                    }
                case 51:
                    {
                        this.ErrorReason = "已通過密碼檢查，又送來帳號密碼檢查";
                        break;
                    }
                case 52:
                    {
                        this.ErrorReason = "文字簡訊傳送未申請";
                        break;
                    }
                case 53:
                    {
                        this.ErrorReason = "接收文字簡訊未申請";
                        break;
                    }
                case 58:
                    {
                        this.ErrorReason = "國際簡訊未申請";
                        break;
                    }
                default:
                    {
                        var reason = this.ParseError();
                        if (string.IsNullOrWhiteSpace(reason))
                        {
                            return;
                        }
                        else
                        {
                            this.ErrorReason = reason;
                            break;
                        }
                    }
            }

            this.IsError = true;
        }

        /// <summary>
        /// 解析錯誤。只需解析自身特定錯誤。
        /// </summary>
        /// <returns>若為錯誤，傳非 null</returns>
        protected virtual string ParseError()
        {
            return null;
        }
    }
}
