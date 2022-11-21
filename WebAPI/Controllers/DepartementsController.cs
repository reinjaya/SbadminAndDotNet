using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Base;
using WebAPI.Modes;
using WebAPI.Repository.Data;

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementsController : BaseController<DepartementRepository, Departement>
    {
        private readonly DepartementRepository _repository;
        public DepartementsController(DepartementRepository departementRepository) : base(departementRepository)
        {
            _repository = departementRepository;
        }
    }
}
