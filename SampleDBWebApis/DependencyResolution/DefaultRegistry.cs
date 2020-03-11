// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SampleDBWebApis.DependencyResolution {
    using SampleDBWebApis.Services;
    using SampleDBWebApis.DataLayer;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using SampleDBWebApis.ModelBuilders;
    using SampleDBWebApis.Service;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            //var myContext = new BuildModelsService();
            For<IBuildProductsModelsServices>().Use<BuildProductsModelsServices>();
            For<IProductModelBuilders>().Use<ProductModelBuilders>();
            For<IBuildCustomersModelServices>().Use<BuildCustomersModelServices>();

            //Entity Framework Context
            //var myContext = new EntityFrameworkContext();
            //myContext.Configuration.ProxyCreationEnabled = false;
            //For<EntityFrameworkContext>().Use(myContext);
        }

        #endregion
    }
}