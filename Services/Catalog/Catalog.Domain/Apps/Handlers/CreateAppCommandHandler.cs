using Catalog.Domain.Apps.Commands;
using Catalog.Domain.Apps.Models;
using Catalog.Domain.Apps.Repositories;
using Catalog.Domain.Authors.Repositories;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Domain.Apps.Handlers
{
    public class CreateAppCommandHandler : IRequestHandler<CreateAppCommand, Response<App>>
    {
        private IAppRepository AppRepository { get; }

        private IAuthorRepository AuthorRepository { get; }

        public CreateAppCommandHandler(IAppRepository appRepository, IAuthorRepository authorRepository)
        {
            AppRepository = appRepository ?? throw new ArgumentNullException(nameof(appRepository));
            AuthorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<Response<App>> Handle(CreateAppCommand command, CancellationToken cancellationToken)
        {
            var response = Response<App>.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return response.WithMessages(validateCommandResponse.Messages);

            var authorResponse = await AuthorRepository.FindAsync(command.AuthorID);

            if (!authorResponse.HasValue)
                return response.WithBusinessError(nameof(App.Author), $"{nameof(App.Author)} not found.");

            var app = new App(command.Name, command.Title, command.Size, command.Version, command.ExternalId, command.ReleaseDate,
                command.AppLogo, command.LanguageList, command.Price, authorResponse);

            await AppRepository.AddAsync(app);

            return response.SetValue(app);
        }
    }
}
