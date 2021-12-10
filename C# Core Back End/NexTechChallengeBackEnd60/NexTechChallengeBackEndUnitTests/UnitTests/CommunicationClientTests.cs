using NexTechChallengeBackEnd.Api.Repositories;
using Moq;
using Xunit;

namespace NexTechChallengeBackEndUnitTests.UnitTests;

public class CommunicationClientTests
{
    //  UnitOfWork_StateUnderTest_ExpectedBehavior

    [Fact]
    public async Task GetNewestStories_WithNullStoryList_ReturnsEmptyStoryList()
    {
        // Arrange
        HttpClient httpClient = new();
        Mock<IStoryRepository> repositoryStub = new();
        CommunicationClient commClient = new(httpClient);

        // Act
        var result = await commClient.GetNewestStories(repositoryStub.Object.GetNewestStories());

        // Assert
        Assert.Empty(result);
    }


    [Fact]
    public async Task StoreStory_WithInvalidStoryId_ReturnsEmptyStory()
    {
        // Arrange
        HttpClient httpClient = new();
        CommunicationClient commClient = new(httpClient);

        // Act
        var result = await commClient.StoreStory(-1, new Dictionary<int, NexTechChallengeBackEnd.Api.Entities.Story>());

        // Assert
        Assert.True(result.id == 0);
    }

    [Fact]
    public async Task StoreStory_WithNullStoryList_ReturnsEmptyStory()
    {
        // Arrange
        HttpClient httpClient = new();
        Mock<IStoryRepository> repositoryStub = new();
        CommunicationClient commClient = new(httpClient);

        // Act
        var result = await commClient.StoreStory(0, repositoryStub.Object.GetNewestStories());

        // Assert
        Assert.True(result.id == 0);
    }
}