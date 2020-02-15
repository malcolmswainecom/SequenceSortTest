using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sequence.Data;
using Sequence.Services;
using System.Collections.Generic;

namespace Sequence.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SequenceController : ControllerBase
    {
        private readonly ILogger<SequenceController> _logger;
        private readonly ISequenceService _sequenceService;

        public SequenceController(ILogger<SequenceController> logger, ISequenceService sequenceService)
        {
            _logger = logger;
            _sequenceService = sequenceService;
        }

        // todo : should oribably retrn dto, not entity

        [HttpGet]
        public IEnumerable<ProcessedSequence> Get()
        {
            return _sequenceService.GetLatest();
        }

        [HttpPost]
        public ActionResult Post([FromBody] IEnumerable<double> sequence)
        {
            var created = _sequenceService.SaveIfNotExists(sequence);
            return Created("", created); // should only return created if actually created entity, otherwise OK 200
        }
    }
}