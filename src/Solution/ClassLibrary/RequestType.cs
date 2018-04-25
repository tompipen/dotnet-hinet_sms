using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    public class RequestType
    {
        private RequestType(
            byte key,
            string description
            )
        {

            this.Key = key;

            this.Description = description;
        }

        public byte Key { get; private set; }

        public string Description { get; private set; }

        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<RequestType> All =
            new System.Collections.ObjectModel.ReadOnlyCollection<RequestType>(
                new RequestType[]
                {
                    new RequestType(0, "帳號密碼檢查"),
                    new RequestType(1, "傳送文字簡訊"),
                    new RequestType(2, "查詢文字簡訊傳送結果"),
                    new RequestType(3, "接收文字簡訊 (一般用戶不開放)"),
                    new RequestType(13, "傳送WAP PUSH"),
                    new RequestType(14, "查詢WAP PUSH 傳送結果"),
                    new RequestType(15, "傳送國際簡訊(查詢國際簡訊傳送結果請用 type=2 即可)"),
                    new RequestType(16, "取消預約文字簡訊"),
                });

        static readonly System.Collections.Generic.Dictionary<byte, RequestType> m_Dictionary =
            new System.Collections.Generic.Dictionary<byte, RequestType>();

        static System.Collections.Generic.Dictionary<byte, RequestType> Dictionary
        {
            get
            {
                return m_Dictionary;
            }
        }

        static RequestType()
        {
            foreach (RequestType obj in All)
            {
                Dictionary.Add(
                    obj.Key,
                    obj);
            }
        }

        public static RequestType GetByKey(byte key)
        {
            try
            {
                return Dictionary[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static System.Collections.ObjectModel.ReadOnlyCollection<RequestType> GetAll()
        {
            return All;
        }

    }
}
