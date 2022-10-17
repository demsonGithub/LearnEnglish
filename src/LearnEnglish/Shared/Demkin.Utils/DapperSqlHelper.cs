using System.Reflection;

namespace Demkin.Utils
{
    public class DapperSqlHelper
    {
        public string Key { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// 查看Dapper最终生成的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetSqlStr(string sql, object param = null)
        {
            try
            {
                string tempSql = sql;
                if (param != null)
                {
                    if (param.ToString().Contains("{") && param.ToString().Contains("}"))//匿名类型
                    {
                        string[] arr = param.ToString().Replace("{", "").Replace("}", "").Split(',');
                        List<DapperSqlHelper> paramList = new List<DapperSqlHelper>();
                        foreach (var item in arr)
                        {
                            DapperSqlHelper kv = new DapperSqlHelper();
                            string[] temp = item.Split('=');
                            kv.Key = temp[0].Trim();
                            kv.Value = temp[1].Trim();
                            paramList.Add(kv);
                        }
                        foreach (var par in paramList)
                        {
                            tempSql = tempSql.Replace("@" + par.Key, "'" + par.Value + "'");
                        }
                    }
                    else//自定义实体类型
                    {
                        Type type = param.GetType();
                        foreach (PropertyInfo p in type.GetProperties())
                        {
                            var Key = p.Name;
                            var Value = p.GetValue(param);
                            tempSql = tempSql.Replace("@" + Key, "'" + Value + "'");
                        }
                    }
                }
                return tempSql;
            }
            catch
            {
                return sql;
            }
        }
    }
}