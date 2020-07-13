using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class OccurrenceService : IOccurrenceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly IOccurrenceContactTypeRepository _occurrenceContactTypeRepository;
        private readonly IOccurrenceMessageRepository _occurrenceMessageRepository;
        private readonly IOccurrenceMessageTypeRepository _occurrenceMessageTypeRepository;
        private readonly IOccurrenceStatusRepository _occurrenceStatusRepository;
        private readonly IOccurrenceSubjectRepository _occurrenceSubjectRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public OccurrenceService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment env, IOccurrenceRepository occurrenceRepository, IOccurrenceContactTypeRepository occurrenceContactTypeRepository, IOccurrenceMessageRepository occurrenceMessageRepository, IOccurrenceMessageTypeRepository occurrenceMessageTypeRepository, IOccurrenceStatusRepository occurrenceStatusRepository, IOccurrenceSubjectRepository occurrenceSubjectRepository, IUserService userService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _occurrenceRepository = occurrenceRepository;
            _occurrenceContactTypeRepository = occurrenceContactTypeRepository;
            _occurrenceMessageRepository = occurrenceMessageRepository;
            _occurrenceMessageTypeRepository = occurrenceMessageTypeRepository;
            _occurrenceStatusRepository = occurrenceStatusRepository;
            _occurrenceSubjectRepository = occurrenceSubjectRepository;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<OccurrenceMessageTypeDto>> GetAllOccurrenceMessageType() => _mapper.Map(await _occurrenceMessageTypeRepository.GetAll(), new List<OccurrenceMessageTypeDto>());

        public async Task<IEnumerable<OccurrenceStatusDto>> GetAllOccurrenceStatus() => _mapper.Map(await _occurrenceStatusRepository.GetAll(), new List<OccurrenceStatusDto>());

        public async Task<IEnumerable<OccurrenceSubjectDto>> GetAllOccurrenceSubject() => _mapper.Map(await _occurrenceSubjectRepository.GetAll(), new List<OccurrenceSubjectDto>());

        public async Task<IEnumerable<OccurrenceMessage>> GetMessagesOccurenceByUser(int occurrenceId) =>
            await _occurrenceMessageRepository.CustomFind(x => x.Occurrence.Id == occurrenceId,
                x => x.OccurrenceMessageType,
                x => x.User,
                x => x.Occurrence,
                x => x.Occurrence.OccurrenceContactType,
                x => x.Occurrence.OccurrenceStatus,
                x => x.Occurrence.OccurrenceSubject,
                x => x.Occurrence.User,
                x => x.Occurrence.User.Office);

        public async Task<IEnumerable<OccurrenceMessage>> GetMessagesOccurenceByUserEai(int occurrenceId) =>
            await _occurrenceMessageRepository.CustomFind(x => x.Occurrence.Id == occurrenceId && x.Catalog,
                x => x.OccurrenceMessageType,
                x => x.User,
                x => x.Occurrence,
                x => x.Occurrence.OccurrenceContactType,
                x => x.Occurrence.OccurrenceStatus,
                x => x.Occurrence.OccurrenceSubject,
                x => x.Occurrence.User,
                x => x.Occurrence.User.Office);

        public async Task<OccurrenceDto> GetOccurrence(int occurrenceId) =>
            _mapper.Map(await _occurrenceRepository.CustomFind(x => x.Id == occurrenceId,
                x => x.OccurrenceContactType,
                x => x.OccurrenceStatus,
                x => x.OccurrenceSubject), new OccurrenceDto());

        public async Task<IEnumerable<Occurrence>> GetOccurrenceAdmin(OccurrenceaAdminFilterDto occurrenceaAdminFilterDto) => await _occurrenceRepository.GetOccurrenceFilter(
           occurrenceaAdminFilterDto.Catalog,
            occurrenceaAdminFilterDto.Critical,
            occurrenceaAdminFilterDto.Participant,
             occurrenceaAdminFilterDto.Name,
            occurrenceaAdminFilterDto.Office,
            occurrenceaAdminFilterDto.Network,
            occurrenceaAdminFilterDto.CreatedAt,
            occurrenceaAdminFilterDto.ClosedAt,
            occurrenceaAdminFilterDto.Cpf,
            occurrenceaAdminFilterDto.Code,
            occurrenceaAdminFilterDto.TypeContact,
            occurrenceaAdminFilterDto.Subject,
            occurrenceaAdminFilterDto.Status,
            occurrenceaAdminFilterDto.Iteration);

        public async Task<IEnumerable<Occurrence>> GetOccurrenceAdminEai(OccurrenceaAdminFilterDto occurrenceaAdminFilterDto) => await _occurrenceRepository.GetOccurrenceFilterEai(
            occurrenceaAdminFilterDto.Name,
            occurrenceaAdminFilterDto.CreatedAt,
            occurrenceaAdminFilterDto.ClosedAt,
            occurrenceaAdminFilterDto.Cpf,
            occurrenceaAdminFilterDto.Code,
            occurrenceaAdminFilterDto.Subject);

        public async Task<IEnumerable<Occurrence>> GetOccurrenceByUser(int userId) =>
            await _occurrenceRepository.CustomFind(x => x.User.Id == userId,
                x => x.OccurrenceContactType,
                x => x.OccurrenceStatus,
                x => x.OccurrenceSubject);

        public async Task<OccurrenceMessageDto> SaveMessage(OccurrenceMessageDto occurrenceDto, IFormFile formFile, int userId)
        {
            if (formFile != null)
            {
                if (formFile.Length > 0)
                {
                    var extensions = Path.GetFileName(formFile.FileName).Split('.').Last().ToUpper();

                    var fileName = $"{DateTime.Now.Year}" +
                                $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

                    var path = Path.Combine(_env.WebRootPath, $"Content/Occurrence/{Path.GetFileName(fileName)}");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    occurrenceDto.File = fileName;

                }
            }

            var occurrenceMessage = _mapper.Map(occurrenceDto, new OccurrenceMessage());

            var occurrence = await _occurrenceRepository.GetById(occurrenceDto.Occurrence.Id);

            occurrenceMessage.Occurrence = occurrence;
            occurrenceMessage.OccurrenceMessageType = await _occurrenceMessageTypeRepository.GetById(occurrenceDto.OccurrenceMessageTypeId);
            occurrenceMessage.CreatedAt = DateTime.Now;
            occurrenceMessage.Activated = true;

            occurrenceMessage.User = await _userService.GetUserById(userId);

            if (occurrenceMessage.User.Office.Id == (int)OfficeEnum.BrasilCT)
                occurrence.ReturnedAt = DateTime.Now;

            if (occurrenceMessage.User.Office.Id != (int)OfficeEnum.BrasilCT && occurrenceMessage.Internal && occurrenceMessage.Catalog)
                occurrence.RedirectedAt = DateTime.Now;

            occurrence.LastIteration = occurrenceMessage.OccurrenceMessageType.Description;

            _occurrenceMessageRepository.Save(occurrenceMessage);

            await _unitOfWork.CommitAsync();

            return _mapper.Map(occurrenceMessage, occurrenceDto);

        }

        public async Task<OccurrenceDto> SaveOccurrence(OccurrenceDto occurrenceDto, IFormFile formFile, int userId)
        {
            if (formFile != null)
            {
                if (formFile.Length > 0)
                {
                    var extensions = Path.GetFileName(formFile.FileName).Split('.').Last().ToUpper();

                    var fileName = $"{DateTime.Now.Year}" +
                                $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                                $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

                    var path = Path.Combine(_env.WebRootPath, $"Content/Occurrence/{Path.GetFileName(fileName)}");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    occurrenceDto.File = fileName;

                }
            }

            var occurrence = _mapper.Map(occurrenceDto, new Occurrence());

            occurrence.OccurrenceContactType = await _occurrenceContactTypeRepository.GetById(occurrenceDto.OccurrenceContactTypeId);
            occurrence.OccurrenceSubject = await _occurrenceSubjectRepository.GetById(occurrenceDto.OccurrenceSubjectId);
            occurrence.OccurrenceStatus = await _occurrenceStatusRepository.GetById(occurrenceDto.OccurrenceStatusId == 0 ? (int)OccurrenceStatusEnum.Open : occurrenceDto.OccurrenceStatusId);
            occurrence.ClosedAt = DateTime.MinValue;

            if (string.IsNullOrEmpty(occurrenceDto.Cpf))
                occurrence.User = await _userService.GetUserById(userId);
            else
                occurrence.User = (await _userRepository.CustomFind(x => x.Cpf.Equals(occurrenceDto.Cpf))).First();

            occurrence.CreatedAt = DateTime.Now;
            occurrence.Activated = true;
            occurrence.Code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8).Select(s => s[new Random().Next(s.Length)]).ToArray());

            _occurrenceRepository.Save(occurrence);

            await _unitOfWork.CommitAsync();

            occurrenceDto.OccurrenceMessage.Occurrence = new OccurrenceDto();

            occurrenceDto.OccurrenceMessage.Occurrence.Id = occurrence.Id;

            await SaveMessage(occurrenceDto.OccurrenceMessage, formFile, userId);

            return _mapper.Map(occurrence, occurrenceDto);
        }

        public async Task<OccurrenceDto> UpdateOccurrence(OccurrenceDto occurrenceDto)
        {

            var occurrence = await _occurrenceRepository.GetById(occurrenceDto.Id);

            occurrence.OccurrenceContactType = await _occurrenceContactTypeRepository.GetById(occurrenceDto.OccurrenceContactTypeId);
            occurrence.OccurrenceSubject = await _occurrenceSubjectRepository.GetById(occurrenceDto.OccurrenceSubjectId);
            occurrence.OccurrenceStatus = await _occurrenceStatusRepository.GetById(occurrenceDto.OccurrenceStatusId);
            occurrence.BrazilCTCall = !string.IsNullOrEmpty(occurrenceDto.BrazilCTCall) ? occurrenceDto.BrazilCTCall : "";

            if (occurrenceDto.OccurrenceStatusId == (int)OccurrenceStatusEnum.Closed)
                occurrence.ClosedAt = DateTime.Now;

            await _unitOfWork.CommitAsync();

            return _mapper.Map(occurrence, occurrenceDto);
        }


    }
}
