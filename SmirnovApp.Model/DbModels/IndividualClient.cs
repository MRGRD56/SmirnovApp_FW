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
        
        public override void CopyPropertiesFrom(Client source)
        {
            LastName = source.LastName;
            FirstName = source.FirstName;
            Patronymic = source.Patronymic;
            BirthDate = source.BirthDate;
            PassportSeries = source.PassportSeries;
            PassportNumber = source.PassportNumber;
            PassportIssuedBy = source.PassportIssuedBy;
            PassportIssueDate = source.PassportIssueDate;
            LivingAddress = source.LivingAddress;
            RegistrationAddress = source.RegistrationAddress;
            ApplicationDate = source.ApplicationDate;
        }
    }
}
