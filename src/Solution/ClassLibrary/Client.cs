using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    public class Client : IDisposable
    {
        public Client(
            string userId,
            string password,
            string hostName = "api.hiair.hinet.net",
            int port = 8000
            )
        {
            this.HostName = hostName;

            this.Port = port;

            this.UserId = userId;

            this.Password = password;

            this.TcpClient = new System.Net.Sockets.TcpClient(
                this.HostName,
                this.Port);

            this.NetworkStream = this.TcpClient.GetStream();
        }

        public string HostName { get; private set; }

        public int Port { get; private set; }

        public string UserId { get; private set; }

        public string Password { get; private set; }

        System.Net.Sockets.TcpClient TcpClient
        {
            get;
            set;
        }

        System.Net.Sockets.NetworkStream NetworkStream
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.NetworkStream != null)
            {
                this.NetworkStream.Close();

                this.NetworkStream.Dispose();

                this.NetworkStream = null;
            }

            if (this.TcpClient != null)
            {
                this.TcpClient.Close();

                this.TcpClient.Dispose();

                this.TcpClient = null;
            }
        }

        T SendRequest<T>(
            RequestBase request,
            Func<byte[], T> makeResponseFunc
            ) where T : ResponseBase
        {
            var requestBytes = request.GetBytes();

            var responseBytes = new byte[ResponseBase.BytesLength];


            this.NetworkStream.Write(requestBytes, 0, requestBytes.Length);

            var readCount = 0;

            while (readCount < responseBytes.Length)
            {
                var rc = this.NetworkStream.Read(responseBytes, readCount, responseBytes.Length - readCount);

                if (rc == 0)
                {
                    break;
                }

                readCount += rc;
            }

            var response = makeResponseFunc(responseBytes);

            if (response.IsError)
            {
                throw new ApplicationException(
                    $"回傳錯誤：{response.CodeKey} 原因：{response.ErrorReason} 內容：{response.GetErrorContent()}"
                    );
            }

            return response;
        }

        /// <summary>
        /// 帳號密碼檢查
        /// </summary>
        public void Authenticate()
        {
            var request = new AuthenticateRequest(
                this.UserId,
                this.Password);

            var response = this.SendRequest<AuthenticateResponse>(
                request,
                b => new AuthenticateResponse(b)
                );
        }

        /// <summary>
        /// 傳送文字簡訊。立即傳送。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <returns>Message ID (用於查詢傳送結果)</returns>
        public string SendTextMessage(
            string phoneNumber,
            string content
            )
        {
            var request = new SendTextMessageRequest(
                phoneNumber,
                content);

            var response = this.SendRequest<SendMessageResponse>(
                request,
                b => new SendMessageResponse(b)
                );

            return response.GetMessageID();
        }

        /// <summary>
        /// 傳送文字簡訊。立即傳送加重送截止時間。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="deadline">重送截止時間</param>
        /// <returns>Message ID (用於查詢傳送結果)</returns>
        public string SendTextMessage(
            string phoneNumber,
            string content,
            TimeSpan deadline
            )
        {
            var request = new SendTextMessageRequest(
                phoneNumber,
                content,
                deadline);

            var response = this.SendRequest<SendMessageResponse>(
                request,
                b => new SendMessageResponse(b)
                );

            return response.GetMessageID();
        }

        /// <summary>
        /// 傳送文字簡訊。預約傳送。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="scheduled">預約時間</param>
        /// <returns>Message ID (用於查詢傳送結果)</returns>
        public string SendTextMessage(
            string phoneNumber,
            string content,
            DateTime scheduled
            )
        {
            var request = new SendTextMessageRequest(
                phoneNumber,
                content,
                scheduled);

            var response = this.SendRequest<SendMessageResponse>(
                request,
                b => new SendMessageResponse(b)
                );

            return response.GetMessageID();
        }

        /// <summary>
        /// 傳送文字簡訊。預約傳送加重送截止時間。
        /// </summary>
        /// <param name="phoneNumber">接收門號</param>
        /// <param name="content">簡訊內容</param>
        /// <param name="scheduled">預約時間</param>
        /// <param name="deadline">重送截止時間</param>
        public string SendTextMessage(
            string phoneNumber,
            string content,
            DateTime scheduled,
            TimeSpan deadline
            )
        {
            var request = new SendTextMessageRequest(
                phoneNumber,
                content,
                scheduled,
                deadline);

            var response = this.SendRequest<SendMessageResponse>(
                request,
                b => new SendMessageResponse(b)
                );

            return response.GetMessageID();
        }

    }
}
