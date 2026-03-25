using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Models.Tasks.Requests;
using Tests.LanguageCloud.Base;

namespace Tests.LanguageCloud;

[TestClass]
public class TaskTests : TestBase
{
	private readonly TaskActions _actions;

	public TaskTests() => _actions = new TaskActions(InvocationContext);

    [TestMethod]
    public async Task GetProjectTasks_ReturnsProjectTasks()
    {
		// Arrange
		var request = new ListAllProjectTasksRequest { Project = "69c2d5babaf07a2f97e237a1" };

		// Act
		var result = await _actions.GetProjectTasks(request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GetTask_ReturnsProjectTasks()
    {
        // Arrange
        var request = new GetTaskRequest { Task = "69c2d5db16148b07b4a6a85a" };

        // Act
        var result = await _actions.GetTask(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}
