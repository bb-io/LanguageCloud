using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Dtos.Vendor;
using Apps.LanguageCloud.Models.Tasks.Responses;

namespace Tests.LanguageCloud;

[TestClass]
public class TaskAssigneeResponseTests
{
    [TestMethod]
    public void Constructor_MapsUserAssignee()
    {
        var response = new TaskAssigneeResponse(new TaskAssigneeDto
        {
            Type = "user",
            User = new UserDto
            {
                Id = "user-id",
                Email = "user@example.com",
                FirstName = "First",
                LastName = "Last"
            }
        });

        Assert.AreEqual("user-id", response.AssigneeId);
        Assert.AreEqual("user@example.com", response.AssigneeEmail);
        Assert.AreEqual("First", response.AssigneeFirstName);
        Assert.AreEqual("Last", response.AssigneeLastName);
        Assert.IsNull(response.AssigneeGroupName);
    }

    [TestMethod]
    public void Constructor_MapsGroupAssignee()
    {
        var response = new TaskAssigneeResponse(new TaskAssigneeDto
        {
            Type = "group",
            Group = new GroupDto
            {
                Id = "group-id",
                Name = "Reviewers"
            }
        });

        Assert.AreEqual("group-id", response.AssigneeId);
        Assert.AreEqual("Reviewers", response.AssigneeGroupName);
        Assert.IsNull(response.AssigneeEmail);
        Assert.IsNull(response.AssigneeFirstName);
        Assert.IsNull(response.AssigneeLastName);
    }

    [TestMethod]
    public void Constructor_MapsVendorOrderTemplateAssignee()
    {
        var response = new TaskAssigneeResponse(new TaskAssigneeDto
        {
            Type = "vendorOrderTemplate",
            VendorOrderTemplate = new VendorOrderTemplateDto { Id = "template-id" }
        });

        Assert.AreEqual("template-id", response.AssigneeId);
        Assert.IsNull(response.AssigneeEmail);
        Assert.IsNull(response.AssigneeGroupName);
    }

    [TestMethod]
    public void Constructor_DoesNotThrowForUnknownAssigneeShape()
    {
        var response = new TaskAssigneeResponse(new TaskAssigneeDto { Type = "futureType" });

        Assert.AreEqual("futureType", response.AssigneeType);
        Assert.IsNull(response.AssigneeId);
    }

    [TestMethod]
    public void TaskResponse_MapsGroupAssigneeWithoutDeferredEnumeration()
    {
        var task = CreateTaskDto();
        task.Assignees =
        [
            new TaskAssigneeDto
            {
                Type = "group",
                Group = new GroupDto { Id = "group-id", Name = "Reviewers" }
            }
        ];

        var response = new TaskResponse(task, "en", "uk", "source-id", "target-id", "source.txt", "target.txt");
        var assignee = response.Assignees.Single();

        Assert.IsInstanceOfType<List<TaskAssigneeResponse>>(response.Assignees);
        Assert.AreEqual("group-id", assignee.AssigneeId);
        Assert.AreEqual("Reviewers", assignee.AssigneeGroupName);
    }

    [TestMethod]
    public void TaskResponse_MapsNullAssigneesToEmptyCollection()
    {
        var task = CreateTaskDto();
        task.Assignees = null;

        var response = new TaskResponse(task, "en", "uk", "source-id", "target-id", "source.txt", "target.txt");

        Assert.AreEqual(0, response.Assignees.Count());
    }

    private static TaskDto CreateTaskDto() => new()
    {
        Project = new ProjectDto { Id = "project-id", Name = "Project" },
        TaskType = new TaskType
        {
            Id = "task-type-id",
            Name = "Review",
            Key = "review",
            Description = "Review task"
        },
        input = new Input()
    };
}
