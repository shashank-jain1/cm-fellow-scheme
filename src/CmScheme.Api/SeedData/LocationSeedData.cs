using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Api.SeedData;

public static class LocationSeedData
{
    public static async Task SeedAsync(IMastersCommandDbContext dbContext)
    {
        int districtCount = await dbContext.Districts.CountAsync();
        if (districtCount >= 52)
        {
            return;
        }

        if (districtCount > 0)
        {
            dbContext.Blocks.RemoveRange(await dbContext.Blocks.ToListAsync());
            dbContext.Districts.RemoveRange(await dbContext.Districts.ToListAsync());
            dbContext.Divisions.RemoveRange(await dbContext.Divisions.ToListAsync());
            dbContext.States.RemoveRange(await dbContext.States.ToListAsync());
            await dbContext.SaveChangesAsync();
        }

        State mp = new() { StateName = "Madhya Pradesh", StateCode = "MP", StateShortName = "MP", IsActive = true };
        dbContext.States.Add(mp);
        await dbContext.SaveChangesAsync();

        Dictionary<string, int> divisionIds = await SeedDivisions(dbContext, mp.StateId);
        await SeedDistricts(dbContext, divisionIds);
    }

    private static async Task<Dictionary<string, int>> SeedDivisions(IMastersCommandDbContext db, int stateId)
    {
        (string name, string code)[] divisions =
        [
            ("Bhopal", "BPL"),
            ("Chambal", "CHB"),
            ("Gwalior", "GWL"),
            ("Jabalpur", "JBP"),
            ("Narmadapuram", "NPD"),
            ("Rewa", "RWA"),
            ("Sagar", "SGR"),
            ("Ujjain", "UJN"),
        ];

        Dictionary<string, int> ids = [];
        foreach (var (name, code) in divisions)
        {
            Division d = new() { StateId = stateId, DivisionName = name, DivisionCode = code, IsActive = true };
            db.Divisions.Add(d);
            await db.SaveChangesAsync();
            ids[name] = d.DivisionId;
        }
        return ids;
    }

