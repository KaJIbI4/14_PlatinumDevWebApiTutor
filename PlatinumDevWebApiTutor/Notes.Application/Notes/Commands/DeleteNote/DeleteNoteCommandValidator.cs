using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(DeleteNoteCommand => DeleteNoteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(DeleteNoteCommand => DeleteNoteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
