using AutoMapper;
using System;
using Whp.MaisTop.Business.Mapping;
using Xunit;

namespace Whp.MaisTop.Tests.Whp.MaisTop.Business.Mapping
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingProfile_VerifyValidConfiguration()
        {
            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            var mapper = new Mapper(config);

            (mapper as IMapper).ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
