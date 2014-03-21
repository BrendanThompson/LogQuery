﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using PowerArgs;

namespace LogQuery.PowerArgs
{
    public class QueryArgs
    {
        public List<string> ReferencedAssemblies { get; set; }

        public string Usings { get; set; }
        public string Namespace { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }

        public string OrderByComment { get; set; }
        public string OrderByDescendingComment { get; set; }
        public string SkipComment { get; set; }
        public string TakeComment { get; set; }
        public string WhereComment { get; set; }

        public string ReturnType { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDescending { get; set; }
        public string Where { get; set; }

        public QueryArgs()
        {
            Usings = "";
            Namespace = "QueryArgsNamespace";
            Class = "DynamicQuery";
            Method = "RunQuery";
            Take = -1;
            Skip = -1;
            ReferencedAssemblies = new List<string>();
        }

        private string GenerateQueryCode()
        {
            var query = Resources.QueryTemplate;

            OrderByComment = string.IsNullOrEmpty(OrderBy) ? "//" : "";
            OrderByDescendingComment = string.IsNullOrEmpty(OrderByDescending) ? "//" : "";
            WhereComment = string.IsNullOrEmpty(Where) ? "//" : "";
            SkipComment = Skip < 0  ? "//" : "";
            TakeComment = Take < 0 ? "//" : "";

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                query = query.Replace("$" + prop.Name + "$", ""+prop.GetValue(this, null));
            }

            return query;
        }

        public IEnumerable RunQuery(IEnumerable src)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");

            foreach (string assembly in ReferencedAssemblies)
            {
                parameters.ReferencedAssemblies.Add(assembly);
            }

            if (Assembly.GetEntryAssembly() != null)
            {
                var programDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                parameters.ReferencedAssemblies.Add(Assembly.GetEntryAssembly().Location);
                foreach (var assembly in Directory.GetFiles(programDir))
                {
                    if (assembly.ToLower().EndsWith(".dll") || assembly.ToLower().EndsWith(".exe"))
                    {
                        parameters.ReferencedAssemblies.Add(assembly);
                    }
                }
            }

            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;

            var code = GenerateQueryCode();
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.Count > 0)
            {
                var errorString = "";
                foreach (var error in results.Errors) errorString += error + "\n";
                throw new ArgException("Could not compile your query", new Exception(errorString + "\n\n\n\n" + code));
            }

            var method = results.CompiledAssembly.GetType(Namespace + "." + Class).GetMethod(Method);
            return (IEnumerable)method.Invoke(null, new object[]{ src });
            
        }

    }

    public class Query : ArgHook
    {
        public Type DataSourceType { get; set; }

        string[] referencedAssemblies;
        public Query(Type dataSourceType, params string[] referencedAssemblies)
        {
            this.DataSourceType = dataSourceType;
            this.referencedAssemblies = referencedAssemblies;
        }

        public override void AfterPopulateProperties(HookContext context)
        {
            var dataSource = Activator.CreateInstance(DataSourceType);
            var dataSourceCollectionProperty = DataSourceType.GetProperty(context.Property.Name);
            IEnumerable dataSourceCollection = (IEnumerable)dataSourceCollectionProperty.GetValue(dataSource, null);

            QueryArgs queryArgs = new QueryArgs()
            {
                ReturnType = dataSourceCollectionProperty.PropertyType.GetGenericArguments()[0].FullName,
            };

            var argObject = context.Args;
            var argType = context.Property.DeclaringType;

            if (argType.GetProperty("Skip") != null)
            {
                queryArgs.Skip = (int)argType.GetProperty("Skip").GetValue(argObject, null);
            }

            if (argType.GetProperty("Take") != null)
            {
                var take = (int)argType.GetProperty("Take").GetValue(argObject, null);
                if (take > 0) queryArgs.Take = take;
            }

            if (argType.GetProperty("Where") != null)
            {
                queryArgs.Where = argType.GetProperty("Where").GetValue(argObject, null) as string;
            }

            if (argType.GetProperty("OrderBy") != null)
            {
                var orderProp = argType.GetProperty("OrderBy").GetValue(argObject, null) as string;
                if (string.IsNullOrEmpty(orderProp) == false)
                {
                    queryArgs.OrderBy = "item => " + orderProp;
                }
            }

            if (argType.GetProperty("OrderByDescending") != null)
            {
                var orderProp = argType.GetProperty("OrderByDescending").GetValue(argObject, null) as string;
                if (string.IsNullOrEmpty(orderProp) == false)
                {
                    queryArgs.OrderByDescending = "item => " + orderProp;
                }
            }

            if (referencedAssemblies != null)
            {
                queryArgs.ReferencedAssemblies.AddRange(referencedAssemblies);
            }
            var results = queryArgs.RunQuery(dataSourceCollection);
            context.Property.SetValue(context.Args, results, null);
        }
    }
}
