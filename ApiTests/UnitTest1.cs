using FluentAssertions;
using NUnit.Framework;

namespace ApiTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        true.Should().BeTrue();
    }
}