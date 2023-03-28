using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Videos.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideosListQueryHandlerXUnitTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            GetVideosListQueryHandler handler = new GetVideosListQueryHandler(_unitOfWork.Object, _mapper);
            GetVideosListQuery request = new GetVideosListQuery("maumendez");

            var result = handler.Handle(request, CancellationToken.None);
            result.ShouldBeOfType<List<VideosVm>>();




        }
    }
}
