using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Base;
using WebAPI.Modes;
using WebAPI.Repository.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : BaseController<DivisionRepository, Division>
    {
        private readonly DivisionRepository _repository;
        public DivisionsController(DivisionRepository divisionRepository) : base(divisionRepository)
        {
            _repository = divisionRepository;
        }
    }
}
