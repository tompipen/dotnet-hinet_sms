using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 訊息型態
    /// </summary>
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

        /// <summary>
        /// 0: 帳號密碼檢查
        /// </summary>
        public static readonly RequestType
            Authenticate = new RequestType(0, "帳號密碼檢查");

        /// <summary>
        /// 1: 傳送文字簡訊
        /// </summary>
        public static readonly RequestType
            SendTextMessage = new RequestType(1, "傳送文字簡訊");

        /// <summary>
        /// 2: 查詢文字簡訊傳送結果
        /// </summary>
        public static readonly RequestType
            QueryTextMessageResult = new RequestType(2, "查詢文字簡訊傳送結果");

        /// <summary>
        /// 3: 接收文字簡訊 (一般用戶不開放)
        /// </summary>
        public static readonly RequestType
            ReceiveTextMessage = new RequestType(3, "接收文字簡訊 (一般用戶不開放)");

        /// <summary>
        /// 13: 傳送WAP PUSH
        /// </summary>
        public static readonly RequestType
            SendWapPush = new RequestType(13, "傳送WAP PUSH");

        /// <summary>
        /// 14: 查詢WAP PUSH 傳送結果
        /// </summary>
        public static readonly RequestType
            QueryWapPushResult = new RequestType(14, "查詢WAP PUSH 傳送結果");

        /// <summary>
        /// 15: 傳送國際簡訊(查詢國際簡訊傳送結果請用 type=2 即可)
        /// </summary>
        public static readonly RequestType
            SendInternationalMessage = new RequestType(15, "傳送國際簡訊(查詢國際簡訊傳送結果請用 type=2 即可)");

        /// <summary>
        /// 16: 取消預約文字簡訊
        /// </summary>
        public static readonly RequestType
            CancelAppointmentTextMessage = new RequestType(16, "取消預約文字簡訊");

        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<RequestType> All =
            new System.Collections.ObjectModel.ReadOnlyCollection<RequestType>(
                new RequestType[]
                {
                    Authenticate,
                    SendTextMessage,
                    QueryTextMessageResult,
                    ReceiveTextMessage,
                    SendWapPush,
                    QueryWapPushResult,
                    SendInternationalMessage,
                    CancelAppointmentTextMessage,
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
