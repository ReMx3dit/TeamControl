using System;
using System.Collections.Generic;
using System.Text;

namespace TeamControlLib.Model
{
    public class Transfer
    {
        public int Id { get; set; }
        public int SpelerId { get; set; }
        public int OudTeamId { get; set; }
        public int NieuwTeamId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Transfer transfer &&
                   SpelerId == transfer.SpelerId &&
                   OudTeamId == transfer.OudTeamId &&
                   NieuwTeamId == transfer.NieuwTeamId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SpelerId, OudTeamId, NieuwTeamId);
        }

        public override string ToString()
        {
            return $"Transfer: {Id}, {SpelerId}, {OudTeamId}, {NieuwTeamId}";
        }
    }
}
