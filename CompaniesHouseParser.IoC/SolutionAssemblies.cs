using System.Reflection;

namespace CompaniesHouseParser.IoC;

public static class SolutionAssemblies
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
            if (assembly.FullName.Contains(GetCurrentAssemlyName()))
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

    private static string GetCurrentAssemlyName()
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();
        string assamblyName = currentAssembly.GetName().Name;
        int index = assamblyName.IndexOf('.');

        if (index > 0)
        {
            assamblyName = assamblyName.Substring(0, index);
        }
        return assamblyName;
    }
}
