using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IOccurrenceService
    {
        Task<IEnumerable<OccurrenceMessageTypeDto>> GetAllOccurrenceMessageType();
        Task<IEnumerable<OccurrenceStatusDto>> GetAllOccurrenceStatus();
        Task<IEnumerable<OccurrenceSubjectDto>> GetAllOccurrenceSubject();
        Task<OccurrenceDto> GetOccurrence(int occurrenceId);
        Task<OccurrenceDto> SaveOccurrence(OccurrenceDto occurrenceDto, IFormFile formFile, int userId);
        Task<OccurrenceDto> UpdateOccurrence(OccurrenceDto occurrenceDto);
        Task<OccurrenceMessageDto> SaveMessage(OccurrenceMessageDto occurrenceDto, IFormFile formFile, int userId);
        Task<IEnumerable<OccurrenceMessage>> GetMessagesOccurenceByUser(int occurrenceId);
        Task<IEnumerable<Occurrence>> GetOccurrenceByUser(int userId);
        Task<IEnumerable<OccurrenceMessage>> GetMessagesOccurenceByUserEai(int occurrenceId);
        Task<IEnumerable<Occurrence>> GetOccurrenceAdmin(OccurrenceaAdminFilterDto occurrenceaAdminFilterDto);
        Task<IEnumerable<Occurrence>> GetOccurrenceAdminEai(OccurrenceaAdminFilterDto occurrenceaAdminFilterDto);
    }
}
