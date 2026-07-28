public class SampleServiceTests
{
    [Fact]
    public async Task Service_ShouldProcessDataCorrectly()
    {
        var data = new { Id = Guid.NewGuid(), Name = "Test" };
        
        data.Should().NotBeNull();
        data.Id.Should().NotBeEmpty();
        data.Name.Should().Be("Test");
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(5, 10, 15)]
    [InlineData(-1, 1, 0)]
    public void Addition_ShouldWorkCorrectly(int a, int b, int expected)
    {
        var result = a + b;
        result.Should().Be(expected);
    }
}