using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 接收訊息編碼種類
    /// </summary>
    public class ResponseCoding
    {
        private ResponseCoding(
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
        public static readonly ResponseCoding Big5 = new ResponseCoding(
            1, "Big5",
            System.Text.Encoding.GetEncoding(950));

        /// <summary>
        /// Binary
        /// </summary>
        public static readonly ResponseCoding Binary = new ResponseCoding(
            2, "Binary",
            System.Text.Encoding.ASCII);

        /// <summary>
        /// Unicode(UCS-2)
        /// </summary>
        public static readonly ResponseCoding UCS2 = new ResponseCoding(
            3, "Unicode(UCS-2)",
            System.Text.Encoding.BigEndianUnicode);



        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<ResponseCoding> All =
            new System.Collections.ObjectModel.ReadOnlyCollection<ResponseCoding>(
                new ResponseCoding[]
                {
                    Big5,
                    Binary,
                    UCS2,
                });

        static readonly System.Collections.Generic.Dictionary<byte, ResponseCoding> m_Dictionary =
            new System.Collections.Generic.Dictionary<byte, ResponseCoding>();

        static System.Collections.Generic.Dictionary<byte, ResponseCoding> Dictionary
        {
            get
            {
                return m_Dictionary;
            }
        }

        static ResponseCoding()
        {
            foreach (ResponseCoding obj in All)
            {
                Dictionary.Add(
                    obj.Key,
                    obj);
            }
        }

        public static ResponseCoding GetByKey(byte key)
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

        public static System.Collections.ObjectModel.ReadOnlyCollection<ResponseCoding> GetAll()
        {
            return All;
        }


    }
}
