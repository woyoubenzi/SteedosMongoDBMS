using MongoDB.Bson;
using MongoDB.Driver;

//实例化数据库连接对象
AddMongo.MongoDB a = new AddMongo.MongoDB();
//获取用户输入的表名
Console.WriteLine("欢迎来到简易mong数据库增删改查系统");
Console.WriteLine("默认数据库为本地steedos");
Console.WriteLine("请输入steedos唯一标识space的值,用于支持添加操作,输入0可以跳过");
var space = Console.ReadLine();
if (space == null)
{
    Console.WriteLine("space不能为空");
    space = "653617a10220596e987efd11";
}
else if (space == "0")
{
    Console.WriteLine("设置space为默认值：653617a10220596e987efd11");
    space = "653617a10220596e987efd11";
}
Console.WriteLine("请输入steedos唯一标识owner的值,用于支持添加操作,输入0可以跳过");
var owner = Console.ReadLine();
if (owner == null)
{
    Console.WriteLine("owner不能为空");
    owner = "6536179b0220596e987efd04";
}
else if (owner == "0")
{
    Console.WriteLine("设置owner为默认值：6536179b0220596e987efd04");
    owner = "6536179b0220596e987efd04";
}
Console.WriteLine("请输入需要进行操作的表名");
var classname = Console.ReadLine();
//内置一个表单
var datamvv = "space_users";
if (classname == null)
{
    Console.WriteLine("表名不能为空,已修改为系统内置人员表");
    classname = datamvv;
}
//检查表名是否正确
if (!a.DataIsNull(classname))
{
    Console.WriteLine("表名不存在,已修改为系统内置人员表");
    classname = datamvv;
}
//获取表的集合
var b = a.MongoDB1(classname);
int zhi = 1;
try
{
    do
    {
        Console.WriteLine("=======简易mong数据库增删改查系统=====");
        Console.WriteLine("1.查询所有");
        Console.WriteLine("2.添加一条字段");
        Console.WriteLine("3.按ID删除信息");
        Console.WriteLine("4.按ID修改字段");
        Console.WriteLine("5.按条件查询");
        Console.WriteLine("0.退出程序");
        Console.WriteLine("请输入功能对应的数字要进行的操作");
        var cr = Console.ReadLine();
        switch (cr)
        {
            case "1":
                //查询
                Console.WriteLine("查询所有");
                var res = b.Find(new BsonDocument()).ToList();
                if (res.Count > 0)
                {
                    Console.WriteLine("查询成功，找到了记录。");
                    foreach (var item in res)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("查询成功，但未找到匹配的记录。");
                }
                break;
            case "2":
                //添加
                // 生成新的 ObjectId随机ID
                ObjectId objectId = ObjectId.GenerateNewId();
                string idString = objectId.ToString();
                Console.WriteLine("假如添加失败请检查你的space和owner值是否输入正确，且表名是否输入正确");
                Console.WriteLine("请输入字段名");
                var iname = Console.ReadLine();
                if (iname == null)
                {
                    Console.WriteLine("字段名不能为空");
                    break;
                }
                Console.WriteLine("请输入字段值(默认此值会传递给name字段)");
                var idata = Console.ReadLine();
                if (idata == null)
                {
                    Console.WriteLine("字段值不能为空");
                    break;
                }
                AddMongo.mvv mvv = new AddMongo.mvv(idString, iname, idata, space, owner);
                var adddata= new BsonDocument();
                if (iname != "name")
                {
                    adddata = new BsonDocument {
                  { "_id",mvv._id },
                  { iname,idata },
                  { "name",idata },
                  {  "space",mvv.space },
                  { "created",mvv.created },
                  { "modified",mvv.modified },
                  { "owner",mvv.owner },
                  { "created_by",mvv.created_by },
                  { "modified_by",mvv.modified_by },
                  { "company_id",mvv.company_id },
                  { "company_ids" , new BsonArray(mvv.company_ids) }
                    };
                }
                else
                {
                    adddata = new BsonDocument {
                  { "_id",mvv._id },
                  { iname,idata },
                  {  "space",mvv.space },
                  { "created",mvv.created },
                  { "modified",mvv.modified },
                  { "owner",mvv.owner },
                  { "created_by",mvv.created_by },
                  { "modified_by",mvv.modified_by },
                  { "company_id",mvv.company_id },
                  { "company_ids" , new BsonArray(mvv.company_ids) }
                     };
                }
                
                b.InsertOne(adddata);
                break;
            case "3":
                //删除
                Console.WriteLine("请输入要删除的ID");
                var dataid = Console.ReadLine();
                if (dataid == null)
                {
                    Console.WriteLine("ID不能为空");
                    break;
                }
                //根据ID删除
                var result=b.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", dataid));
                if (result.DeletedCount > 0)
                {
                    Console.WriteLine($"删除成功，删除了 {result.DeletedCount} 条记录。");
                }
                else
                {
                    Console.WriteLine("删除失败，未找到匹配的记录。");
                }
                break;
            case "4":
                //修改
                Console.WriteLine("请输入要修改字段的ID");
                var idu = Console.ReadLine();
                if (idu == null)
                {
                    Console.WriteLine("ID不能为空");
                    break;
                }
                Console.WriteLine("请输入你需要将name更改为的字段");
                var nameu = Console.ReadLine();
                if (nameu == null)
                {
                    Console.WriteLine("name不能为空");
                    break;
                }
                //根据ID更新
                var update = b.UpdateMany(Builders<BsonDocument>.Filter.Eq("_id", idu), Builders<BsonDocument>.Update.Set("name", nameu));
                if (update.ModifiedCount > 0)
                {
                    Console.WriteLine($"更新成功，更新了 {update.ModifiedCount} 条记录。");
                }
                else
                {
                    Console.WriteLine("更新失败，未找到匹配的记录。");
                }
                break;
            case "5":
                //按条件查询
                Console.WriteLine("请输入条件字段");
                var nameuif = Console.ReadLine();
                if (nameuif == null)
                {
                    Console.WriteLine("条件字段不能为空");
                    break;
                }
                Console.WriteLine("请输入条件值");
                var nameudata = Console.ReadLine();
                if (nameudata == null)
                {
                    Console.WriteLine("条件值不能为空");
                    break;
                }
                var finif = b.Find(new BsonDocument() { { nameuif, nameudata } }).ToList();
                Console.WriteLine("查询结果");
                foreach (var item in finif)
                {
                    Console.WriteLine(item);
                }
                break;
            case "0":
                zhi = 2;
                Console.WriteLine("程序退出");
                break;
            default:
                Console.WriteLine("程序异常退出");
                break;
        }
    } while (zhi == 1);
}
catch (Exception ex)
{
    Console.WriteLine("错误信息：",ex.Message);
}


//多线程查询
//var list = Task.Run(async() =>await b.Find);