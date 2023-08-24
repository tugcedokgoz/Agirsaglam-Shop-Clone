using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AgirSaglam.Model.Models.Entity;

namespace AgirSaglam.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(RepositoryContext context) : base(context)
        {

        }
        //tüm kullanıcıları listeleme
        public async Task<(Role Role, Adress Adress)> GetUserRoleAndAdressById(int userId)
        {
            var user = await RepositoryContext.Users
                .Include(u => u.Role) // Role nesnesini Include ederek ilişkili veriyi getirin
                .Include(u => u.Adress) // Adress nesnesini Include ederek ilişkili veriyi getirin
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return (null, null);

            return (user.Role, user.Adress);
        }

        //silme
        public void RemoveUser(int userId)
        {
            RepositoryContext.Users.Where(r => r.Id == userId).ExecuteDelete();
        }
        //userId göre role listeleme
        public async Task<Role> GetUserRoleById(int userId)
        {
            var user = await RepositoryContext.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var role = await RepositoryContext.Roles
                .Where(r => r.Id == user.RoleId)
                .FirstOrDefaultAsync();

            return role;
        }

        //userId göre adress getirme
        public async Task<Adress> GetUserAdressById(int userId)
        {
            var user = await RepositoryContext.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var adress = await RepositoryContext.Adresses
                .Where(r => r.Id == user.AdressId)
                .FirstOrDefaultAsync();

            return adress;
        }

        //userId göre listeleme
        public async Task<User> GetUserById(int userId)
        {
            var user = await RepositoryContext.Users
                .Include(u => u.Role) // Role nesnesini Include ederek ilişkili veriyi getirin
                .Include(u => u.Adress) // Adress nesnesini Include ederek ilişkili veriyi getirin
                //.Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        //Aktif kullaniciları getirme
        public List<V_AktiveUsers> GetAktiveUsers()
        {
            return RepositoryContext.AktiveUsers.ToList<V_AktiveUsers>();
        }

        public List<User> GetUsersByName(string userName)
        {
            var users = RepositoryContext.Users
                .Where(r => r.UserName.Contains(userName))
                .ToList();

            return users;
        }

        public async Task<User> GetUserAdminList()
        {
            var user = await RepositoryContext.Users
                .Include(u => u.Role) 
                .Include(u => u.Adress)
                                       
                .FirstOrDefaultAsync();

            return user;
        }


        public async Task<int?> GetUserIdByEmailAsync(string email)
        {
            var user = await RepositoryContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user?.Id; 
        }

        public User GetById(string userName)
        {
            return RepositoryContext.Users
                .Include(u => u.Role)
                .Include(u => u.Adress)
                .FirstOrDefault(u => u.UserName == userName);
        }
        public async Task<EmailResponse> UpdatePasswordByEmail(EmailRequest emailRequest)
        {

            var user = await RepositoryContext.Users.FirstOrDefaultAsync(x => x.Email == emailRequest.Email);

            if (user == null)
            {
                return new EmailResponse()
                {
                    Control = false,
             
                };
            }
            else
            {
                string newPassword = CreatingPassword(9);
                user.Password = newPassword;

                RepositoryContext.Update(user); // Kullanıcı bilgisini güncelle
                await RepositoryContext.SaveChangesAsync(); // Değişiklikleri kaydet

                MailSender("ahlproje@outlook.com", "Td_goz854", emailRequest.Email, newPassword);

                return new EmailResponse();
            }



        }

        public void MailSender(string senderMail, string senderPassword, string email, string newPassword)
        {
            string subject = "Important Information";

            string htmlBody = @"
                    <html>
                    <head>
                        <style>
                            body {
                                background-repeat: no-repeat;
                                background-size: cover;
                            }
                        </style>
                    </head>
                    <body>
                        <h1>Hello,</h1>
                        <p>We hope this message finds you well.</p>
                        <p>We would like to inform you about an important matter:</p>
                        <p>Your password for our service has been updated.</p>
                        <p>If you have not requested this change, please <a href=""https://www.yourservice.com/contact"">contact us</a> immediately.</p>
                        <p>Thank you for being a valued member of our community.</p>
                        <p>Sincerely,</p>
                        <p>Your Service Team</p>
                    </body>
                    </html>";







            SmtpClient smtpClient = new SmtpClient("smtp.yoursmtpserver.com");

            smtpClient.Port = 587;

            smtpClient.EnableSsl = true;

            smtpClient.Host = "smtp.outlook.com";

            smtpClient.Credentials = new NetworkCredential(senderMail, senderPassword);

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(senderMail);

            mailMessage.To.Add(email);

            mailMessage.Subject = subject;

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = htmlBody + $":This is your Temporal Password : {newPassword}. {DateTime.Now.ToString()}"; ;

            try
            {
                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            mailMessage.Body = htmlBody + $": This is your Temporary Password: {newPassword}. {DateTime.Now.ToString()}"; ;
        }
        //random şifre
        public string CreatingPassword(int length)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";


            Random random = new Random();

            char[] password = new char[length];


            for (int i = 0; i < length; i++)
            {

                password[i] = allowedChars[random.Next(0, allowedChars.Length)];

            }

            return new string(password);


        }
    }
}
