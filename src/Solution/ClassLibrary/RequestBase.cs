using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 傳送訊息
    /// </summary>
    internal class RequestBase
    {
        protected RequestBase()
        {
            this.Coding = RequestCoding.Big5;

            this.Priority = RequestPriority.P4;

            this.ContentLength = 0;

            this.ContentBytes = new byte[ContentBytesCount];
        }

        public RequestType Type { get; protected set; }

        public RequestCoding Coding { get; protected set; }

        public RequestPriority Priority { get; protected set; }

        public IEnumerable<string> Set { get; protected set; }

        public byte ContentLength { get; protected set; }

        protected byte[] ContentBytes { get; set; }

        public byte[] GetContentBytes()
        {
            var result = new byte[ContentBytesCount];

            Array.Copy(this.ContentBytes, result, ContentBytesCount);

            return result;
        }

        protected const string ScheduledFormat = "yyMMddHHmmss";

        protected const string DeadlineFormat = "0000";

        public static readonly TimeSpan DeadlineMax = TimeSpan.FromMinutes(1440d);

        public static readonly TimeSpan DeadlineMin = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 驗證預約時間
        /// </summary>
        /// <param name="scheduled">預約時間</param>
        protected static void ValidateScheduled(DateTime scheduled)
        {
            if(scheduled < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("scheduled", "預約時間不得早於目前時間");
            }

        }

        /// <summary>
        /// 驗證重送截止時間
        /// </summary>
        /// <param name="deadline">重送截止時間</param>
        protected static void ValidateDeadline(TimeSpan deadline)
        {
            if (deadline < DeadlineMin)
            {
                throw new ArgumentOutOfRangeException("deadline", $"重送截止時間不得小於 {DeadlineMin}");
            }

            if (deadline > DeadlineMax)
            {
                throw new ArgumentOutOfRangeException("deadline", $"重送截止時間不得大於 {DeadlineMax}");
            }
        }

        const int SetBytesCount = 100;

        const int ContentBytesCount = 160;

        byte ReadSetBytes(byte[] result)
        {
            long length = 0L;

            using (var ms = new System.IO.MemoryStream())
            {
                foreach (var item in this.Set)
                {
                    var bs = System.Text.Encoding.ASCII.GetBytes(
                        item);

                    ms.Write(bs, 0, bs.Length);

                    ms.WriteByte(0);
                }

                ms.Seek(0L, System.IO.SeekOrigin.Begin);

                ms.Read(result, 0, result.Length);

                length = ms.Length;
            }

            return Convert.ToByte(length);
        }

        public byte[] GetBytes()
        {
            byte[] result = null;

            using (var ms = new System.IO.MemoryStream())
            {
                ms.WriteByte(this.Type.Key);

                ms.WriteByte(this.Coding.Key);

                ms.WriteByte((byte)this.Priority);

                ms.WriteByte(0);

                var setBytes = new byte[SetBytesCount];

                ms.WriteByte(this.ReadSetBytes(setBytes));

                ms.WriteByte(this.ContentLength);

                ms.Write(setBytes, 0, SetBytesCount);

                ms.Write(this.ContentBytes, 0, ContentBytesCount);

                result = ms.ToArray();
            }

            return result;
        }

        

        
    }
}
