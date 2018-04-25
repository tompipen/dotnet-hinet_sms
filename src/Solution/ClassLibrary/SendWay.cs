using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 傳送形式
    /// </summary>
    public class SendWay
    {
        private SendWay(
            string key,
            string description)
        {
            this.Key = key;

            this.Description = description;
        }

        public string Key { get; private set; }

        public string Description { get; private set; }

        /// <summary>
        /// 01: 立即傳送，若為WAP PUSH 則為Service Indication
        /// </summary>
        public static readonly SendWay Immediate = new SendWay(
            "01", "立即傳送，若為WAP PUSH 則為Service Indication"
            );

        /// <summary>
        /// 02: 立即傳送加重送截止時間，若為WAP PUSH 則為Service Loading
        /// </summary>
        public static readonly SendWay ImmediateAndDeadline = new SendWay(
            "02", "立即傳送加重送截止時間，若為WAP PUSH 則為Service Loading"
            );

        /// <summary>
        /// 03: 預約傳送
        /// </summary>
        public static readonly SendWay Scheduled = new SendWay(
            "03", "預約傳送"
            );

        /// <summary>
        /// 04: 預約傳送加重送截止時間
        /// </summary>
        public static readonly SendWay ScheduledAndDeadline = new SendWay(
            "04", "預約傳送加重送截止時間"
            );

        public static readonly System.Collections.ObjectModel.ReadOnlyCollection<SendWay> All =
            new System.Collections.ObjectModel.ReadOnlyCollection<SendWay>(
                new SendWay[]
                {
                    Immediate,
                    ImmediateAndDeadline,
                    Scheduled,
                    ScheduledAndDeadline,
                });

        static readonly System.Collections.Generic.Dictionary<string, SendWay> m_Dictionary =
            new System.Collections.Generic.Dictionary<string, SendWay>();

        static System.Collections.Generic.Dictionary<string, SendWay> Dictionary
        {
            get
            {
                return m_Dictionary;
            }
        }

        static SendWay()
        {
            foreach (SendWay obj in All)
            {
                Dictionary.Add(
                    obj.Key,
                    obj);
            }
        }

        public static SendWay GetByKey(string key)
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

        public static System.Collections.ObjectModel.ReadOnlyCollection<SendWay> GetAll()
        {
            return All;
        }


    }
}
