using Domain;
using System.Collections.Generic;

namespace Application.Commands.GetBanks
{
    public class GetBanksResponse
    {
        public List<Bank> Banks { get; set; }
    }
}
