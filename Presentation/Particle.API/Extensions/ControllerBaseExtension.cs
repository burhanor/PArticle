using Domain.Contracts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Particle.API.Extensions
{
	public static class ControllerBaseExtension
	{
		private static async Task<IActionResult> CreateOrUpdateAsync<TRequest>(this ControllerBase controller, IMediator mediator, TRequest request)
		{
			if (request == null)
				return controller.NotFound();
			var response = await mediator.Send(request);
			if (response == null)
				return controller.NotFound();
			return controller.Ok(response);
		}

		public static async Task<IActionResult> CreateAsync<TRequest>(this ControllerBase controller, IMediator mediator, TRequest request) => await CreateOrUpdateAsync(controller, mediator, request);

		public static async Task<IActionResult> UpdateAsync<TRequest>(this ControllerBase controller, IMediator mediator, TRequest request) where TRequest : class, IEntityBase, new()
		{
			return await CreateOrUpdateAsync(controller, mediator, request);
		}
		public static async Task<IActionResult> DeleteAsync<T>(this ControllerBase controller, IMediator mediator, T request)
		{
			if (request == null)
				return controller.NotFound();
			var response = await mediator.Send(request);
			return controller.Ok(response);
		}
		public static async Task<IActionResult> GetByIdAsync<TRequest>(this ControllerBase controller, IMediator mediator, TRequest request)
		{
			if (request == null)
				return controller.NotFound();
			var response = await mediator.Send(request);
			if (response == null)
				return controller.NotFound();
			return controller.Ok(response);
		}

		public static async Task<IActionResult> GetAsync<TRequest>(this ControllerBase controller, IMediator mediator, TRequest request)
		{
			if (request == null)
				return controller.NotFound();
			return controller.Ok(await mediator.Send(request));
		}
	}
}
