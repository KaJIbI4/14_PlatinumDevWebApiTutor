using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;
using System;

namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();

            context.Notes.AddRange(
                new Note
                {
                    CreatedDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("84E99EF1-B4BE-4E21-B47B-162337F7D8A0"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Note
                {
                    CreatedDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("AC5C2F4B-3477-437F-8DA7-CD7ADE6691C7"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Note
                {
                    CreatedDate = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Id = NoteIdForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Note
                {
                    CreatedDate = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Id = NoteIdForUpdate,
                    Title = "Title4",
                    UserId = UserBId
                }
                );

            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
