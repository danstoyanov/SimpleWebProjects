using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class UserPlayer
    {
        public string UserId { get; set; } 
        
        public User User { get; set; } 
        
        public string PlayerId { get; set; } 

        public Player Player { get; set; } 
    }
}
