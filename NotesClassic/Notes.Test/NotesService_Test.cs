using Notes.Service;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Notes.UnitTest.Service
{
    public class NotesService_Test
    {
        private TestNotesRepository repository;
        private NotesService service;

        [SetUp]
        public void Setup()
        {
            repository = new TestNotesRepository();
            service = new NotesService(repository);
        }

        [Test]
        public void ShouldAddANewNote()
        {
            service.Add("titolo", "descrizione");

            Assert.That(repository.notes, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddedNote_ShouldContainsTitleAndDescription()
        {
            service.Add("titolo", "descrizione");

            Note note = repository.notes.First();
            Assert.That(note.title, Is.EqualTo("titolo"));
            Assert.That(note.description, Is.EqualTo("descrizione"));
        }

        [Test]
        public void Should_ReturnEmptyList_BeforeAddANote()
        {
            IList<Note> notes = service.All();

            Assert.That(notes, Is.Empty);
        }

        [Test]
        public void Should_RetunrAListOfAddedNotes()
        {
            service.Add("titolo 1", "description 1");
            service.Add("titolo 2", "description 2");

            IList<Note> notes = service.All();
            Assert.That(notes, Has.Count.EqualTo(2));
        }

        [Test]
        public void Should_RetunrAListOfAddedNotes_UsingRepository()
        {
            IList<Note> repositoryNotes = new List<Note>();
            repositoryNotes.Add(new Note("titolo 1", "description 1"));
            repositoryNotes.Add(new Note("titolo 2", "description 2"));
            repository.notes = repositoryNotes;

            IList<Note> notes = service.All();
            Assert.That(notes, Has.Count.EqualTo(2));
        }
    }
}