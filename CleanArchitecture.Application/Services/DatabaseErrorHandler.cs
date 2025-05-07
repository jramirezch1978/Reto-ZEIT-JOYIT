using System.Text.RegularExpressions;

namespace CleanArchitecture.Application.Services;

public static class DatabaseErrorHandler
{
    public static string FormatErrorMessage(string errorMessage)
    {
        if (string.IsNullOrEmpty(errorMessage))
            return "Error desconocido";

        // Reemplazar caracteres de escape
        var formattedMessage = errorMessage
            .Replace("\\n", " ")
            .Replace("\\r", " ")
            .Replace("\\\"", "\"")
            .Replace("\\t", " ")
            .Replace("\\'", "'");

        // Limpiar espacios múltiples
        formattedMessage = Regex.Replace(formattedMessage, @"\s+", " ").Trim();

        // Manejar errores específicos de PostgreSQL
        if (formattedMessage.Contains("23505")) // Violación de clave única
        {
            if (formattedMessage.Contains("partners_tax_id_key"))
                return "Ya existe un proveedor con el mismo número de RUC";
            if (formattedMessage.Contains("users_username_key"))
                return "Ya existe un usuario con el mismo nombre de usuario";
            if (formattedMessage.Contains("users_email_key"))
                return "Ya existe un usuario con el mismo correo electrónico";
        }
        else if (formattedMessage.Contains("23503")) // Violación de clave foránea
        {
            return "No se puede realizar la operación porque hay registros relacionados";
        }
        else if (formattedMessage.Contains("23502")) // Violación de NOT NULL
        {
            return "Faltan campos obligatorios en la solicitud";
        }

        // Si no es un error específico, devolver el mensaje formateado
        return formattedMessage;
    }
} 