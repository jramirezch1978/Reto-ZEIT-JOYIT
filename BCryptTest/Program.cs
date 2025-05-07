using BCrypt.Net;

// Simulamos una contraseña existente en la base de datos (usando el método antiguo)
string password = "myPassword123";
string oldHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 11);
Console.WriteLine($"Hash antiguo (como los que están en la BD): {oldHash}");

// Intentamos verificar la contraseña con el método Verify
bool isValid = BCrypt.Net.BCrypt.Verify(password, oldHash);
Console.WriteLine($"¿La contraseña se verifica correctamente con hash antiguo?: {isValid}");

// Generamos un nuevo hash con EnhancedHashPassword
string newHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 11);
Console.WriteLine($"\nNuevo hash (usando EnhancedHashPassword): {newHash}");

// Verificamos que también funciona con el nuevo hash
bool isValidNew = BCrypt.Net.BCrypt.Verify(password, newHash);
Console.WriteLine($"¿La contraseña se verifica correctamente con el nuevo hash?: {isValidNew}");

// Verificamos que el hash antiguo se puede verificar con EnhancedVerify
bool isValidEnhanced = BCrypt.Net.BCrypt.EnhancedVerify(password, oldHash);
Console.WriteLine($"\n¿La contraseña antigua se verifica con EnhancedVerify?: {isValidEnhanced}");
