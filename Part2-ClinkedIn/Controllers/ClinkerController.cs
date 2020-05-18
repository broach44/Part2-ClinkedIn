using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2_ClinkedIn.DataAccessLayer;
using Part2_ClinkedIn.Models;

namespace Part2_ClinkedIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinkerController : ControllerBase
    {
        ClinkerRepo _repository = new ClinkerRepo();


        [HttpPost]
        public IActionResult AddClinker(Clinker clinkerToAdd)
        {
            var existingClinker = _repository.GetByFullName(clinkerToAdd);
            if (existingClinker == null)
            {
                _repository.Add(clinkerToAdd);
                return Created("", clinkerToAdd);
            }
            else
            {
                return BadRequest("User already exists, please try again.");
            }
        }

        [HttpPost("services")]
        public IActionResult CreateService(Services ServiceToAdd)
        {
            var existingService = _repository.CheckForService(ServiceToAdd);
            if (existingService == null)
            {
                _repository.CreateService(ServiceToAdd);
                return Ok(ServiceToAdd);
            }
            else
            {
                return BadRequest("This service already exists");
            }
        }

        [HttpGet]
        public IActionResult GetAllClinkers()
        {
            var allClinkers = _repository.GetAll();

            return Ok(allClinkers);
        }

        // api/Clinker/{id}
        // api/Clinker/5
        [HttpGet("{id}")]
        public IActionResult GetSingleClinker(int id)
        {
            var singleClinker = _repository.GetClinkerById(id);
            if (singleClinker != null)
            {
                return Ok(singleClinker);
            }
            else
            {
                return NotFound("No clinker found");
            }
        }

        // api/Clinker/searchByInterest/{interest}
        // api/Clinker/searchByInterest/origamin
        [HttpGet("searchByInterest/{interest}")]
        public IActionResult GetByInterest(string interest)
        {
            var clinkersWithInterest = _repository.GetClinkerByInterest(interest);
            var isEmpty = !clinkersWithInterest.Any();
            if (!isEmpty)
            {
                return Ok(clinkersWithInterest);
            }
            else
            {
                return Ok("No Clinkers Found With Those Interests");
            }
        }


        //api/Clinker/1/searchForPrisonTerm
        //api/Clinker/{id}/searchForPrisonTerm
        [HttpGet("{id}/searchForPrisonTerm")]
        public IActionResult GetByPrisonTerm(int id)
        {
            var validClinker = _repository.GetClinkerById(id);
            if (validClinker == null)
            {
                return BadRequest($"The user id {id} could not be found.");
            }
            else
            {
                var inmatePrisonTerm = _repository.GetClinkerByPrisonTerm(id);

                return Ok(inmatePrisonTerm);
            }

        }



        // api/Clinker/1/myFriends
        // api/Clinker/{id}/myFriends
        [HttpGet("{id}/myFriends")]
        public IActionResult GetAllMyFriends(int id)
        {
            var allMyFriends = _repository.GetAllMyFriends(id);
            var isEmpty = !allMyFriends.Any();

            if (!isEmpty)
            {
                return Ok(allMyFriends);
            }
            else
            {
                return Ok("You have no friends!");
            }

        }

        // api/Clinker/1/FriendsOfMyFriends
        // api/Clinker/{id}/FriendsOfMyFriends
        [HttpGet("{id}/FriendsOfMyFriends")]
        public IActionResult GetAllFriendsOfMyFriends(int id)
        {
            var allMyFriends = _repository.AllFriendsOfFriends(id);
            var isEmpty = !allMyFriends.Any();

            if (!isEmpty)
            {
                return Ok(allMyFriends);
            }
            else
            {
                return Ok("Your friends have no friends");
            }
        }


        //api/Clinker/1/addFriend/2       
        [HttpPut("{clinkerId}/addFriend/{friendId}")]
        public IActionResult UpdateClinkerFriends(int clinkerId, int friendId)
        {
            var updatedClinker = _repository.UpdateFriend(clinkerId, friendId);
            if (updatedClinker != null)
            {
                return Ok(updatedClinker);
            }
            else
            {
                return BadRequest("The Clinker does not exist.");
            }
        }

        //api/Clinker/1/addEnemy/2       
        [HttpPut("{clinkerId}/addEnemy/{enemyId}")]
        public IActionResult UpdateClinkerEnemies(int clinkerId, int enemyId)
        {
            var updatedClinker = _repository.UpdateEnemy(clinkerId, enemyId);
            if (updatedClinker != null)
            {
                return Ok(updatedClinker);
            }
            else
            {
                return BadRequest("The Clinker does not exist.");
            }
        }

        //api/Clinker/AddInterest/1/archery
        [HttpPut("AddInterest/{id}/{interest}")]
        public IActionResult AddInterest(int id, string interest)
        {
            var clinkerExists = _repository.GetClinkerById(id);
            if (clinkerExists == null)
            {
                return BadRequest($"The user id {id} could not be found.");
            }
            else
            {
                var currentInterests = _repository.GetInterestsByClinkerId(id);
                if (currentInterests.Contains(interest))
                {
                    return BadRequest($"{interest} is already on {clinkerExists.FirstName}'s interest list");
                }
                else
                {
                    _repository.CheckMasterInterestsAndUpdate(interest);
                    clinkerExists.AddInterests(interest);
                    return Ok(_repository.GetClinkerById(id));
                }
            }
        }

        //api/Clinker/RemoveInterest/1/archery
        [HttpPut("RemoveInterest/{id}/{interest}")]
        public IActionResult RemoveClinkerInterest(int id, string interest)
        {
            var clinkerExists = _repository.GetClinkerById(id);
            if (clinkerExists == null)
            {
                return BadRequest($"The user id {id} could not be found.");
            }
            else
            {
                var currentInterests = _repository.GetInterestsByClinkerId(id);
                if (currentInterests.Contains(interest))
                {
                    clinkerExists.RemoveInterests(interest);
                    return Ok(_repository.GetClinkerById(id));
                }
                else
                {
                    return BadRequest($"{interest} was not found on {clinkerExists.FirstName}'s interest list");

                }
            }
        }

        //api/Clinker/AddService/1
        [HttpPut("AddService/{clinkerId}")]
        public IActionResult AddService(int clinkerId, Services serviceToAdd)
        {
            var clinkerExists = _repository.GetClinkerById(clinkerId);
            if (clinkerExists == null)
            {
                return BadRequest($"The user id {clinkerId} could not be found.");
            }
            else
            {
                var currentClinkerService = _repository.GetServicesByClinkerId(clinkerId);
                if (currentClinkerService.FirstOrDefault(s => s.Name.ToLower() == serviceToAdd.Name.ToLower()) != null)
                {
                    return BadRequest($"{serviceToAdd.Name} is already on {clinkerExists.FirstName}'s service list");
                }
                else
                {
                    var currentService = _repository.CheckForService(serviceToAdd);
                    if (currentService == null)
                    {
                        _repository.CreateService(serviceToAdd);
                        var newService = _repository.CheckForService(serviceToAdd);
                        clinkerExists.AddService(newService);
                    }
                    else
                    {
                        clinkerExists.AddService(currentService);
                    }
                    return Ok(_repository.GetClinkerById(clinkerId));
                }
            }
        }

        //api/Clinker/RemoveService/1
        [HttpPut("RemoveService/{clinkerId}")]
        public IActionResult RemoveClinkerService(int clinkerId, Services serviceToRemove)
        {
            var clinkerExists = _repository.GetClinkerById(clinkerId);
            if (clinkerExists == null)
            {
                return BadRequest($"The user id {clinkerId} could not be found.");
            }
            else
            {
                var currentClinkerServices = _repository.GetServicesByClinkerId(clinkerId);
                var matchToCurrentService = currentClinkerServices.FirstOrDefault(s => s.Name.ToLower() == serviceToRemove.Name.ToLower());
                if (matchToCurrentService == null)
                {
                    return BadRequest($"{serviceToRemove.Name} was not found on {clinkerExists.FirstName}'s service list");
                }
                else
                {
                    clinkerExists.RemoveService(matchToCurrentService);
                    return Ok(_repository.GetClinkerById(clinkerId));
                }
            }
        }

    }
}