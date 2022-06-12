using Models;
using Moq;
using Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace InformacionIPTest
{
    public class InformacionIPServiceTest
    {
        IGeolocalizacionService _geolocalizacionService;
        ICacheService _cacheService;

        private void SetupMocks()
        {
            var geolocalizcionMock = new Mock<IGeolocalizacionService>();
            var cacheMock = new Mock<ICacheService>();

            geolocalizcionMock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Geolocalizacion { CountryName = null}));

            cacheMock.Setup(x => x.GetCacheValueAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(null));

            _geolocalizacionService = geolocalizcionMock.Object;
            _cacheService = cacheMock.Object;
        }

        [Fact]
        public async Task ValidIPReturnExceptionTest()
        {
            //Arrange 
            var infoIPService = new InformacionIPService(null, null, null);

            //Act
            var act = () => infoIPService.Get("12.asa.131");

            //Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("La IP es invalida", exception.Message);
        }

        [Fact]
        public async Task GetGeolocalizacionServiceReturnExceptionTest()
        {
            //Arrange 
            SetupMocks();
            var infoIPService = new InformacionIPService(_geolocalizacionService, null, _cacheService);

            //Act
            var act = () => infoIPService.Get("12.123.131");

            //Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("No hay información de la ip", exception.Message);
        }


    }
}