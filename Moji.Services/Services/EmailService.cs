using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moji.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Moji.Services.Services
{
    public class EmailService : IEmailService
    {
      
            private readonly IConfiguration _configuration;
            private readonly ILogger<EmailService> _logger;

            public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
            {
                _configuration = configuration;
                _logger = logger;
            }

            public async Task<bool> SendVerificationEmailAsync(string email, string username, string verificationCode)
            {
                try
                {
                    var smtpSettings = _configuration.GetSection("EmailSettings");
                    var fromEmail = smtpSettings["FromEmail"];
                    var fromName = smtpSettings["FromName"];
                    var subject = "Verify Your Email Address - Moji";

                    var body = $@"
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                            .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; }}
                            .content {{ padding: 20px; background-color: #f9f9f9; }}
                            .code {{ font-size: 32px; font-weight: bold; text-align: center; padding: 20px; background-color: #e8f5e9; border-radius: 5px; margin: 20px 0; letter-spacing: 5px; }}
                            .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
                            button {{ background-color: #4CAF50; color: white; padding: 12px 24px; border: none; border-radius: 4px; cursor: pointer; font-size: 16px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>Welcome to Moji!</h1>
                            </div>
                            <div class='content'>
                                <h2>Hello {username},</h2>
                                <p>Thank you for registering with Moji. Please use the verification code below to complete your registration:</p>
                                <div class='code'>{verificationCode}</div>
                                <p>This code will expire in <strong>10 minutes</strong>.</p>
                                <p>If you didn't request this verification, please ignore this email.</p>
                                <p>For security reasons, never share this code with anyone.</p>
                            </div>
                            <div class='footer'>
                                <p>© 2024 Moji. All rights reserved.</p>
                                <p>This is an automated message, please do not reply to this email.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                    return await SendEmailAsync(email, fromName, fromEmail, subject, body);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send verification email to {Email}", email);
                    return false;
                }
            }

            public async Task<bool> SendWelcomeEmailAsync(string email, string username)
            {
                try
                {
                    var smtpSettings = _configuration.GetSection("EmailSettings");
                    var fromEmail = smtpSettings["FromEmail"];
                    var fromName = smtpSettings["FromName"];
                    var subject = "Welcome to Moji!";

                    var body = $@"
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                            .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; }}
                            .content {{ padding: 20px; background-color: #f9f9f9; }}
                            .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>Welcome to Moji, {username}!</h1>
                            </div>
                            <div class='content'>
                                <h2>Your account has been successfully verified!</h2>
                                <p>We're excited to have you on board. Here's what you can do now:</p>
                                <ul>
                                    <li>Complete your profile</li>
                                    <li>Explore our features</li>
                                    <li>Connect with others</li>
                                </ul>
                                <p>If you have any questions, feel free to contact our support team.</p>
                                <p>Best regards,<br>The Moji Team</p>
                            </div>
                            <div class='footer'>
                                <p>© 2024 Moji. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                    return await SendEmailAsync(email, fromName, fromEmail, subject, body);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send welcome email to {Email}", email);
                    return false;
                }
            }

            public async Task<bool> SendVerificationCodeEmailAsync(string email, string verificationCode)
            {
                try
                {
                    var smtpSettings = _configuration.GetSection("EmailSettings");
                    var fromEmail = smtpSettings["FromEmail"];
                    var fromName = smtpSettings["FromName"];
                    var subject = "Your Verification Code - Moji";

                    var body = $@"
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                            .header {{ background-color: #2196F3; color: white; padding: 20px; text-align: center; }}
                            .content {{ padding: 20px; background-color: #f9f9f9; }}
                            .code {{ font-size: 32px; font-weight: bold; text-align: center; padding: 20px; background-color: #e3f2fd; border-radius: 5px; margin: 20px 0; letter-spacing: 5px; }}
                            .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>Email Verification Code</h1>
                            </div>
                            <div class='content'>
                                <p>Your verification code is:</p>
                                <div class='code'>{verificationCode}</div>
                                <p>This code will expire in <strong>10 minutes</strong>.</p>
                                <p>If you didn't request this code, please ignore this email.</p>
                            </div>
                            <div class='footer'>
                                <p>© 2024 Moji. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                    return await SendEmailAsync(email, fromName, fromEmail, subject, body);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send verification code email to {Email}", email);
                    return false;
                }
            }

            private async Task<bool> SendEmailAsync(string toEmail, string fromName, string fromEmail, string subject, string body)
            {
                try
                {
                    var smtpSettings = _configuration.GetSection("EmailSettings");
                    var smtpServer = smtpSettings["SmtpServer"];
                    var smtpPort = int.Parse(smtpSettings["SmtpPort"] ?? "587");
                    var smtpUsername = smtpSettings["SmtpUsername"];
                    var smtpPassword = smtpSettings["SmtpPassword"];
                    var enableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true");

                    using var client = new SmtpClient(smtpServer, smtpPort);
                    client.EnableSsl = enableSsl;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.Timeout = 100000; // 10 seconds timeout

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail, fromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email sent successfully to {ToEmail}", toEmail);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email to {ToEmail}", toEmail);
                    return false;
                }
            }
       
    }
}
