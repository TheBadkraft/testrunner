
namespace TestRunner.Testing;

[TestContainer]
public class CollectionTests
{
    [Test]
    public void CollectionIsEmpty()
    {
        var collection = new List<int>();
        Assert.IsEmpty(collection);
    }
    [Test]
    public void CollectionIsEmpty_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.IsEmpty(collection);
    }

    [Test]
    public void CollectionIsNotEmpty()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.IsNotEmpty(collection);
    }
    [Test]
    public void CollectionIsNotEmpty_Fail()
    {
        var collection = new List<int>();
        Assert.IsNotEmpty(collection);
    }

    [Test]
    public void CollectionContains()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.Contains(collection, (x) => x == 2);
    }
    [Test]
    public void CollectionContains_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.Contains(collection, (x) => x == 4);
    }

    [Test]
    public void CollectionDoesNotContain()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.DoesNotContain(collection, (x) => x == 4);
    }
    [Test]
    public void CollectionDoesNotContain_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.DoesNotContain(collection, (x) => x == 2);
    }

    [Test]
    public void CollectionContainsAll()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.ContainsAll(collection, new List<int> { 1, 2 });
    }
    [Test]
    public void CollectionContainsAll_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.ContainsAll(collection, new List<int> { 1, 4 });
    }

    [Test]
    public void CollectionDoesNotContainAll()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.DoesNotContainAll(collection, new List<int> { 1, 4 });
    }
    [Test]
    public void CollectionDoesNotContainAll_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.DoesNotContainAll(collection, new List<int> { 1, 2 });
    }

    [Test]
    public void CollectionContainsAny()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.ContainsAny(collection, new List<int> { 1, 4 });
    }
    [Test]
    public void CollectionContainsAny_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.ContainsAny(collection, new List<int> { 4, 5 });
    }

    [Test]
    public void CollectionDoesNotContainAny()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.DoesNotContainAny(collection, new List<int> { 4, 5 });
    }
    [Test]
    public void CollectionDoesNotContainAny_Fail()
    {
        var collection = new List<int> { 1, 2, 3 };
        Assert.DoesNotContainAny(collection, new List<int> { 1, 4 });
    }
}
