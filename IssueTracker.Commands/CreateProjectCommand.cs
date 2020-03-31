﻿using CSharpFunctionalExtensions;
using IssueTracker.Domain;
using IssueTracker.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class CreateProjectCommand : IRequest<Result>
    {
        public CreateProjectCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            return await Project.Create(request.Name)
                .OnSuccess(async project => await _projectRepository.SaveAsync(project));
        }
    }
}
