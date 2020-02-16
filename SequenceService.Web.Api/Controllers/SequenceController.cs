using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sequence.Data;
using Sequence.Services;
using System;
using System.Collections.Generic;

namespace Sequence.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SequenceController : ControllerBase
    {
        private readonly ILogger<SequenceController> logger;
        private readonly ISequenceService sequenceService;

        public SequenceController(ILogger<SequenceController> _logger, ISequenceService _sequenceService)
        {
            logger = _logger;
            sequenceService = _sequenceService;
        }

        // todo : should oribably retrn dto, not entity

        [HttpGet]
        public ProcessedSequence Get()
        {
            return sequenceService.GetLatest();
        }

        [HttpPost]
        public ActionResult Post([FromBody] List<double> unsortedList)
        {
            //var unsorted = String.Join(", ", unsortedList.ToArray());
            var created = sequenceService.SaveIfNotExists(unsortedList);
            return Created("", created); // should only return created if actually created entity, otherwise OK 200
        }
    }
}