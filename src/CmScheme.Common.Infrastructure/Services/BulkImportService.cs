using System.Security.Cryptography;
using System.Text;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class BulkImportService(IRegistrationCommandDbContext dbContext) : IBulkImportService
{
    public async Task<int> ImportUsersFromCsvAsync(Stream csvStream, CancellationToken ct = default)
    {
        using StreamReader reader = new StreamReader(csvStream, Encoding.UTF8, leaveOpen: true);

        string? headerLine = await reader.ReadLineAsync(ct);
        if (string.IsNullOrWhiteSpace(headerLine))
        {
            return 0;
        }

        string[] headers = ParseCsvLine(headerLine);
        int nameIdx = Array.FindIndex(headers, h => h.Equals("Name", StringComparison.OrdinalIgnoreCase));
        int emailIdx = Array.FindIndex(headers, h => h.Equals("Email", StringComparison.OrdinalIgnoreCase));
        int phoneIdx = Array.FindIndex(headers, h => h.Equals("Phone", StringComparison.OrdinalIgnoreCase));
        int roleIdx = Array.FindIndex(headers, h => h.Equals("Role", StringComparison.OrdinalIgnoreCase));

        if (nameIdx < 0 || emailIdx < 0 || phoneIdx < 0 || roleIdx < 0)
        {
            throw new FormatException("CSV must contain Name, Email, Phone, and Role columns.");
        }

        int importedCount = 0;

        while (true)
        {
            string? line = await reader.ReadLineAsync(ct);
            if (line is null)
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] fields = ParseCsvLine(line);
            if (fields.Length <= Math.Max(nameIdx, Math.Max(emailIdx, Math.Max(phoneIdx, roleIdx))))
            {
                continue;
            }

            string name = fields[nameIdx].Trim();
            string email = fields[emailIdx].Trim();
            string phone = fields[phoneIdx].Trim();
            string role = fields[roleIdx].Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                continue;
            }

            string[] nameParts = name.Split(' ', 2);
            string firstName = nameParts[0];
            string lastName = nameParts.Length > 1 ? nameParts[1] : nameParts[0];

            Applicant applicant = new Applicant
            {
                FirstName = firstName,
                LastName = lastName,
                FatherName = "N/A",
                EmailId = email,
                MobileNumber = phone,
                DateOfBirth = new DateTime(1990, 1, 1),
                PermanentAddress = "N/A",
                PinCode = "000000",
                BoardUniversityName = "N/A",
                PassingYear = 2020,
                PercentageCGPA = 0,
                Status = "Approved",
                CreatedOn = DateTime.UtcNow,
            };

            dbContext.Applicants.Add(applicant);
            await dbContext.SaveChangesAsync(ct);

            string passwordHash = HashPassword("Default@123");

            UserAccount userAccount = new UserAccount
            {
                ApplicantId = applicant.ApplicantId,
                Username = email,
                PasswordHash = passwordHash,
                Role = role,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
            };

            dbContext.UserAccounts.Add(userAccount);
            await dbContext.SaveChangesAsync(ct);

            importedCount++;
        }

        return importedCount;
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);
        return Convert.ToBase64String(hashBytes);
    }

    private static string[] ParseCsvLine(string line)
    {
        List<string> fields = [];
        bool inQuotes = false;
        StringBuilder current = new();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        fields.Add(current.ToString());
        return fields.ToArray();
    }
}
