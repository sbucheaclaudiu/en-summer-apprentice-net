namespace WebApplication1.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() { }
    
    public EntityNotFoundException(string messageError) : base(messageError) { }

    public EntityNotFoundException(string messageError, Exception innerException) : base(messageError, innerException) { }

    public EntityNotFoundException(int entityId, string entityName) : base(FormattableString.Invariant($"'{entityName}' with id '{entityId}' was not found!")) { }
    
    public EntityNotFoundException(string description, string entityName) : base(FormattableString.Invariant($"'{entityName}' with description '{description}' was not found!")) { }

}