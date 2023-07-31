using AutoMapper;
using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Interface.Facades;
using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Models;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;

namespace CreditsAplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientAccountFacade _clientAccountFacade;
        private readonly IMapper _mapper;

        public ClientController(IClientAccountFacade clientAccountFacade, IMapper mapper)
        {
            _clientAccountFacade = clientAccountFacade;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientAccountFacade.GetAllAsync();
            return Ok(_mapper.Map<List<ClientDto>>(clients));
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientAccountFacade.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(_mapper.Map<ClientDto>(client));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateClient(ClientCreationDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            await _clientAccountFacade.AddAsync(client);

            // Generate the URL for the newly created client
            return CreatedAtRoute("GetClientById", new { id = client.Id }, _mapper.Map<ClientDto>(client));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientUpdateDto updatedClient)
        {
            var existingClient = await _clientAccountFacade.GetByIdAsync(id);
            if (existingClient == null)
                return NotFound();

            var client = _mapper.Map<Client>(updatedClient);
            client.Id = existingClient.Id; // Ensure the correct ID is set
            client.PersonId = existingClient.PersonId; // Ensure the correct ID is set
            await _clientAccountFacade.UpdateAsync(client);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchClient(int id, [FromBody] JsonPatchDocument patchDocument)
        {
            var client = await _clientAccountFacade.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            patchDocument.ApplyTo(client);
            client.Id = id;
            await _clientAccountFacade.UpdateAsync(client);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientAccountFacade.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            await _clientAccountFacade.DeleteAsync(id);

            return NoContent();
        }
    }
}
