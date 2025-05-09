﻿using Asp.Versioning;
using GreenSphere.Api.Controllers.Base;
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Email.Commands.SendEmail;
using GreenSphere.Application.Features.Email.Commands.SendEmailBulk;
using GreenSphere.Application.Features.Email.Commands.SendEmailBulkWithAttachments;
using GreenSphere.Application.Features.Email.Commands.SendEmailWithAttachments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{ver:apiVersion}/emails")]
public class EmailsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("send")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> SendEmailAsync(SendEmailCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("send-with-attachment")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> SendEmailAsync([FromForm] SendEmailWithAttachmentsCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("send-bulk")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> SendEmailAsync(SendEmailBulkCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("send-bulk-with-attachments")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(SuccessResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailedResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> SendEmailAsync(
        [FromForm] SendEmailBulkWithAttachmentsCommand command)
        => CustomResult(await Mediator.Send(command));
}