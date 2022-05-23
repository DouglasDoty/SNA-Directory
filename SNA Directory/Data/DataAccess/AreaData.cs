using MongoDB.Driver;

namespace SNA_Directory.Data.DataAccess;

public class AreaData : IAreaData
{
    private readonly IMongoCollection<AreaModel> _Areas;
    public bool DatabaseInvalid { get; private set; }

    public AreaData(IDbConnection db)
    {
        _Areas = db.AreaCollection;
        try
        {
            if (_Areas.EstimatedDocumentCount() == 0)
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

    public async Task<List<AreaModel>> GetAllAreasAsync()
    {
        if (DatabaseInvalid) return null;
        var results = await _Areas.FindAsync(_ => true);
        return results.ToList();
    }

    public async Task<AreaModel> GetAreaAsync(string id)
    {
        if (DatabaseInvalid) return null;
        var results = await _Areas.FindAsync(s => s.Id == id);
        return results.FirstOrDefault();
    }

    public async Task<List<AreaModel>> GetProvinceAreasAsync(string biome)
    {
        if (DatabaseInvalid) return null;
        var results = await _Areas.FindAsync(p => p.Biome == biome);
        return results.ToList();
    }

    public Task CreateArea(AreaModel sna)
    {
        return _Areas.InsertOneAsync(sna);
    }

    public Task CreateMultipleAreas(IEnumerable<AreaModel> snas)
    {
        return _Areas.InsertManyAsync(snas);
    }

    public Task UpdateArea(AreaModel sna)
    {
        var filter = Builders<AreaModel>.Filter.Eq("Id", sna.Id);
        return _Areas.ReplaceOneAsync(filter, sna, new ReplaceOptions { IsUpsert = true });

    }

    public void InitializeDatabase()
    {
        List<AreaModel> snas = new()
        {
            new AreaModel { DNRId = 00972, Name = "Agassiz Dunes", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 02069, Name = "Antelope Valley", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 02037, Name = "Avon Hills Forest", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new AreaModel { DNRId = 02066, Name = "Badoura Jack Pine Woodland", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new AreaModel { DNRId = 01086, Name = "Bald Eagle Bluff", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 01078, Name = "Big Island", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new AreaModel { DNRId = 01035, Name = "Black Lake Bog", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 02041, Name = "Blaine Airport Rich Fen", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 02036, Name = "Blaine Preserve", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 02027, Name = "Blanket Flower Prairie", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new AreaModel { DNRId = 01050, Name = "Blue Devil Valley", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00996, Name = "Bluestem Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 02059, Name = "Boltuck-Rice Forever Wild", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new AreaModel { DNRId = 01043, Name = "Bonanza Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00967, Name = "Boot Lake", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 01005, Name = "Botany Bog", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new AreaModel { DNRId = 02067, Name = "Brownsville Bluff", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 01044, Name = "Bruce Hitman Heron Rookery", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01049, Name = "Burntside Islands", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new AreaModel { DNRId = 02043, Name = "Butternut Valley Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01045, Name = "Butterwort Cliffs", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 01014, Name = "Caldwell Brook Cedar Swamp", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 01002, Name = "Cannon River Trout Lily", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01030, Name = "Cannon River Turtle Preserve", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 02034, Name = "Cedar Mountain", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 02062, Name = "Cedar Rock", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01034, Name = "Chamberlain Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 02022, Name = "Cherry Grove Blind Valley", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new AreaModel { DNRId = 02040, Name = "Chimney Rock ", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new AreaModel { DNRId = 02028, Name = "Chisholm Point Island", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new AreaModel { DNRId = 00978, Name = "Clear Lake", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 02048, Name = "Clinton Falls Dwarf Trout Lily", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 01046, Name = "Clinton Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00964, Name = "Cold Spring Heron Colony", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new AreaModel { DNRId = 01053, Name = "Compass Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 01054, Name = "Cottonwood River Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 02068, Name = "Crystal Spring", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 01079, Name = "Des Moines River", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 02049, Name = "Dinner Creek", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new AreaModel { DNRId = 01052, Name = "Eagles Nest Island No. 4", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new AreaModel { DNRId = 02002, Name = "East Rat Root River Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 00974, Name = "Egret Island", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00984, Name = "Englund Ecotone ", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 01057, Name = "Falls Creek", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 00955, Name = "Felton Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 02045, Name = "Franconia Bluffs", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 00971, Name = "Frenchman's Bluff", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 01103, Name = "Glynn Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 01071, Name = "Gneiss Outcrops", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00977, Name = "Greenwater Lake", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new AreaModel { DNRId = 01101, Name = "Grey Cloud Dunes", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 01084, Name = "Gully Fen", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new AreaModel { DNRId = 01068, Name = "Gustafson's Camp", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 02017, Name = "Harry W. Cater Homestead Prairie", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 02042, Name = "Hastings Sand Coulee", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 00954, Name = "Hastings", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 00962, Name = "Helen Allison Savanna", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 01013, Name = "Hemlock Ravine", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 02001, Name = "Hole in the Bog Peatland", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new AreaModel { DNRId = 02016, Name = "Holthe Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 01047, Name = "Hovland Woods", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new AreaModel { DNRId = 01038, Name = "Hythecker Prairie", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 01000, Name = "Iona's Beach", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 01018, Name = "Iron Horse Prairie", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 00966, Name = "Iron Springs Bog", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new AreaModel { DNRId = 00958, Name = "Itasca Wilderness Sanctuary", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new AreaModel { DNRId = 01056, Name = "Joseph A. Tauer Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00961, Name = "Kasota Prairie", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01074, Name = "Kawishiwi Pines", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new AreaModel { DNRId = 00979, Name = "Kellogg Weaver Dunes", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 00950, Name = "Kettle River", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 01037, Name = "King's and Queen's Bluffs", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 02056, Name = "La Salle Lake", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new AreaModel { DNRId = 01023, Name = "Ladies Tresses Swamp", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new AreaModel { DNRId = 01094, Name = "Lake Alexander Woods", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new AreaModel { DNRId = 01075, Name = "Lake Bronson Parkland", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new AreaModel { DNRId = 02044, Name = "Langhei Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 02065, Name = "Lawrence Creek", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 02046, Name = "Lester Lake", Biome = "Coniferous Forest", Landscape = "Pine Moraines and Outwash Plains" },
            new AreaModel { DNRId = 02054, Name = "Little Too Much Lake", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new AreaModel { DNRId = 01063, Name = "Lost 40", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new AreaModel { DNRId = 01072, Name = "Lost Lake Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 02003, Name = "Lost River Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 01041, Name = "Lost Valley Prairie", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 01077, Name = "Lundblad Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 01070, Name = "Lutsen", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 02004, Name = "Luxemberg Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 00968, Name = "Malmberg Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 01022, Name = "Mary Schmidt Crawford Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01033, Name = "McGregor Marsh", Biome = "Coniferous Forest", Landscape = "Tamarack Lowlands" },
            new AreaModel { DNRId = 02060, Name = "Mille Lacs Moraine", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 02000, Name = "Minnesota Point Pine Forest", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 02070, Name = "Mississippi Oxbow", Biome = "Coniferous Forest", Landscape = "Tamarack Lowlands" },
            new AreaModel { DNRId = 00980, Name = "Mississippi River Islands", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 01017, Name = "Moose Mountain", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 02051, Name = "Morton Outcrops", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01059, Name = "Mound Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 02026, Name = "Mound Spring Prairie", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 01073, Name = "Mulligan Lake Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 02032, Name = "Myhr Creek Ridge", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 02005, Name = "Myrtle Lake Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 02006, Name = "Nett Lake Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 02007, Name = "Norris Camp Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 01098, Name = "North Black River Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 01081, Name = "North Fork Zumbro Woods", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new AreaModel { DNRId = 01095, Name = "Oronoco Prairie", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new AreaModel { DNRId = 01031, Name = "Osmundson Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01040, Name = "Otter Tail Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 00960, Name = "Partch Woods", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new AreaModel { DNRId = 00988, Name = "Pembina Trail", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new AreaModel { DNRId = 00957, Name = "Pennington Bog", Biome = "Coniferous Forest", Landscape = "Chippewa Plains" },
            new AreaModel { DNRId = 01009, Name = "Pigs Eye Island Heron Rookery", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 02021, Name = "Pin Oak Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 01007, Name = "Pine and Curry Island", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 02030, Name = "Pine Bend Bluffs", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 02008, Name = "Pine Creek Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 02055, Name = "Potato Lake", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 01036, Name = "Prairie Bush Clover", Biome = "Prairie Grasslands", Landscape = "Coteau Moraines" },
            new AreaModel { DNRId = 01026, Name = "Prairie Coteau", Biome = "Prairie Grasslands", Landscape = "Inner Coteau" },
            new AreaModel { DNRId = 01062, Name = "Prairie Creek Woods", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 01067, Name = "Prairie Smoke Dunes", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 00953, Name = "Purvis Lake-Ober Foundation", Biome = "Coniferous Forest", Landscape = "Border Lakes" },
            new AreaModel { DNRId = 02020, Name = "Quarry Park", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 01011, Name = "Racine Prairie", Biome = "Deciduous Woods", Landscape = "Rochester Plateau" },
            new AreaModel { DNRId = 02009, Name = "Red Lake Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 01064, Name = "Rice Lake Savanna", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 00949, Name = "Richard M. and Mathilde Rice Elliot", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 00959, Name = "Ripley Esker", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 01066, Name = "River Terrace Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 00001, Name = "River Warren Outcrops", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 02025, Name = "Rock Ridge Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00963, Name = "Roscoe Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 00952, Name = "Rush Lake Island", Biome = "Coniferous Forest", Landscape = "Mille Lacs Uplands" },
            new AreaModel { DNRId = 01058, Name = "Rushford Sand Barrens", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 02010, Name = "Sand Lake Peatland", Biome = "Coniferous Forest", Landscape = "Laurentian Uplands" },
            new AreaModel { DNRId = 01032, Name = "Sandpiper Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 00989, Name = "Santee Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 00999, Name = "Savage Fen", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 02023, Name = "Sedan Brook Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 02018, Name = "Seminary Fen", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01004, Name = "Shooting Star Prairie", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 02011, Name = "South Black River Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 02012, Name = "Sprague Creek Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 01042, Name = "Spring Beauty Northern Hardwoods", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 01080, Name = "Spring Creek Prairie", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 01061, Name = "St. Croix Savanna", Biome = "Deciduous Woods", Landscape = "St. Paul-Baldwin Plains and Moraines" },
            new AreaModel { DNRId = 02038, Name = "St. Wendel Tamarack Bog", Biome = "Deciduous Woods", Landscape = "Hardwoood Hills" },
            new AreaModel { DNRId = 01069, Name = "Sugarloaf Point", Biome = "Coniferous Forest", Landscape = "North Shore Highlands" },
            new AreaModel { DNRId = 01027, Name = "Swedes Forest", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01015, Name = "Townsend Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 02052, Name = "Twin Lakes", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 00991, Name = "Twin Valley Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 01065, Name = "Two Rivers Aspen Parkland", Biome = "Tallgrass Aspen Parklands", Landscape = "Aspen Parklands" },
            new AreaModel { DNRId = 01039, Name = "Uncas Dunes", Biome = "Deciduous Woods", Landscape = "Anoka Sand Plain" },
            new AreaModel { DNRId = 01055, Name = "Verlyn Marth Memorial Prairie", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01048, Name = "Wabu Woods", Biome = "Coniferous Forest", Landscape = "St. Louis Moraines" },
            new AreaModel { DNRId = 02063, Name = "Watrous Island", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 02013, Name = "Wawina Peatland", Biome = "Coniferous Forest", Landscape = "Tamarack Lowlands" },
            new AreaModel { DNRId = 02014, Name = "West Rat Root River Peatland", Biome = "Coniferous Forest", Landscape = "Littlefork-Vermillion Uplands" },
            new AreaModel { DNRId = 00951, Name = "Western Prairie", Biome = "Prairie Grasslands", Landscape = "Red River Prairie" },
            new AreaModel { DNRId = 01097, Name = "Whitney Island", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01051, Name = "Wild Indigo", Biome = "Deciduous Woods", Landscape = "Oak Savanna" },
            new AreaModel { DNRId = 02015, Name = "Winter Road Lake Peatland", Biome = "Coniferous Forest", Landscape = "Agassiz Lowlands" },
            new AreaModel { DNRId = 00985, Name = "Wolsfeld Woods", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01087, Name = "Wood-Rill", Biome = "Deciduous Woods", Landscape = "Big Woods" },
            new AreaModel { DNRId = 01001, Name = "Wykoff Balsam Fir", Biome = "Deciduous Woods", Landscape = "The Blufflands" },
            new AreaModel { DNRId = 01024, Name = "Yellow Bank Hills", Biome = "Prairie Grasslands", Landscape = "Minnesota River Prairie" },
            new AreaModel { DNRId = 01099, Name = "Zumbro Falls Woods", Biome = "Deciduous Woods", Landscape = "The Blufflands" }
        };
        _Areas.InsertMany(snas);


    }
}
