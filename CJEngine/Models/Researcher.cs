using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models.Join_Entities;
using Microsoft.AspNetCore.Identity;

namespace CJEngine.Models
{
    //public class Researcher : IdentityUser<int>
    public class Researcher
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public IList<ExpResearcher> ExpResearchers { get; set; }
    }
}
