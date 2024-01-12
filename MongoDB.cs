using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AddMongo
{
    //连接数据库
    public class MongoDB
    {
        /// <summary>
        /// 获取 MongoDB 数据库连接
        /// </summary>
        /// <param name="collectionName">集合（表）名称</param>
        /// <returns>MongoDB 集合对象</returns>
        //连接数据库方法
        public IMongoDatabase MongoDBClass()
        {
            // MongoDB 服务器连接字符串
            string mongoCon = @"mongodb://127.0.0.1:27017";

            // 创建 MongoDB 客户端
            var client = new MongoClient(mongoCon);

            // 获取 MongoDB 数据库对象
            var database = client.GetDatabase("steedos");
            return database;
        }
        //返回集合
        public IMongoCollection<BsonDocument> MongoDB1(string collectionName)
        {
            var database = MongoDBClass();
            // 返回指定集合的 MongoDB 集合对象
            return database.GetCollection<BsonDocument>(collectionName);
        }
        //判断集合是否存在
        public bool DataIsNull(string collectionName)
        {
            var database = MongoDBClass();
            var collectionNames = database.ListCollectionNames().ToList();

            if (collectionNames.Contains(collectionName))
            {
                // 集合存在，执行查询
                return true;
            }
            else
            {
                // 集合不存在
                return false;
            }
            
        }
    }
}
