using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 傳送文字簡訊
    /// </summary>
    internal class SendTextMessageRequest:RequestBase
    {
        
        /// <summary>
        /// 傳送文字簡訊。立即傳送。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        public SendTextMessageRequest(
            string phoneNumber,
            string content
            )
        {
            this.Init(
                phoneNumber,
                content,
                SendWay.Immediate,
                new string[0]);
        }

        /// <summary>
        /// 傳送文字簡訊。立即傳送加重送截止時間。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="deadline">重送截止時間</param>
        public SendTextMessageRequest(
            string phoneNumber,
            string content,
            TimeSpan deadline
            )
        {
            ValidateDeadline(deadline);

            this.Init(
                phoneNumber,
                content,
                SendWay.ImmediateAndDeadline,
                new string[] { deadline.TotalMinutes.ToString(DeadlineFormat) });
        }

        /// <summary>
        /// 傳送文字簡訊。預約傳送。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="scheduled">預約時間</param>
        public SendTextMessageRequest(
            string phoneNumber,
            string content,
            DateTime scheduled
            )
        {
            ValidateScheduled(scheduled);

            this.Init(
                phoneNumber,
                content,
                SendWay.Scheduled,
                new string[] { scheduled.ToString(ScheduledFormat) });
        }

        /// <summary>
        /// 傳送文字簡訊。預約傳送加重送截止時間。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="scheduled">預約時間</param>
        /// <param name="deadline">重送截止時間</param>
        public SendTextMessageRequest(
            string phoneNumber,
            string content,
            DateTime scheduled,
            TimeSpan deadline
            )
        {
            ValidateScheduled(scheduled);

            ValidateDeadline(deadline);

            this.Init(
                phoneNumber,
                content,
                SendWay.Scheduled,
                new string[]
                {
                    scheduled.ToString(ScheduledFormat),
                    deadline.TotalMinutes.ToString(DeadlineFormat)
                });
        }

        /// <summary>
        /// 傳送文字簡訊。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="sendWay">傳送形式</param>
        void Init(
            string phoneNumber,
            string content,
            SendWay sendWay,
            IEnumerable<string> extraSet
            )
        {
            const int PhoneNumberLength = 10;

            const int ContentMaxLength = 159;

            var phoneNumberBytes = System.Text.Encoding.ASCII.GetBytes(phoneNumber);

            if (phoneNumberBytes.Length != PhoneNumberLength)
            {
                throw new ArgumentOutOfRangeException("phoneNumber", $"接收門號應為十碼");
            }

            this.Coding = RequestCoding.UTF8;

            var contentBytesLength = this.Coding.Encoding.GetBytes(
                content, 0, content.Length,
                this.ContentBytes, 0
                );

            if (contentBytesLength > ContentMaxLength)
            {
                throw new ArgumentOutOfRangeException("content", $"簡訊內容最多 159 位元組");
            }

            this.Type = RequestType.SendTextMessage;

            var baseSet = new List<string>(new[] { phoneNumber, sendWay.Key });

            baseSet.AddRange(extraSet);

            this.Set = baseSet;

            this.ContentLength = Convert.ToByte(contentBytesLength);

            this.ContentBytes[contentBytesLength] = 0;
        }

    }
}
