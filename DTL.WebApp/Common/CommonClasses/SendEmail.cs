using DTL.Business.Common;
using DTL.Business.Dapper;
using DTL.Business.Logging;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DTL.WebApp.Common.CommonClasses
{
    public class SendEmail
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
         
        public static void GenerateCreedential(string username,string userID, string password)
        {
            var commonJsonData = ReadJson.LoadJson();
            string user = commonJsonData.EmailConnectionList.FirstOrDefault().EmailAddress;
            string pass = commonJsonData.EmailConnectionList.FirstOrDefault().Password;
            var message = new MimeMessage();
            try
            {
                message.From.Add(new MailboxAddress("DTL Project", user));
                message.To.Add(new MailboxAddress(username, userID));
                message.Subject = "Credential for your account";
                message.Body = new TextPart("plain")
                {
                    Text = string.Format("Hi {0},{1}{2}LoginName: {3}{4}{5}Password: {6}{7}{8}Thanks,{9}", username
                    , Environment.NewLine, Environment.NewLine, userID, Environment.NewLine, Environment.NewLine, password, Environment.NewLine, Environment.NewLine, "")
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(user, pass);
                    client.Send(message);
                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                log.Info("SendEmail GenerateCreedential", ex); 
                throw ex;
            }
        }
        public static void SendCredentialEmail(string fullName, string emailId, string username, string password)
        {
            var commonJsonData = ReadJson.LoadJson();
            string smtpuser = commonJsonData.EmailConnectionList.FirstOrDefault().EmailAddress;
            string smtppass = commonJsonData.EmailConnectionList.FirstOrDefault().Password;
            var message = new MimeMessage();
            try
            {
                message.From.Add(new MailboxAddress("DTL Project", smtpuser));
                message.To.Add(new MailboxAddress(fullName, emailId));
                message.Subject = "Credential for your account";
                message.Body = new TextPart("plain")
                {
                    Text = string.Format("Hi {0},{1}{2}LoginName: {3}{4}{5}Password: {6}{7}{8}Thanks,{9}", fullName
                    , Environment.NewLine, Environment.NewLine, username, Environment.NewLine, Environment.NewLine, password, Environment.NewLine, Environment.NewLine, "")
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(smtpuser, smtppass);
                    client.Send(message);
                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                log.Info("SendEmail GenerateCreedential", ex);
                throw ex;
            }
        }
        public static void SendAcknowledgment(string fullName, string emailId, string claimId, string claimType, string action,string remark="")
        {
            string _text = "";

            if (action.ToLower() == "approve" || action.ToLower() == "created") {
                //_text = string.Format("Hi {0},{1}{2}Your {3} claim has been generated. {4}{5}Your claim Id is  {6}{7}{8}Thanks,{9}", fullName
                //   , Environment.NewLine, Environment.NewLine, claimType, Environment.NewLine, Environment.NewLine, claimId, Environment.NewLine, Environment.NewLine, "");

                _text = $"Hi {fullName}, {Environment.NewLine} {Environment.NewLine} Your {claimType} has been generated. {Environment.NewLine} {Environment.NewLine}" +
                $"Your claim Id is '{claimId}' {Environment.NewLine} {Environment.NewLine} Thanks,";
            }
            else { 
                //_text = string.Format("Hi {0},{1}{2}Your {3} claim has been Rejected. {4}{5}Please correct and resubmit it as per below remark. {6}{7}{8}{9}Thanks,{10}", fullName
                //   , Environment.NewLine, Environment.NewLine, claimType, Environment.NewLine, Environment.NewLine, claimId, Environment.NewLine,remark, Environment.NewLine, "");

                _text = $"Hi {fullName}, {Environment.NewLine} {Environment.NewLine} Your {claimType} with Id {claimId} has been rejectd.{Environment.NewLine} {Environment.NewLine}" +
                $"Please correct and resubmit  it as per below  remark  {Environment.NewLine} {Environment.NewLine} '{remark}' {Environment.NewLine} {Environment.NewLine} Thanks,";
            }
            EmailAction(fullName, emailId, "Your claim generated successfully", _text);
        }

        public static void EmailAction(string fullName, string emailId,string subject, string text)        
        {
            var commonJsonData = ReadJson.LoadJson();
            string smtpuser = commonJsonData.EmailConnectionList.FirstOrDefault().EmailAddress;
            string smtppass = commonJsonData.EmailConnectionList.FirstOrDefault().Password;
            var message = new MimeMessage();

            try
            {
                message.From.Add(new MailboxAddress("DTL Project", smtpuser));
                message.To.Add(new MailboxAddress(fullName, emailId));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = text
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(smtpuser, smtppass);
                    client.Send(message);
                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                log.Info("SendEmail GenerateCreedential", ex);
                throw ex;
            }
        }

    }
}
