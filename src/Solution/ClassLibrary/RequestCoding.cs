using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 傳送訊息編碼種類
    /// </summary>
    public class RequestCoding
    {
        private RequestCoding(
            byte key,
            string description,
            System.Text.Encoding encoding)
        {
            this.Key = key;

            this.Description = description;

            this.Encoding = encoding;
        }

        public byte Key { get; private set; }

        public string Description { get; private set; }

        public System.Text.Encoding Encoding { get; private set; }

        /// <summary>
        /// Big5
        /// </summary>
        public static readonly RequestCoding Big5 = new RequestCoding(
            1, "Big5",
            System.Text.Encoding.GetEncoding(950));


        /// <summary>
        /// Binary
        /// </summary>
        public static readonly RequestCoding Binary = new RequestCoding(
            2, "Binary",
            System.Text.Encoding.ASCII);

        /// <summary>
        /// Unicode(UCS-2)
        /// </summary>
        public static readonly RequestCoding UCS2 = new RequestCoding(
            3, "Unicode(UCS-2)",
            System.Text.Encoding.BigEndianUnicode);


        /// <summary>
        /// Unicode(UTF-8)
        /// </summary>
        public static readonly RequestCoding UTF8 = new RequestCoding(
            4, "Unicode(UTF-8)",
            System.Text.Encoding.UTF8);


        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<RequestCoding> All =
            new System.Collections.ObjectModel.ReadOnlyCollection<RequestCoding>(
                new RequestCoding[]
                {
                    Big5,
                    Binary,
                    UCS2,
                    UTF8,
                });

        static readonly System.Collections.Generic.Dictionary<byte, RequestCoding> m_Dictionary =
            new System.Collections.Generic.Dictionary<byte, RequestCoding>();

        static System.Collections.Generic.Dictionary<byte, RequestCoding> Dictionary
        {
            get
            {
                return m_Dictionary;
            }
        }

        static RequestCoding()
        {
            foreach (RequestCoding obj in All)
            {
                Dictionary.Add(
                    obj.Key,
                    obj);
            }
        }

        public static RequestCoding GetByKey(byte key)
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

        public static System.Collections.ObjectModel.ReadOnlyCollection<RequestCoding> GetAll()
        {
            return All;
        }


    }
}
