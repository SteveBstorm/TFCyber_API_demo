using DAL = GestionContact_DAL.Entities;
using API = TFCyber_API.Models;
namespace TFCyber_API.Tools
{
    public static class Mappers
    {
        public static DAL.Contact ToDal(this API.Contact c)
        {
            return new DAL.Contact
            {
                Id = c.Id,
                Email = c.Email,
                Firstname = c.Firstname,
                Lastname = c.Lastname,
                Telephone = c.Telephone
            };
        }
    }
}
