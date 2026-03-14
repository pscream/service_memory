using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Core.Api.TenantSwitch.Models.Entities.Resources;
using Core.Api.TenantSwitch.DbContexts.Interfaces;

namespace Core.Api.TenantSwitch.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {

        private readonly ITenantDbContext _tenantDbContext;

        public ResourceController(ITenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }

        // GET: api/<ResourceController>
        [HttpGet]
        public async Task<IEnumerable<Resource>> Get()
        {
            if (_tenantDbContext == null)
                throw new InvalidOperationException("TenantDbContext is not available.");
            var resources = await _tenantDbContext.Resources.AsNoTracking().ToListAsync();
            return resources;
        }

        // GET api/<ResourceController>/5
        [HttpGet("{id}")]
        public async Task<Resource> Get(Guid id)
        {
            if (_tenantDbContext == null)
                throw new InvalidOperationException("TenantDbContext is not available.");
            var resource = await _tenantDbContext.Resources.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            return resource != null ? resource : null;
        }

        // POST api/<ResourceController>
        [HttpPost]
        public Task Post([FromBody] string value)
        {
            return Task.CompletedTask;
        }

        // PUT api/<ResourceController>/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] string value)
        {
            return Task.CompletedTask;
        }

        // DELETE api/<ResourceController>/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }

    }

}
