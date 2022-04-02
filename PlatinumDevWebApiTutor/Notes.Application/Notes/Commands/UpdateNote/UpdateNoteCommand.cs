using System;
using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class UpdateNoteCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Detais { get; set; }
    }
}
