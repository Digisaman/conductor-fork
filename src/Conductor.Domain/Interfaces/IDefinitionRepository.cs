﻿using System;
using System.Collections.Generic;
using System.Text;
using Conductor.Domain.Models;

namespace Conductor.Domain.Interfaces
{
    public interface IDefinitionRepository
    {
        IEnumerable<Definition> GetAll();

        Definition Find(string workflowId);
        Definition Find(string workflowId, int version);
        int? GetLatestVersion(string workflowId);

        void Save(Definition definition);
        bool Remove(string id);
    }
}
