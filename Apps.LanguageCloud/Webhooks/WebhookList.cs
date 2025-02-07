﻿using Apps.LanguageCloud.Actions;
using Apps.LanguageCloud.Dtos;
using Apps.LanguageCloud.Models;
using Apps.LanguageCloud.Models.Accounts;
using Apps.LanguageCloud.Models.Files.Requests;
using Apps.LanguageCloud.Models.Projects.Requests;
using Apps.LanguageCloud.Models.Tasks.Requests;
using Apps.LanguageCloud.Webhooks.Payload;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Webhooks;

[WebhookList]
public class WebhookList(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    #region ProjectWebhooks

    [Webhook("On project created", Description = "On project created")]
    public async Task<WebhookResponse<ProjectDto>> ProjectCreation(WebhookRequest webhookRequest,
        [WebhookParameter] AccountOptionalRequest request)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectEvent>>(webhookRequest.Body.ToString()!);
        if(data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (!string.IsNullOrEmpty(request.AccountId) && data.AccountId != request.AccountId)
        {
            return new WebhookResponse<ProjectDto>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }

        var actions = new ProjectActions(InvocationContext);
        var project = await actions.GetProject(new GetProjectRequest { Project = data.Data.Id });

        return new WebhookResponse<ProjectDto>
        {
            HttpResponseMessage = null,
            Result = project
        };
    }

    [Webhook("On project updated", Description = "On project updated")]
    public async Task<WebhookResponse<ProjectDto>> ProjectUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] AccountOptionalRequest request,
        [WebhookParameter] GetProjectOptionalRequest projectRequest,
        [WebhookParameter] OptionalProjectStatusRequest statusRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectEvent>>(webhookRequest.Body.ToString()!);
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (!string.IsNullOrEmpty(request.AccountId) && data.AccountId != request.AccountId)
        {
            return new WebhookResponse<ProjectDto>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        if(projectRequest.ProjectId != null && data.Data.Id != projectRequest.ProjectId)
        {
            return new WebhookResponse<ProjectDto>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }

        var actions = new ProjectActions(InvocationContext);
        var project = await actions.GetProject(new GetProjectRequest { Project = data.Data.Id });

        if (statusRequest.Status != null && project.Status != statusRequest.Status)
        {
            return new WebhookResponse<ProjectDto>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }

        return new WebhookResponse<ProjectDto>
        {
            HttpResponseMessage = null,
            Result = project
        };
    }

    [Webhook("On project deleted", Description = "On project deleted")]
    public async Task<WebhookResponse<ProjectEvent>> ProjectDeleted(WebhookRequest webhookRequest,
        [WebhookParameter] AccountOptionalRequest request,
        [WebhookParameter] GetProjectOptionalRequest projectRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectEvent>>(webhookRequest.Body.ToString()!);
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (!string.IsNullOrEmpty(request.AccountId) && data.AccountId != request.AccountId)
        {
            return new WebhookResponse<ProjectEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }

        if (projectRequest.ProjectId != null && data.Data.Id != projectRequest.ProjectId)
        {
            return new WebhookResponse<ProjectEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }

        return new WebhookResponse<ProjectEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    #endregion

    #region TaskWebhooks
    
    [Webhook("On task accepted", Description = "On task accepted")]
    public async Task<WebhookResponse<TaskEvent>> TaskAccepted(WebhookRequest webhookRequest,
        [WebhookParameter] GetTaskOptionalRequest taskOptionalRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (taskOptionalRequest.TaskId != null && data.Data.id != taskOptionalRequest.TaskId)
        {
            return new WebhookResponse<TaskEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        return new WebhookResponse<TaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On task created", Description = "On task created")]
    public async Task<WebhookResponse<TaskEvent>> TaskCreated(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<TaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On task completed", Description = "On task completed")]
    public async Task<WebhookResponse<TaskEvent>> TaskCompleted(WebhookRequest webhookRequest,
        [WebhookParameter] GetTaskOptionalRequest taskOptionalRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (taskOptionalRequest.TaskId != null && data.Data.id != taskOptionalRequest.TaskId)
        {
            return new WebhookResponse<TaskEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        return new WebhookResponse<TaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On task updated", Description = "On task updated")]
    public async Task<WebhookResponse<TaskEvent>> TaskUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] GetTaskOptionalRequest taskOptionalRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (taskOptionalRequest.TaskId != null && data.Data.id != taskOptionalRequest.TaskId)
        {
            return new WebhookResponse<TaskEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        return new WebhookResponse<TaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On task deleted", Description = "On task deleted")]
    public async Task<WebhookResponse<TaskEvent>> TaskDeleted(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<TaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }
    #endregion

    #region ProjectTemplateWebhooks

    [Webhook("On project template created", Description = "On project template created")]
    public async Task<WebhookResponse<ProjectTemplateEvent>> ProjectTemplateCreation(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectTemplateEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        return new WebhookResponse<ProjectTemplateEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On project template updated", Description = "On project template updated")]
    public async Task<WebhookResponse<ProjectTemplateEvent>> ProjectTemplateUpdated(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectTemplateEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<ProjectTemplateEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On project template deleted", Description = "On project template deleted")]
    public async Task<WebhookResponse<ProjectTemplateEvent>> ProjectTemplateDeleted(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectTemplateEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<ProjectTemplateEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }
    #endregion

    #region SourceFileWebhooks

    [Webhook("On source file created", Description = "On source file created")]
    public async Task<WebhookResponse<SourceFileEvent>> SourceFileCreation(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<SourceFileEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<SourceFileEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On source file updated", Description = "On source file updated")]
    public async Task<WebhookResponse<SourceFileEvent>> SourceFileUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] GetFileOptionalRequest sourceFileOptionalRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<SourceFileEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (sourceFileOptionalRequest.FileId != null && data.Data.Id != sourceFileOptionalRequest.FileId)
        {
            return new WebhookResponse<SourceFileEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        if (sourceFileOptionalRequest.ProjectId != null && data.Data.Project.Id != sourceFileOptionalRequest.ProjectId)
        {
            return new WebhookResponse<SourceFileEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        return new WebhookResponse<SourceFileEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On source file deleted", Description = "On source file deleted")]
    public async Task<WebhookResponse<SourceFileEvent>> SourceFileDeleted(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<SourceFileEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<SourceFileEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }
    #endregion

    #region TargetFileWebhooks

    [Webhook("On target file created", Description = "On target file created")]
    public async Task<WebhookResponse<TargetFileEvent>> TargetFileCreation(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TargetFileEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        return new WebhookResponse<TargetFileEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On target file updated", Description = "On target file updated")]
    public async Task<WebhookResponse<TargetFileEvent>> TargetFileUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TargetFileEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        if (fileOptionalRequest.FileId != null && data.Data.Id != fileOptionalRequest.FileId)
        {
            return new WebhookResponse<TargetFileEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        if (fileOptionalRequest.ProjectId != null && data.Data.Project.Id != fileOptionalRequest.ProjectId)
        {
            return new WebhookResponse<TargetFileEvent>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
        
        return new WebhookResponse<TargetFileEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On target file deleted", Description = "On target file deleted")]
    public async Task<WebhookResponse<TargetFileEvent>> TargetFileDeleted(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<TargetFileEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        return new WebhookResponse<TargetFileEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }
    #endregion

    #region ErrorTaskWebhooks

    [Webhook("On error task created", Description = "On error task created")]
    public async Task<WebhookResponse<ErrorTaskEvent>> ErrorTaskCreation(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ErrorTaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        return new WebhookResponse<ErrorTaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On error task updated", Description = "On error task updated")]
    public async Task<WebhookResponse<ErrorTaskEvent>> ErrorTaskUpdated(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ErrorTaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<ErrorTaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    [Webhook("On error task deleted", Description = "On error task deleted")]
    public async Task<WebhookResponse<ErrorTaskEvent>> ErrorTaskDeleted(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ErrorTaskEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        return new WebhookResponse<ErrorTaskEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }
    #endregion

    #region GroupProjectWebhooks

    [Webhook("On group project membership changed", Description = "On group project membership changed")]
    public async Task<WebhookResponse<ProjectGroupEvent>> MembershipChanged(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<WebhookPayloadWrapper<ProjectGroupEvent>>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }
        
        return new WebhookResponse<ProjectGroupEvent>
        {
            HttpResponseMessage = null,
            Result = data.Data
        };
    }

    #endregion
}