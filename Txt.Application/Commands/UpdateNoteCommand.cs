using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Txt.Application.Commands.Interfaces;
using Txt.Application.PipelineBehaviors;
using Txt.Domain.Entities;
using Txt.Domain.Repositories.Interfaces;
using Txt.Shared.Commands;
using Txt.Shared.Dtos;
using Txt.Shared.ErrorModels;
using Txt.Shared.Exceptions;
using Txt.Shared.Result;

namespace Txt.Application.Commands;

public class UpdateNoteCommandHandler(INotesModuleRepository notesModuleRepository, IMapper mapper, ILogger<UpdateNoteCommandHandler> logger)
    : ICommandHandler<UpdateNoteCommand, NoteDto>
{
    public async Task<OneOf<NoteDto, Error>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var note = await notesModuleRepository
                .FindNotesWhere(note => note.Id == request.NoteId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Given note doesn't exist.");

            note.Id = request.NoteId;
            note.Name = request.Name;

            if (note.ParentId != request.ParentId)
            {
                Folder folder = await notesModuleRepository
                    .FindFoldersWhere(f => f.Id == request.ParentId)
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new ValidationException("Given parent folder doesn't exist.");
                note.Path = folder.Path + "/" + request.Name;
            }

            if (await notesModuleRepository
                .FindNotesWhere(n => n.Path == note.Path && n.Id != note.Id)
                .AnyAsync(cancellationToken))
            {
                throw new ValidationException("Given note name already exists.");
            }

            if (await notesModuleRepository
                .FindFoldersWhere(f => f.Path == note.Path)
                .AnyAsync(cancellationToken))
            {
                throw new ValidationException("Given name already exists as a folder");
            }

            note.ParentId = request.ParentId;

            notesModuleRepository.UpdateNote(note);

            await notesModuleRepository.SaveAsync(cancellationToken);

            return new(mapper.Map<NoteDto>(note));
        }
        catch (Exception ex)
        {
            return new(ExceptionHandlerExtension.HandleException(ex, logger));
        }
    }
}