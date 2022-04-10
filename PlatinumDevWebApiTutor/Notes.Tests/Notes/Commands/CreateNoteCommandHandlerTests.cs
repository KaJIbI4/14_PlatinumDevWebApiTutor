using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "note name";
            var noteDetails = "note details";

            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Detais = noteDetails,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);

            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId && note.Title == noteName && note.Details == noteDetails));
        }
    }
}
