using System;

namespace DonVo.SignalR_MediatR.Core.Models.Entities
{
    /// <summary>
    /// Value object
    /// </summary>
    public class Grade
    {
        public string Value { get; }
        public DateTime DateSet { get; }
        public string Note { get; }
        public Grade(string value, DateTime dateSet, string note)
        {
            Value = value;
            DateSet = dateSet;
            Note = note;
        }

        /// <summary>
        /// For EF
        /// </summary>
        private Grade() { }
    }
}