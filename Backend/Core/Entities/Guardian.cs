using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Guardian
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public string? PersonalEmail { get; set; }
        public string? Phone {  get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public string? Photo {  get; set; }
        public string? RelationShip { get; set; }
        public string EntityStatus { get; set; }
        public DateTime? DismissalDate { get; set; }
        public string? DismissalReason { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
