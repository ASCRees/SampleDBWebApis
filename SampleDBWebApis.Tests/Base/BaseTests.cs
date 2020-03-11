using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDBWebApis.Tests.Base
{
    public class BaseTests
    {
        [SetUp]
        public virtual void Setup()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory + "App_Data\\");
        }
    }
}
