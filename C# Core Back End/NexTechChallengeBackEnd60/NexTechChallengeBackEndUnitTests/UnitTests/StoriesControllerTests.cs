using NexTechChallengeBackEnd.Api.Controllers;
using NexTechChallengeBackEnd.Api.Repositories;
using Moq;
using Xunit;

namespace NexTechChallengeBackEndUnitTests.UnitTests;

public class StoriesControllerTests
{
    //  UnitOfWork_StateUnderTest_ExpectedBehavior

    [Fact]
    public async Task GetNewestStories_WithStories_ReturnsNotNullOrEmptyStoryList()
    {
        // Arrange
        HttpClient httpClient = new();
        Mock<StoryRepository> repositoryStub = new();
        Mock<CommunicationClient> httpClientStub = new(httpClient);
        StoriesController controller = new StoriesController(repositoryStub.Object, httpClientStub.Object);

        // Act
        var result = await controller.GetNewestStories();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}