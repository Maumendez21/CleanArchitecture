using AutoFixture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRepository()
        {
            Fixture fixture = new Fixture();
            List<Video> videos = fixture.CreateMany<Video>().ToList();

            Mock<IVideoRepository> mockRepository = new Mock<IVideoRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(videos);
            return mockRepository;
        }
    }
}
