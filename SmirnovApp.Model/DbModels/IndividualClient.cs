using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Клиент-физическое лицо.
    /// </summary>
    [Table("IndividualClients")]
    public class IndividualClient : Client
    {
        public override object Clone()
        {
            return new IndividualClient
            {
                Id = Id,
                LastName = LastName,
                FirstName = FirstName,
                Patronymic = Patronymic,
                BirthDate = BirthDate,
                PassportSeries = PassportSeries,
                PassportNumber = PassportNumber,
                PassportIssuedBy = PassportIssuedBy,
                PassportIssueDate = PassportIssueDate,
                LivingAddress = LivingAddress,
                RegistrationAddress = RegistrationAddress,
                ApplicationDate = ApplicationDate
            };
        }
        
        public override void CopyPropertiesFrom(Client anotherClient)
        {
            LastName = anotherClient.LastName;
            FirstName = anotherClient.FirstName;
            Patronymic = anotherClient.Patronymic;
            BirthDate = anotherClient.BirthDate;
            PassportSeries = anotherClient.PassportSeries;
            PassportNumber = anotherClient.PassportNumber;
            PassportIssuedBy = anotherClient.PassportIssuedBy;
            PassportIssueDate = anotherClient.PassportIssueDate;
            LivingAddress = anotherClient.LivingAddress;
            RegistrationAddress = anotherClient.RegistrationAddress;
            ApplicationDate = anotherClient.ApplicationDate;
        }
    }
}
