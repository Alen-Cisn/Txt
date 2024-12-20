
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Txt.Shared.Commands;
using Txt.Shared.Dtos;
using Txt.Shared.Queries;
namespace Txt.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class NotesController(IMediator mediator) : ControllerBase
{

    /// <summary>
    /// Retrieves a list of notes within a specified folder.
    /// </summary>
    /// <param name="folderId">The ID of the folder to retrieve notes from.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of NoteDto objects.</returns>
    [HttpGet]
    public Task<List<NoteDto>> Get([FromQuery] int folderId, CancellationToken cancellationToken)
        => mediator.Send(new NotesByFolderIdQuery(folderId: folderId), cancellationToken);


    /// <summary>
    /// Retrieves a specific note by its path.
    /// </summary>
    /// <param name="path">The path of the note to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a NoteDto object.
    /// </returns>
    [HttpGet(@"/notes/path/{*path}")]
    public Task<NoteDto> Get(string path, CancellationToken cancellationToken)
        => mediator.Send(new NoteByPathQuery() { Path = "/" + path }, cancellationToken);


    /// <summary>
    /// Retrieves a specific note by its ID.
    /// </summary>
    /// <param name="id">The ID of the note to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a NoteDto object.</returns>
    [HttpGet(@"{id:int}")]
    public Task<NoteDto> GetById(int id, CancellationToken cancellationToken)
        => mediator.Send(new NoteByIdQuery(id), cancellationToken);

    /// <summary>
    /// Creates a new note.
    /// </summary>
    /// <param name="command">The command containing the data of the note to be created.</param>
    /// <returns>
    /// A 200 OK response containing the newly created <see cref="NoteDto"/>,
    /// or a 400 Bad Request response containing error information.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<NoteDto>> Post([FromBody] CreateNoteCommand command)
        => (await mediator.Send(command)).Match<ActionResult<NoteDto>>(
            note => Ok(note),
            error => BadRequest(error)
            );


    /// <summary>
    /// Updates an existing note with the provided data.
    /// </summary>
    /// <param name="command">The command containing the data to update the note.</param>
    /// <returns>
    /// A 200 OK response containing the updated <see cref="NoteDto"/>,
    /// or a 400 Bad Request response containing error information.
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<NoteDto>> Put([FromBody] UpdateNoteCommand command)
        => (await mediator.Send(command)).Match<ActionResult<NoteDto>>(
            note => Ok(note),
            error => BadRequest(error)
            );

    /// <summary>
    /// Deletes a note based on the provided command.
    /// </summary>
    /// <param name="command">The command containing the ID of the note to be deleted.</param>
    /// <returns>
    /// A 200 OK response containing the name of the deleted note as a string,
    /// or a 400 Bad Request response containing error information.
    /// </returns>
    [HttpDelete(@"{id:int}")]
    public async Task<ActionResult<string>> Delete(int id)
        => (await mediator.Send(new DeleteNoteCommand() { NoteId = id })).Match<ActionResult<string>>(
            noteName => Ok(noteName),
            error => BadRequest(error)
            );
}
