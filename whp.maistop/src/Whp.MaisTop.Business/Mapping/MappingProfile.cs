using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Utils;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {

            CreateMap<User, UserExcelDto>()
                .ForMember(x => x.Cnpj, o => o.Ignore())
                .ForMember(x => x.Network, o => o.Ignore())
                .ForMember(x => x.BithDate, o => o.MapFrom(m => (m.BithDate != null ?  m.BithDate.Value.ToString("dd/mm/yyyy") : "")))
                .ForMember(x => x.CreatedAt, o => o.MapFrom(m => (m.CreatedAt != DateTime.MinValue ? m.CreatedAt.ToString("dd/mm/yyyy") : "")))
                .ForMember(x => x.PasswordRecoveredAt, o => o.MapFrom(m => (m.PasswordRecoveredAt != null ? m.PasswordRecoveredAt.Value.ToString("dd/mm/yyyy") : "")))
                .ForMember(x => x.OffIn, o => o.MapFrom(m => (m.OffIn != null ? m.OffIn.Value.ToString("dd/mm/yyyy") : "")))
                .ForMember(x => x.Activated, o => o.MapFrom(m => (m.Activated ? "SIM" : "NÃO")))
                .ForMember(x => x.PrivacyPolicy, o => o.MapFrom(m => (m.PrivacyPolicy ? "SIM" : "NÃO")))
                .ForMember(x => x.UserStatus, o => o.MapFrom(m => m.UserStatus.Description))
                .ForMember(x => x.Office, o => o.MapFrom(m => m.Office.Description ))
                .ForMember(x => x.CivilStatus, o => o.MapFrom(m => m.CivilStatus.ToString()))
                .ForMember(x => x.Gender, o => o.MapFrom(m => m.Gender.ToString()));

            CreateMap<User, UserDto>()
                .ForMember(x => x.AccessCode, o => o.Ignore())
                .ForMember(x => x.Network, o => o.Ignore())
                .ForMember(x => x.Shop, o => o.Ignore())
                .ForMember(x => x.Password, o => o.Ignore())
                .ForMember(x => x.UserStatusId, o => o.Ignore())
                .ForMember(x => x.OfficeId, o => o.Ignore())
                .ForMember(x => x.OldPassword, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.Office, o => o.Ignore())
                .ForMember(x => x.PasswordRecoveredAt, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.OffIn, o => o.Ignore())
                .ForMember(x => x.OffIn, o => o.Ignore())
                .ForMember(x => x.Password, o => o.Ignore())
                .ForMember(x => x.AccessCodeInvite, o => o.Ignore())
                .ForMember(x => x.UserStatus, o => o.Ignore());

            CreateMap<PhraseologyCategoryDto, PhraseologyCategory>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ReverseMap();

            CreateMap<PhraseologySubjectDto, PhraseologySubject>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.PhraseologyCategory, x => x.Ignore())
                .ReverseMap()
                .ForMember(x => x.PhraseologyCategoryId, x => x.Ignore());

            CreateMap<PhraseologyTypeSubjectDto, PhraseologyTypeSubject>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.PhraseologySubject, x => x.Ignore())
                .ReverseMap()
                .ForMember(x => x.PhraseologySubjectId, x => x.Ignore());

            CreateMap<PhraseologyDto, Phraseology>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.PhraseologyTypeSubject, x => x.Ignore())
                .ReverseMap()
                .ForMember(x => x.PhraseologyTypeSubjectId, x => x.Ignore());

            CreateMap<HierarchyFileDto, HierarchyFile>()
                .ReverseMap();

            CreateMap<Order, OrderReturnDto>()
                .ForMember(x => x.AccessToken, o => o.Ignore())
                .ForMember(x => x.Activated, o => o.Ignore())
                .ForMember(x => x.AuthorizationCode, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.Description, o => o.Ignore())
                .ForMember(x => x.Login, o => o.Ignore())
                .ReverseMap();

            CreateMap<OrderReversalItem, ReversalRequestItemsDto>()
                .ReverseMap()
                .ForMember(x => x.OrderItem, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore());

            CreateMap<ProductDto, Product>()
                .ForMember(x => x.Producer, o => o.Ignore())
                .ForMember(x => x.CategoryProduct, o => o.Ignore())
                .ForMember(x => x.Activated, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.Photo, o => o.Ignore())
                .ForMember(x => x.Ean, o => o.MapFrom(m => (m.Ean != null ? m.Ean : "")))
                .ReverseMap()
                .ForMember(x => x.ProducerId, o => o.Ignore())
                .ForMember(x => x.CategoryProductId, o => o.Ignore());

            CreateMap<FocusProductDto, FocusProduct>()
                .ForMember(x => x.Network, o => o.Ignore())
                .ForMember(x => x.Product, o => o.Ignore())
                .ForMember(x => x.Activated, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.GroupProduct, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.NetworkId, o => o.Ignore())
                .ForMember(x => x.GroupProductId, o => o.Ignore())
                .ForMember(x => x.ProductId, o => o.Ignore());

            CreateMap<ParticipantProductDto, ParticipantProduct>()
                .ForMember(x => x.Network, o => o.Ignore())
                .ForMember(x => x.Product, o => o.Ignore())
                .ForMember(x => x.Activated, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.NetworkId, o => o.Ignore())
                .ForMember(x => x.ProductId, o => o.Ignore());

            CreateMap<OrderReturnDto, Order>()
                .ReverseMap()
                .ForMember(x => x.Description, o => o.Ignore());

            CreateMap<RightAnswer, AnswerUserQuiz>()
                .ReverseMap()
                .ForMember(x => x.CreatedAt, o => o.Ignore());

            CreateMap<Occurrence, OccurrenceDto>()
                .ForMember(x => x.OccurrenceStatusId, o => o.Ignore())
                .ForMember(x => x.OccurrenceSubjectId, o => o.Ignore())
                .ForMember(x => x.OccurrenceContactTypeId, o => o.Ignore())
                .ForMember(x => x.Cpf, o => o.Ignore())
                .ForMember(x => x.OccurrenceMessage, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.User, o => o.Ignore());
            
            CreateMap<OccurrenceMessage, OccurrenceMessageDto>()
                .ForMember(x => x.OccurrenceMessageTypeId, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.User, o => o.Ignore());

            CreateMap<SaleFileData, SaleFileDataDto>()
                .ForMember(x => x.NotExisting, o => o.Ignore())
                .ReverseMap();
        }
    }
}
