using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conductor.Domain.Interfaces;
using Conductor.Domain.Models;
using Microsoft.Extensions.Logging;
using SharpYaml.Serialization;

namespace Conductor.Domain.Services
{
    public class DefinitionService : IDefinitionService
    {

        private readonly IDefinitionRepository _repository;
        private readonly IWorkflowLoader _loader;
        private readonly IClusterBackplane _backplane;
        private readonly ILogger _logger;

        public DefinitionService(IDefinitionRepository repository, IWorkflowLoader loader, IClusterBackplane backplane, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _loader = loader;
            _backplane = backplane;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public void LoadDefinitionsFromStorage()
        {
            foreach (var definition in _repository.GetAll())
            {
                try
                {
                    _loader.LoadDefinition(definition);
                }
                catch (Exception ex)
                {
                    _logger.LogError(default(EventId), ex, $"Error loading definition {definition.Id}");
                }
            }
        }

        public void RegisterNewDefinition(Definition definition)
        {
            var version = _repository.GetLatestVersion(definition.Id) ?? 0;
            definition.Version = version + 1;
            _loader.LoadDefinition(definition);
            _repository.Save(definition);
            _backplane.LoadNewDefinition(definition.Id, definition.Version);
        }

        public void ReplaceVersion(Definition definition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the latest version of a definition by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Definition GetDefinition(string id)
        {
            return _repository.Find(id);
        }

        /// <summary>
        /// Gets all definition
        /// </summary>
        /// <param name="latestDefinitionVersion">true : return only latest versions of definitions - false : return all versions</param>
        /// <returns></returns>
        public IEnumerable<Definition> GetDefinition(bool latestDefinitionVersion = true)
        {
            if (latestDefinitionVersion)
            {
                var query = _repository.GetAll();
                var groups = query.GroupBy(c => c.Id);

                query = groups.SelectMany(a => a.Where(b => b.Version == a.Max(c => c.Version)));

                return query;
            }
            return _repository.GetAll();
        }

        /// <summary>
        /// removes a definition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveDefinition(string id)
        {
            return _repository.Remove(id);
        }
    }
}
