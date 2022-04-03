﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNode;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper mapper;

        public NoteController(IMapper mapper) => this.mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteListVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;

            var noteId = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = noteId });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand()
            {
                Id = id,
                UserId = UserId
            };

            await Mediator.Send(command);

            return NoContent();
        }
    }
}