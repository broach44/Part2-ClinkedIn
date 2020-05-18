using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part2_ClinkedIn.Models
{
    public class Clinker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PrisonTermEndDate { get; set; }
        public List<string> Interests { get; set; }
        public List<Clinker> Enemies { get; set; }
        public List<Clinker> Friends { get; set; }
        public List<Services> Services { get; set; }

        public Clinker()
        {
            Friends = new List<Clinker>();
            Enemies = new List<Clinker>();
            Interests = new List<string>();
            Services = new List<Services>();
        }

        public void AddNewFriend(Clinker newClinkerFriend) => Friends.Add(newClinkerFriend);
        public void AddNewEnemy(Clinker newClinkerEnemy) => Enemies.Add(newClinkerEnemy);
        public void AddInterests(string newInterest) => Interests.Add(newInterest);
        public void AddService(Services newService) => Services.Add(newService);
        public void RemoveInterests(string newInterest) => Interests.Remove(newInterest);
        public void RemoveService(Services newService) => Services.Remove(newService);
    }
}
