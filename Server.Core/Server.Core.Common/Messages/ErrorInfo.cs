namespace Server.Core.Common.Messages
{
    /// <summary>
    /// Информация об ошибках.
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ErrorInfo(int code, string text)
        {
            Code = code;
            Text = text;
        }

        public ErrorInfo(object code, string text)
        {
            Code = (int)code;
            Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ErrorInfo()
        {
        }

        /// <summary>
        /// Код ошибки.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public string Text { get; set; }
    }
}
