using System.Collections.Generic;
using BidKaro.Model.CustomModel;
using BidKaro.Model;

namespace BidKaro.Models
{
    public class UsersView
    {
     
        public Users CurrentUsers { get; set; }
        public List<UsersCustomModel> UsersList { get; set; }

    }
}
