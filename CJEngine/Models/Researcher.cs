using System.Collections.Generic;
using CJEngine.Models.Join_Entities;
using Microsoft.AspNetCore.Identity;

namespace CJEngine.Models
{
    public class Researcher : IdentityUser<int>
    {
        //public int Id { get; set; }
        public string Name { get; set; }

        public IList<ExpResearcher> ExpResearchers { get; set; }
    }
}
