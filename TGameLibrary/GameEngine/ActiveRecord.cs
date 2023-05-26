using System.Collections.ObjectModel;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace TanksGameEngine.GameEngine
{                                                                         
    /// <summary>
    /// ActiveRecord class that provides database _connection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActiveRecord<T> where T : ActiveRecord<T>
    {
        /// <summary>
        /// Sql _connection field
        /// </summary>
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TGameSqlProvider"].ConnectionString);

        /// <summary>
        /// This property supports Sql _connection in active state
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                while (true)
                {
                    try
                    {
                        if (_connection.State == ConnectionState.Broken)
                        {
                            _connection.Close();
                        }
                        if (_connection.State == ConnectionState.Closed)
                        {
                            _connection.Open();
                        }
                        return _connection;
                    }
                    catch (SqlException e)
                    {
                        _connection.Close();

                        Console.WriteLine(e.Message);
               
                    }
                }
            }
        }

        /// <summary>
        /// This property returns full table name by the short table name
        /// </summary>
        public static String TableName
        {
            get
            {
                return @"TGameBase.dbo.[" + typeof(T).Name.ToString() + "]";
            }
        }

        /// <summary>
        /// Method that make dictionary of parametres to database querry 
        /// from object field information.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> DataParametres(T obj)
        {
            Dictionary<String, Object> Selected = new Dictionary<String, Object>();

            foreach (MemberInfo mi in typeof(T).GetMembers())
            {
                if (Attribute.IsDefined(mi, typeof(DataMemberAttribute)))
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        Selected.Add(mi.Name, ((PropertyInfo)mi).GetValue(obj));
                    }
                    if (mi.MemberType == MemberTypes.Field)
                    {
                        Selected.Add(mi.Name, ((FieldInfo)mi).GetValue(obj));
                    }
                }
            }

            return Selected;
        }

        /// <summary>
        /// Method that gets all objects of the same type from database
        /// </summary>
        /// <returns></returns>
        public static Collection<T> AllItems()
        {
            Collection<T> items = new Collection<T>();

            String sql = String.Format(CultureInfo.InvariantCulture, "Select * From {0}", TableName);

            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                DataTable data_table = new DataTable();

                data_table.Locale = CultureInfo.InvariantCulture;

                SqlDataReader data_reader = cmd.ExecuteReader();
                data_table.Load(data_reader);
                data_reader.Close();

                Dictionary<string, object> parametres;

                foreach (DataRow dataRow in data_table.Rows)
                {
                    parametres = dataRow.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dataRow.Field<object>(col.ColumnName));

                    items.Add((T)Activator.CreateInstance(typeof(T), new object[] { parametres }));
                }

                data_table.Dispose();
            }

            return items;
        }

        /// <summary>
        /// Method that takes object from database by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static T GetItem(Guid Id)
        {
            T obj = default(T);

            String sql = String.Format(CultureInfo.InvariantCulture, "Select * From {0} Where Id = @Id", TableName);

            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id.ToString());

                DataTable data_table = new DataTable();

                data_table.Locale = CultureInfo.InvariantCulture;

                SqlDataReader dr = cmd.ExecuteReader();

                data_table.Load(dr);
                dr.Close();

                Dictionary<string, object> parametres;

                try
                {
                    DataRow dataRow = data_table.Rows[0];

                    parametres = dataRow.Table.Columns.Cast<DataColumn>()
                        .ToDictionary(col => col.ColumnName, col => dataRow.Field<object>(col.ColumnName));

                    obj = (T) Activator.CreateInstance(typeof(T), new object[] { parametres });
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message); 
                    
                }    

                data_table.Dispose();
            }

            return obj;
        }

        /// <summary>
        /// Method that inserts object in database
        /// </summary>
        /// <param name="Obj"></param>
        public static void Insert(T obj)
        {
            Dictionary<String, Object> Params = DataParametres(obj);

            string sql = string.Format(CultureInfo.InvariantCulture, "Insert Into {0}({1}) Values(@{2})", TableName, String.Join(", ", Params.Keys), String.Join(", @", Params.Keys));

            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                foreach (String key in Params.Keys)
                {
                    cmd.Parameters.AddWithValue("@" + key, Params[key]);
                }

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method that updates object in database
        /// </summary>
        /// <param name="obj"></param>
        public static void Update(T obj)
        {
            Dictionary<String, Object> Params = DataParametres(obj);

            List<String> ParamNames = new List<String>();

            foreach (String key in Params.Keys)
            {
                if (!key.Equals("Id"))
                {
                    ParamNames.Add(key + " = @" + key);
                }
            }

            string sql = string.Format(CultureInfo.InvariantCulture, "Update {0} Set {1} Where Id = @Id", TableName, String.Join(", ", ParamNames));

            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                foreach (String key in Params.Keys)
                {

                    cmd.Parameters.AddWithValue("@" + key, Params[key]);
                }

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method that remove object with certain Id from database
        /// </summary>
        /// <param name="Id"></param>
        public static void Delete(Guid Id)
        {
            string sql = string.Format(CultureInfo.InvariantCulture, "Delete from {0} where Id = @Id", TableName);

            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }

                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);    
                }
            }
        }

        /// <summary>
        /// Identifical guid key of object(property)
        /// </summary>
        [DataMember]
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// IsNew field. If object loaded from database, isNew == false, if it created
        /// in game process, isNew == true 
        /// </summary>
        private Boolean isNew;

        /// <summary>
        /// IsNew property
        /// </summary>
        protected Boolean IsNew
        {
            get; set; 
        }

        /// <summary>
        /// First instance constructor for Active Record class object. It generate new
        /// object and set new guid key to it
        /// </summary>
        public ActiveRecord()
        {
            this.Id = Guid.NewGuid();
            isNew = true;
        }

        /// <summary>
        /// Second instance constructor for Active Record class object. It finds an object data
        /// from database by guid Id and creates an Active Record object
        /// </summary>
        /// <param name="Id"></param>
        public ActiveRecord(Guid Id)
        {
            this.Id = Id;
            isNew = false;
        }

        /// <summary>
        /// Mehod that deletes certain object from database(but not from runtime)
        /// </summary>
        public void Delete()
        {
            Delete(this.Id);
            isNew = true;
        }

        /// <summary>
        /// Method that saves current object to database
        /// </summary>
        public void Save()
        {
            if (isNew == true)
            {
                Insert((T)this);
                isNew = false;
            }
            else
            {
                Update((T)this);
            }
        }
    }

    /// <summary>
    /// DataMember attirbute that marks data fields in object
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DataMemberAttribute : System.Attribute
    {
        /// <summary>
        /// Name of data property attribute
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        ///  First data member attribute constructor
        /// </summary>
        public DataMemberAttribute(){ }

        /// <summary>
        ///  Second data member attribute constructor
        /// </summary>
        /// <param name="str"></param>
        public DataMemberAttribute(string str)
        {
            Name = str;
        }
    }
}