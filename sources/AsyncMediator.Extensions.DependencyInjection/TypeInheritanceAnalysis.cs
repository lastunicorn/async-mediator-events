namespace AsyncMediator.Extensions.DependencyInjection;

internal class TypeInheritanceAnalysis
{
    private readonly List<Type> inheritedTypes = [];
    private readonly Type baseType;

    public Type DerivedType { get; }

    public IReadOnlyCollection<Type> InheritedTypes { get; private set; }

    public TypeInheritanceAnalysis(Type derivedType, Type baseType)
    {
        if (derivedType == null) throw new ArgumentNullException(nameof(derivedType));
        if (baseType == null) throw new ArgumentNullException(nameof(baseType));

        DerivedType = derivedType;
        this.baseType = baseType;

        if (baseType.IsInterface)
            CheckImplementedInterfaces();
        else
            CheckBaseClasses();

        InheritedTypes = inheritedTypes.AsReadOnly();
    }

    private void CheckImplementedInterfaces()
    {
        IEnumerable<Type> interfaceTypes = DerivedType.GetInterfaces()
                .Where(x => x == baseType || (x.IsGenericType && x.GetGenericTypeDefinition() == baseType));

        inheritedTypes.AddRange(interfaceTypes);
    }

    private void CheckBaseClasses()
    {
        Type derivedType = DerivedType.BaseType;

        while (derivedType != null && derivedType != typeof(object))
        {
            if (baseType.IsGenericTypeDefinition)
            {
                if (derivedType.IsGenericType && derivedType.GetGenericTypeDefinition() == baseType)
                    inheritedTypes.Add(derivedType);
            }
            else
            {
                if (derivedType == baseType)
                    inheritedTypes.Add(baseType);
            }

            derivedType = derivedType.BaseType;
        }
    }
}