    private static async Task SeedDistricts(IMastersCommandDbContext db, Dictionary<string, int> divisionIds)
    {
        (string division, string name, string code, string[] blocks)[] districts =
        [
            // ── Bhopal Division (5) ──
            ("Bhopal", "Bhopal", "BPL", ["Bhopal", "Huzur", "Phanda", "Kolar"]),
            ("Bhopal", "Raisen", "RSN", ["Raisen", "Sultanpur", "Begamganj", "Goharganj"]),
            ("Bhopal", "Rajgarh", "RJG", ["Rajgarh", "Khilchipur", "Sarangpur", "Biaora"]),
            ("Bhopal", "Sehore", "SHR", ["Sehore", "Ashta", "Budhni", "Ichhawar"]),
            ("Bhopal", "Vidisha", "VDS", ["Vidisha", "Basoda", "Sironj", "Kurwai"]),

            // ── Chambal Division (4) ──
            ("Chambal", "Morena", "MRE", ["Morena", "Sabalgarh", "Porsa", "Kailaras"]),
            ("Chambal", "Bhind", "BND", ["Bhind", "Ater", "Gohad", "Mehgaon"]),
            ("Chambal", "Sheopur", "SHP", ["Sheopur", "Vijaypur", "Karahal"]),
            ("Chambal", "Datia", "DTA", ["Datia", "Seondha", "Bhander", "Indergarh"]),

            // ── Gwalior Division (3) ──
            ("Gwalior", "Gwalior", "GWL", ["Gwalior", "Dabra", "Bhitarwar", "Chinour"]),
            ("Gwalior", "Shivpuri", "SVP", ["Shivpuri", "Pichor", "Kolaras", "Badarwas"]),
            ("Gwalior", "Ashoknagar", "ASK", ["Ashoknagar", "Mungaoli", "Isagarh", "Chanderi"]),

            // ── Jabalpur Division (8) ──
            ("Jabalpur", "Jabalpur", "JBP", ["Jabalpur", "Panagar", "Mandla Road", "Patpan"]),
            ("Jabalpur", "Katni", "KTN", ["Katni", "Murwara", "Barhi", "Bahoriband"]),
            ("Jabalpur", "Narsinghpur", "NSP", ["Narsinghpur", "Gadarwara", "Tendukheda", "Kareli"]),
            ("Jabalpur", "Dindori", "DND", ["Dindori", "Shahpura", "Bajag", "Adegaon"]),
            ("Jabalpur", "Mandla", "MDL", ["Mandla", "Nainpur", "Niwas", "Bichhiya"]),
            ("Jabalpur", "Seoni", "SEI", ["Seoni", "Lakhnadon", "Khapa", "Chhindwara Road"]),
            ("Jabalpur", "Chhindwara", "CHW", ["Chhindwara", "Parasia", "Amarwara", "Saunsar"]),
            ("Jabalpur", "Balaghat", "BLG", ["Balaghat", "Lanji", "Katangi", "Birsa"]),

            // ── Narmadapuram Division (4) ──
            ("Narmadapuram", "Narmadapuram", "NPD", ["Narmadapuram", "Itarsi", "Seoni Malwa", "Timarni"]),
            ("Narmadapuram", "Betul", "BTL", ["Betul", "Amla", "Multai", "Bhainsdehi"]),
            ("Narmadapuram", "Harda", "HDA", ["Harda", "Timarni", "Rampur"]),
            ("Narmadapuram", "Hoshangabad", "HSG", ["Hoshangabad", "Itarsi", "Seoni Malwa"]),

            // ── Rewa Division (7) ──
            ("Rewa", "Rewa", "RWA", ["Rewa", "Mauganj", "Teonthar", "Hanumana"]),
            ("Rewa", "Satna", "STN", ["Satna", "Maihar", "Amarpatan", "Pawai"]),
            ("Rewa", "Sidhi", "SDH", ["Sidhi", "Majhauli", "Kusmi", "Churhat"]),
            ("Rewa", "Singrauli", "SNL", ["Singrauli", "Waidhan", "Mada", "Chitrangi"]),
            ("Rewa", "Shahdol", "SHL", ["Shahdol", "Burhar", "Sojana", "Amlengo"]),
            ("Rewa", "Umaria", "UMR", ["Umaria", "Manpur", "Nowrozabad", "Bandhavgarh"]),
            ("Rewa", "Anuppur", "ANP", ["Anuppur", "Kotma", "Pushprajgarh", "Jaithari"]),

            // ── Sagar Division (6) ──
            ("Sagar", "Sagar", "SGR", ["Sagar", "Banda", "Khurai", "Rehli"]),
            ("Sagar", "Damoh", "DMH", ["Damoh", "Hatta", "Patan", "Tendukheda"]),
            ("Sagar", "Panna", "PNA", ["Panna", "Ajaigarh", "Gunour", "Shahnagar"]),
            ("Sagar", "Chhatarpur", "CTP", ["Chhatarpur", "Badausa", "Laundi", "Bijawar"]),
            ("Sagar", "Tikamgarh", "TKG", ["Tikamgarh", "Prithvipur", "Niwari", "Orchha"]),
            ("Sagar", "Niwari", "NWR", ["Niwari", "Prithvipur", "Orchha"]),

            // ── Ujjain Division (15) ──
            ("Ujjain", "Ujjain", "UJN", ["Ujjain", "Mahidpur", "Tarana", "Badnagar"]),
            ("Ujjain", "Indore", "IND", ["Indore", "Mhow", "Sanwer", "Depalpur"]),
            ("Ujjain", "Dhar", "DHR", ["Dhar", "Badnawar", "Manawar", "Dharampuri"]),
            ("Ujjain", "Jhabua", "JHB", ["Jhabua", "Ranapur", "Thandla", "Petlawad"]),
            ("Ujjain", "Alirajpur", "ALR", ["Alirajpur", "Jobat", "Bhabra"]),
            ("Ujjain", "Barwani", "BRI", ["Barwani", "Rajpur", "Sendhwa", "Pansemal"]),
            ("Ujjain", "Burhanpur", "BUP", ["Burhanpur", "Nepanagar", "Shahpur"]),
            ("Ujjain", "Khargone", "KGN", ["Khargone", "Segaon", "Bhagwanpura", "Kasrawad"]),
            ("Ujjain", "Khandwa", "KHA", ["Khandwa", "Pandhana", "Harsud", "Omkareshwar"]),
            ("Ujjain", "Dewas", "DWS", ["Dewas", "Sonkatch", "Khanpur", "Bagli"]),
            ("Ujjain", "Shajapur", "SJP", ["Shajapur", "Shujalpur", "Arona", "Kalapipal"]),
            ("Ujjain", "Ratlam", "RTM", ["Ratlam", "Sailana", "Bajna", "Piploda"]),
            ("Ujjain", "Mandsaur", "MDS", ["Mandsaur", "Daloda", "Garoth", "Bhanpura"]),
            ("Ujjain", "Neemuch", "NEM", ["Neemuch", "Jiran", "Manasa", "Bharpur"]),
            ("Ujjain", "Agar Malwa", "AGM", ["Agar", "Barod", "Susner", "Nalkheda"]),
        ];

        foreach (var (division, name, code, blocks) in districts)
        {
            if (!divisionIds.TryGetValue(division, out int divId))
            {
                continue;
            }

            District d = new() { DivisionId = divId, DistrictName = name, DistrictCode = code, IsActive = true };
            db.Districts.Add(d);
            await db.SaveChangesAsync();

            foreach (string blockName in blocks)
            {
                Block b = new()
                {
                    DistrictId = d.DistrictId,
                    BlockName = blockName,
                    BlockCode = blockName[..Math.Min(3, blockName.Length)].ToUpper(),
                    IsActive = true,
                };
                db.Blocks.Add(b);
            }
            await db.SaveChangesAsync();
        }
    }
}
