using System.Reflection;
using VirtualHoftalon_Server.Exceptions;

namespace VirtualHoftalon_Server.Utils;

public class ArgumentsValidation
{
    public static void CheckIfAllPropertiesIsNull(Object dto)
    {
        PropertyInfo[] tests = dto.GetType().GetProperties();
        
        // Conta a quantidade de propriedades de um objeto, porém removendo 1,
        // pois no for abaixo, o valor sempre começa por zero
        // Ex: amountProperties = 7, mas no for começa por zero, resultando em 6
        int amountProperties = tests.Length - 1; 
        int countNullProperties = 0;
        foreach (var dtoProperty in tests)
        {
            if (dtoProperty.GetValue(dto) == null)
            {
                countNullProperties++;
            }
            else
            {
                break;
            }
            if (countNullProperties == amountProperties)
            {
                throw new PatientArgumentsInvalidException("Os argumentos da entidade não pode ser null!");

            }
            
        }
    }        
        
    
}