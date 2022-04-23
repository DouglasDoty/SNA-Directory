using MongoDB.Driver;

namespace SNA_Directory.Data.DataAccess;

public class SNAData : ISNAData
{
    private readonly IMongoCollection<SNAModel> _SNAs;
    private readonly IMongoCollection<Biome> _Biomes;
    private readonly IMongoCollection<CommentModel> _Comments;
    public bool DatabaseInvalid { get; private set; }

    public SNAData(IDbConnection db)
    {
        _SNAs = db.SNACollection;
        _Biomes = db.BiomeCollection;
        _Comments = db.CommentCollection;
        try
        {
            if (_SNAs.EstimatedDocumentCount() == 0)
            {
                InitializeDatabase();
            }
            DatabaseInvalid = false;
        }
        catch (Exception)
        {
            DatabaseInvalid = true;
        }
     }

    public async Task<List<SNAModel>> GetAllSNAsAsync()
    {
        if (DatabaseInvalid) return null;
        var results = await _SNAs.FindAsync(_ => true);
        return results.ToList();
    }

    public async Task<SNAModel> GetSnaAsync(string id)
    {
        if (DatabaseInvalid) return null;
        var results = await _SNAs.FindAsync(s => s.Id == id);
        return results.FirstOrDefault();
    }

    public async Task<List<SNAModel>> GetProvinceSNAsAsync(string biome)
    {
        if (DatabaseInvalid) return null;
        var results = await _SNAs.FindAsync(p => p.Biome == biome);
        return results.ToList();
    }

    public async Task<List<Biome>> GetAllBiomesAsync()
    {
        if (DatabaseInvalid) return null;
        var results = await _Biomes.FindAsync(_ => true);
        return results.ToList();
    }

    public Task CreateSNA(SNAModel sna)
    {
        return _SNAs.InsertOneAsync(sna);
    }

    public Task CreateMultipleSNAs(IEnumerable<SNAModel> snas)
    {
        return _SNAs.InsertManyAsync(snas);
    }

    public Task UpdateSNA(SNAModel sna)
    {
        var filter = Builders<SNAModel>.Filter.Eq("Id", sna.Id);
        return _SNAs.ReplaceOneAsync(filter, sna, new ReplaceOptions { IsUpsert = true });

    }

    public async Task<List<CommentModel>> GetComments(int snaId)
    {
        if (DatabaseInvalid) return null;
        var results = await _Comments.FindAsync(c => c.DnrId == snaId);
        return results.ToList();
    }

    public Task CreateComment(CommentModel comment)
    {
        return _Comments.InsertOneAsync(comment);
    }

