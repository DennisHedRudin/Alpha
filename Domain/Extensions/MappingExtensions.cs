using System.Reflection;

namespace Domain.Extentions;

public static class MappingExtensions
{
    public static TDestination MapTo<TDestination>(this object source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        TDestination destination = Activator.CreateInstance<TDestination>()!;

        var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destinationProperties = destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach ( var destiantionProperty in destinationProperties )
        {
            var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destiantionProperty.Name && x.PropertyType == destiantionProperty.PropertyType);
            if (sourceProperty != null && destiantionProperty.CanWrite)
            {
                var value = sourceProperty.GetValue(source);
                destiantionProperty.SetValue(destination, null);
            }
        }
        return destination;
    }
}
