using System;
using odec.Framework.Generic;
using odec.Framework.Generic.Utility;

namespace odec.Server.Model.Conversation.Filter
{
    public class ConversationFilter<TKey>: FilterBase
    {
        public ConversationFilter()
        {
            Sord = "desc";
            Sidx = "DateCreated";
            Rows = 20;
        }
        public string Title { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// Диапазон в который попадает дата начала.
        /// </summary>
        public Interval<DateTime?> DateCreatedInterval { get; set; }
        
        // public Interval<decimal?> CurrentPrice { get; set; }
        public TKey UserId { get; set; }
        public TKey ConversationTypeId { get; set; }
    }
}
