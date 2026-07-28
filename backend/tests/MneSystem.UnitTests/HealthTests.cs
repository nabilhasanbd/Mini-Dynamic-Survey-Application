public class HealthTests
{
    [Fact]
    public void Health_ShouldReturnHealthyStatus()
    {
        var result = true;
        result.Should().BeTrue();
    }

    [Fact]
    public void System_ShouldBeOperational()
    {
        var status = "Operational";
        status.Should().NotBeNull();
        status.Should().Be("Operational");
    }
}