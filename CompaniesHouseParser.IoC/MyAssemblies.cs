using System.Reflection;

namespace CompaniesHouseParser.IoC;

public static class MyAssemblies
{
    public static IList<Assembly> GetFiltredAssemlies(IList<Assembly> assemblies)
    {
        var filtredAssemblies = new List<Assembly>();
        foreach (var assembly in assemblies)
        {
            if (assembly.FullName == null)
            {
                continue;
            }
            if (assembly.FullName.Contains("CompaniesHouseParser"))
            {
                filtredAssemblies.Add(assembly);
            }
        }
        return filtredAssemblies;
    }
    public static IList<Assembly> GetAllAssemblies()
    {
        var returnAssemblies = new List<Assembly>();
        var loadedAssemblies = new HashSet<string>();
        var assembliesToCheck = new Queue<Assembly>();

        var entryAssembly = Assembly.GetEntryAssembly();
        assembliesToCheck.Enqueue(entryAssembly);

        while (assembliesToCheck.Any())
        {
            var assemblyToCheck = assembliesToCheck.Dequeue();

            foreach (var reference in assemblyToCheck.GetReferencedAssemblies())
            {
                if (!loadedAssemblies.Contains(reference.FullName))
                {
                    var assembly = Assembly.Load(reference);
                    assembliesToCheck.Enqueue(assembly);
                    loadedAssemblies.Add(reference.FullName);
                    returnAssemblies.Add(assembly);
                }
            }
        }

        return returnAssemblies;
    }
}
