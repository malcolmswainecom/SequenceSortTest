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

        /// <summary>
        /// Get latest saved sequence
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IProcessedSequenceDto Get()
        {
            return sequenceService.GetLatest();
        }

        /// <summary>
        /// Save a sequence if it doesn't exist, otherwise return existing 
        /// In real life might make this async if save op has overhead
        /// </summary>
        /// <param name="unsortedList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] IList<double> unsortedList)
        {
            var created = sequenceService.SaveIfNotExists(unsortedList);
            return Created("", created); // should only return created if actually created entity, otherwise OK 200
        }
    }
}