using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Статус договора.
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// Услуга ещё не была оказана.
        /// </summary>
        NotPerformed,
        /// <summary>
        /// Услуга по договору оказана.
        /// </summary>
        ServiceProvided,
        /// <summary>
        /// Договор расторгнут.
        /// </summary>
        Terminated
    }
}
