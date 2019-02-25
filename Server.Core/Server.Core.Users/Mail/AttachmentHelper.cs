﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Server.Users.Mail
{
    /// <summary>
    /// Внутренний класс для создания вложений в email.
    /// Взято с http://social.msdn.microsoft.com/Forums/en-US/b6c764f7-4697-4394-b45f-128a24306d55/40-smtpclientsend-attachments-mit-umlauten-im-dateinamen
    /// </summary>
    internal class AttachmentHelper
    {
        public static Attachment CreateAttachment(Stream attachmentFile, string displayName,
            TransferEncoding transferEncoding)
        {
            Attachment attachment = new Attachment(attachmentFile, MediaTypeNames.Application.Octet);
            attachment.TransferEncoding = transferEncoding;

            string tranferEncodingMarker = String.Empty;
            string encodingMarker = String.Empty;
            int maxChunkLength = 0;

            switch (transferEncoding)
            {
                case TransferEncoding.Base64:
                    tranferEncodingMarker = "B";
                    encodingMarker = "UTF-8";
                    maxChunkLength = 30;
                    break;
                case TransferEncoding.QuotedPrintable:
                    tranferEncodingMarker = "Q";
                    encodingMarker = "ISO-8859-1";
                    maxChunkLength = 76;
                    break;
                default:
                    throw (new ArgumentException(String.Format("The specified TransferEncoding is not supported: {0}",
                        transferEncoding), "transferEncoding"));
            }

            attachment.NameEncoding = Encoding.GetEncoding(encodingMarker);

            string encodingtoken = String.Format("=?{0}?{1}?", encodingMarker, tranferEncodingMarker);
            string softbreak = "?=";
            string encodedAttachmentName = encodingtoken;

            
            encodedAttachmentName = Convert.ToBase64String(Encoding.UTF8.GetBytes(displayName));
            
            encodedAttachmentName = SplitEncodedAttachmentName(encodingtoken, softbreak, maxChunkLength,
                encodedAttachmentName);
            attachment.Name = encodedAttachmentName;

            return attachment;
        }

        private static string SplitEncodedAttachmentName(string encodingtoken, string softbreak, int maxChunkLength,
            string encoded)
        {
            int splitLength = maxChunkLength - encodingtoken.Length - (softbreak.Length * 2);
            var parts = SplitByLength(encoded, splitLength);

            string encodedAttachmentName = encodingtoken;

            foreach (var part in parts)
            {
                encodedAttachmentName += part + softbreak + encodingtoken;
            }

            encodedAttachmentName = encodedAttachmentName.Remove(encodedAttachmentName.Length - encodingtoken.Length,
                encodingtoken.Length);
            return encodedAttachmentName;
        }

        private static IEnumerable<string> SplitByLength(string stringToSplit, int length)
        {
            while (stringToSplit.Length > length)
            {
                yield return stringToSplit.Substring(0, length);
                stringToSplit = stringToSplit.Substring(length);
            }

            if (stringToSplit.Length > 0)
            {
                yield return stringToSplit;
            }
        }
    }

}