    public void InitializeDatabase()
    {
        List<SNAModel> snas = new()
        {
            new SNAModel { DNRId = 00972, Name = "Agassiz Dunes", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 02069, Name = "Antelope Valley", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 02037, Name = "Avon Hills Forest", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new SNAModel { DNRId = 02066, Name = "Badoura Jack Pine Woodland", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new SNAModel { DNRId = 01086, Name = "Bald Eagle Bluff", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 01078, Name = "Big Island", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new SNAModel { DNRId = 01035, Name = "Black Lake Bog", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 02041, Name = "Blaine Airport Rich Fen", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 02036, Name = "Blaine Preserve", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 02027, Name = "Blanket Flower Prairie", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new SNAModel { DNRId = 01050, Name = "Blue Devil Valley", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00996, Name = "Bluestem Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 02059, Name = "Boltuck-Rice Forever Wild", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new SNAModel { DNRId = 01043, Name = "Bonanza Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00967, Name = "Boot Lake", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 01005, Name = "Botany Bog", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new SNAModel { DNRId = 02067, Name = "Brownsville Bluff", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 01044, Name = "Bruce Hitman Heron Rookery", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01049, Name = "Burntside Islands", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new SNAModel { DNRId = 02043, Name = "Butternut Valley Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01045, Name = "Butterwort Cliffs", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 01014, Name = "Caldwell Brook Cedar Swamp", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 01002, Name = "Cannon River Trout Lily", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01030, Name = "Cannon River Turtle Preserve", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 02034, Name = "Cedar Mountain", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 02062, Name = "Cedar Rock", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01034, Name = "Chamberlain Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 02022, Name = "Cherry Grove Blind Valley", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new SNAModel { DNRId = 02040, Name = "Chimney Rock ", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new SNAModel { DNRId = 02028, Name = "Chisholm Point Island", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new SNAModel { DNRId = 00978, Name = "Clear Lake", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 02048, Name = "Clinton Falls Dwarf Trout Lily", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 01046, Name = "Clinton Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00964, Name = "Cold Spring Heron Colony", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new SNAModel { DNRId = 01053, Name = "Compass Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 01054, Name = "Cottonwood River Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 02068, Name = "Crystal Spring", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 01079, Name = "Des Moines River", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 02049, Name = "Dinner Creek", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new SNAModel { DNRId = 01052, Name = "Eagles Nest Island No. 4", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new SNAModel { DNRId = 02002, Name = "East Rat Root River Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 00974, Name = "Egret Island", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00984, Name = "Englund Ecotone ", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 01057, Name = "Falls Creek", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 00955, Name = "Felton Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 02045, Name = "Franconia Bluffs", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 00971, Name = "Frenchman's Bluff", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 01103, Name = "Glynn Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 01071, Name = "Gneiss Outcrops", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00977, Name = "Greenwater Lake", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new SNAModel { DNRId = 01101, Name = "Grey Cloud Dunes", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 01084, Name = "Gully Fen", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new SNAModel { DNRId = 01068, Name = "Gustafson's Camp", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 02017, Name = "Harry W. Cater Homestead Prairie", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 02042, Name = "Hastings Sand Coulee", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 00954, Name = "Hastings", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 00962, Name = "Helen Allison Savanna", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 01013, Name = "Hemlock Ravine", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 02001, Name = "Hole in the Bog Peatland", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new SNAModel { DNRId = 02016, Name = "Holthe Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 01047, Name = "Hovland Woods", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new SNAModel { DNRId = 01038, Name = "Hythecker Prairie", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 01000, Name = "Iona's Beach", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 01018, Name = "Iron Horse Prairie", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 00966, Name = "Iron Springs Bog", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new SNAModel { DNRId = 00958, Name = "Itasca Wilderness Sanctuary", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new SNAModel { DNRId = 01056, Name = "Joseph A. Tauer Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00961, Name = "Kasota Prairie", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01074, Name = "Kawishiwi Pines", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new SNAModel { DNRId = 00979, Name = "Kellogg Weaver Dunes", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 00950, Name = "Kettle River", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 01037, Name = "King's and Queen's Bluffs", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 02056, Name = "La Salle Lake", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new SNAModel { DNRId = 01023, Name = "Ladies Tresses Swamp", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new SNAModel { DNRId = 01094, Name = "Lake Alexander Woods", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new SNAModel { DNRId = 01075, Name = "Lake Bronson Parkland", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new SNAModel { DNRId = 02044, Name = "Langhei Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 02065, Name = "Lawrence Creek", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 02046, Name = "Lester Lake", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new SNAModel { DNRId = 02054, Name = "Little Too Much Lake", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new SNAModel { DNRId = 01063, Name = "Lost 40", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new SNAModel { DNRId = 01072, Name = "Lost Lake Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 02003, Name = "Lost River Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 01041, Name = "Lost Valley Prairie", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 01077, Name = "Lundblad Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 01070, Name = "Lutsen", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 02004, Name = "Luxemberg Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 00968, Name = "Malmberg Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 01022, Name = "Mary Schmidt Crawford Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01033, Name = "McGregor Marsh", Biome = "Coniferous Forest", Landscape = "Tamarack Lowlands" },
            new SNAModel { DNRId = 02060, Name = "Mille Lacs Moraine", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 02000, Name = "Minnesota Point Pine Forest", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 02070, Name = "Mississippi Oxbow", Biome = "Coniferous Forest", Landscape = "Tamarack Lowlands" },
            new SNAModel { DNRId = 00980, Name = "Mississippi River Islands", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 01017, Name = "Moose Mountain", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 02051, Name = "Morton Outcrops", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01059, Name = "Mound Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 02026, Name = "Mound Spring Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 01073, Name = "Mulligan Lake Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 02032, Name = "Myhr Creek Ridge", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 02005, Name = "Myrtle Lake Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 02006, Name = "Nett Lake Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 02007, Name = "Norris Camp Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 01098, Name = "North Black River Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 01081, Name = "North Fork Zumbro Woods", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new SNAModel { DNRId = 01095, Name = "Oronoco Prairie", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new SNAModel { DNRId = 01031, Name = "Osmundson Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01040, Name = "Otter Tail Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 00960, Name = "Partch Woods", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new SNAModel { DNRId = 00988, Name = "Pembina Trail", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new SNAModel { DNRId = 00957, Name = "Pennington Bog", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new SNAModel { DNRId = 01009, Name = "Pigs Eye Island Heron Rookery", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 02021, Name = "Pin Oak Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 01007, Name = "Pine and Curry Island", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 02030, Name = "Pine Bend Bluffs", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 02008, Name = "Pine Creek Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 02055, Name = "Potato Lake", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 01036, Name = "Prairie Bush Clover", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new SNAModel { DNRId = 01026, Name = "Prairie Coteau", Biome = "Prairie Grasslands", Landscape = "Inner Coteau" },
            new SNAModel { DNRId = 01062, Name = "Prairie Creek Woods", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 01067, Name = "Prairie Smoke Dunes", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 00953, Name = "Purvis Lake-Ober Foundation", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new SNAModel { DNRId = 02020, Name = "Quarry Park", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 01011, Name = "Racine Prairie", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new SNAModel { DNRId = 02009, Name = "Red Lake Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 01064, Name = "Rice Lake Savanna", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 00949, Name = "Richard M. and Mathilde Rice Elliot", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 00959, Name = "Ripley Esker", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 01066, Name = "River Terrace Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 00001, Name = "River Warren Outcrops", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 02025, Name = "Rock Ridge Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00963, Name = "Roscoe Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 00952, Name = "Rush Lake Island", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new SNAModel { DNRId = 01058, Name = "Rushford Sand Barrens", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 02010, Name = "Sand Lake Peatland", Biome = "Coniferous Forest", Landscape = "Laurentian Uplands" },
            new SNAModel { DNRId = 01032, Name = "Sandpiper Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 00989, Name = "Santee Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 00999, Name = "Savage Fen", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 02023, Name = "Sedan Brook Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 02018, Name = "Seminary Fen", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01004, Name = "Shooting Star Prairie", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 02011, Name = "South Black River Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 02012, Name = "Sprague Creek Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 01042, Name = "Spring Beauty Northern Hardwoods", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 01080, Name = "Spring Creek Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 01061, Name = "St. Croix Savanna", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new SNAModel { DNRId = 02038, Name = "St. Wendel Tamarack Bog", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new SNAModel { DNRId = 01069, Name = "Sugarloaf Point", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new SNAModel { DNRId = 01027, Name = "Swedes Forest", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01015, Name = "Townsend Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 02052, Name = "Twin Lakes", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 00991, Name = "Twin Valley Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 01065, Name = "Two Rivers Aspen Parkland", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new SNAModel { DNRId = 01039, Name = "Uncas Dunes", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new SNAModel { DNRId = 01055, Name = "Verlyn Marth Memorial Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01048, Name = "Wabu Woods", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new SNAModel { DNRId = 02063, Name = "Watrous Island", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 02013, Name = "Wawina Peatland", Biome = "Coniferous Forest", Landscape = "Tamarack Lowlands" },
            new SNAModel { DNRId = 02014, Name = "West Rat Root River Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new SNAModel { DNRId = 00951, Name = "Western Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Pairie" },
            new SNAModel { DNRId = 01097, Name = "Whitney Island", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01051, Name = "Wild Indigo", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new SNAModel { DNRId = 02015, Name = "Winter Road Lake Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new SNAModel { DNRId = 00985, Name = "Wolsfeld Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01087, Name = "Wood-Rill", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new SNAModel { DNRId = 01001, Name = "Wykoff Balsam Fir", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new SNAModel { DNRId = 01024, Name = "Yellow Bank Hills", Biome = "Prairie Grasslands", Landscape = "Minnesota River Pairie" },
            new SNAModel { DNRId = 01099, Name = "Zumbro Falls Woods", Biome = "Deciduous Woods", Landscape = "The Blufflands" }
        };
        _SNAs.InsertMany(snas);

        List<Biome> biomes = new()
        {
            new Biome
            {
                Name = "Coniferous Forest",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/lmf/photo_212ma.jpg",
                Landscapes = new List<string> { "Agassiz Lowlands",
                    "Border Lakes",
                    "Chippewa Plains",
                    "Laurentian Uplands",
                    "Littlefork - Vermillion Uplands",
                    "Mille Lacs Uplands",
                    "North Shore Highlands",
                    "Pine Moraines and Outwash Plains",
                    "St.Louis Moraines",
                    "Tamarack Lowlands"}
            },
            new Biome
            {
                Name = "Deciduous Woods",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/ebf/photo_222mb.jpg",
                Landscapes = new List<string>{"Anoka Sand Plain",
                        "Big Woods",
                        "Hardwoood Hills",
                        "Oak Savanna",
                        "Rochester Plateau",
                        "St. Paul-Baldwin Plains and Moraines",
                        "The Blufflands" }
            },
            new Biome
            {
                Name = "Prairie Grasslands",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/ppa/photo_251bb.jpg",
                Landscapes = new List<string> { "Coteau Moraines", "Inner Coteau", "Minnesota River Pairie", "Red River Pairie" }
            },
            new Biome
            {
                Name = "Tallgrass Aspen Parklands",
                Url = "https://images.dnr.state.mn.us/natural_resources/ecs/tap/photo_223na.jpg",
                Landscapes = new List<string> { "Aspen Parklands" }
            }
        };

        _Biomes.InsertMany(biomes);

    }

}
