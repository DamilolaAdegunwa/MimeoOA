﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.EntityFrameworkCore.Repositories
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        public Type RepositoryInterface { get; private set; }

        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        public Type RepositoryImplementation { get; private set; }

        public Type RepositoryImplementationWithPrimaryKey { get; private set; }

        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation)
            //Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            //RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}
