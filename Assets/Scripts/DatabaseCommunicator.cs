using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

public class DatabaseCommunicator : MonoBehaviour
{
    public List<Item> items;

    public List<Plant> plants;

    public List<ItemInventory> itemsInventory;

    MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

    private static MySqlConnection _connection;

    private static void OpenConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
        {
            try
            {
                _connection.Open();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }
    private static void CloseConnection()
    {
        try
        {
            _connection.Close();
        }
        catch (System.Exception e)
        {

            Debug.Log(e);
        }
    }
    private void Awake()
    {
        builder.Server = "server134.hosting.reg.ru";
        builder.Port = 3306;
        builder.Database = "u2596281_Farm";
        builder.UserID = "u2596281_admFarm";
        builder.Password = "waq-i9B-RrX-M24";

        _connection = new MySqlConnection(builder.ToString());

        OpenConnection();
        try
        {
            LoadCost();
            LoadDataItems();
            LoadPlant();
            LoadLevel();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            CloseConnection();
        }

        CloseConnection();

        itemsInventory = PlayerInventory.ItemsInventory;

        DontDestroyOnLoad(gameObject);
    }

    private void LoadLevel()
    {
        string loalLevel = $"Select * from levelplayer;";
        MySqlCommand loalLevelCommand = new MySqlCommand(loalLevel, _connection);
        MySqlDataReader reader = loalLevelCommand.ExecuteReader();
        while (reader.Read())
        {
            Levels.LevelsPlayer.Add(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()));
        }
        reader.Close();
    }

    private void LoadCost()
    {
        string loadCost = $"Select * from cost;";
        MySqlCommand loadCostCommand = new MySqlCommand(loadCost, _connection);
        MySqlDataReader reader = loadCostCommand.ExecuteReader();
        while (reader.Read())
        {
            Costs.CostItems.Add(int.Parse(reader[0].ToString()), new List<int> { int.Parse(reader[2].ToString()), int.Parse(reader[1].ToString()) });
        }
        reader.Close();
    }
    private void LoadDataItems()
    {
        LoadInstrument();
        LoadMaterial();
        LoadFood();
        LoadSeed();
    }
    private void LoadInstrument()
    {
        string loadInstrument = $"Select * from instrument;";
        MySqlCommand loadInstrumentCommand = new MySqlCommand(loadInstrument, _connection);
        MySqlDataReader reader = loadInstrumentCommand.ExecuteReader();

        while (reader.Read())
        {
            Item item = new Item(int.Parse(reader[0].ToString()), reader[3].ToString(), Costs.CostItems[int.Parse(reader[1].ToString())][0], Costs.CostItems[int.Parse(reader[1].ToString())][1],
                new Instrument(int.Parse(reader[2].ToString())));
            items.Add(item);
            AllItems.Items.Add(item);
        }
        reader.Close();
    }
    private void LoadMaterial()
    {
        string loadMaterial = $"Select * from material;";
        MySqlCommand loadMaterialCommand = new MySqlCommand(loadMaterial, _connection);
        MySqlDataReader reader = loadMaterialCommand.ExecuteReader();

        while (reader.Read())
        {
            Item item = new Item(int.Parse(reader[0].ToString()), reader[2].ToString(), Costs.CostItems[int.Parse(reader[1].ToString())][0], Costs.CostItems[int.Parse(reader[1].ToString())][1],
                new Material());
            items.Add(item);
            AllItems.Items.Add(item);
        }
        reader.Close();
    }
    private void LoadSeed()
    {
        string loadSeed = $"Select * from seed;";
        MySqlCommand loadSeedCommand = new MySqlCommand(loadSeed, _connection);
        MySqlDataReader reader = loadSeedCommand.ExecuteReader();

        while (reader.Read())
        {
            Item item = new Item(int.Parse(reader[0].ToString()), reader[3].ToString(), Costs.CostItems[int.Parse(reader[1].ToString())][0], Costs.CostItems[int.Parse(reader[1].ToString())][1],
                new Seed(int.Parse(reader[2].ToString())));
            items.Add(item);
            AllItems.Items.Add(item);
        }
        reader.Close();
    }
    private void LoadFood()
    {
        string loadFood = $"Select * from food;";
        MySqlCommand loadFoodCommand = new MySqlCommand(loadFood, _connection);
        MySqlDataReader reader = loadFoodCommand.ExecuteReader();

        while (reader.Read())
        {
            Item item = new Item(int.Parse(reader[0].ToString()), reader[4].ToString(), Costs.CostItems[int.Parse(reader[1].ToString())][0], Costs.CostItems[int.Parse(reader[1].ToString())][1],
                new Food(int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString())));
            items.Add(item);
            AllItems.Items.Add(item);
        }
        reader.Close();
    }
    private void LoadPlant()
    {
        string loadPlant = $"Select * from plant;";

        MySqlCommand loadPlantCommand = new MySqlCommand(loadPlant, _connection);

        MySqlDataReader reader = loadPlantCommand.ExecuteReader();

        while (reader.Read())
        {
            Plant plant = new Plant(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), int.Parse(reader[2].ToString()), reader[3].ToString(),
                Convert.ToBoolean(int.Parse(reader[4].ToString())), reader[5].ToString(), int.Parse(reader[6].ToString()));

            plants.Add(plant);
            AllPlants.Plants.Add(plant);
        }
        reader.Close();
    }
    public static void Registration()
    {
        OpenConnection();

        string registration = $"insert into user(email,password,username) VALUES(@email,@password,@username)";

        MySqlCommand registrationCommand = new MySqlCommand(registration, _connection);

        registrationCommand.Parameters.AddWithValue("@email", PlayerInfo.Email);
        registrationCommand.Parameters.AddWithValue("@password", PlayerInfo.Password);
        registrationCommand.Parameters.AddWithValue("@username", PlayerInfo.Username);

        registrationCommand.ExecuteNonQuery();

        CloseConnection();
    }
    public static string Authorization(string username, string password)
    {
        string idResult = "";
        string usernameResult = "";
        string passwordResult = "";

        OpenConnection();

        string authorization = $"Select * from user where username = @username";

        MySqlCommand authorizationCommand = new MySqlCommand(authorization, _connection);

        authorizationCommand.Parameters.AddWithValue("@username", username);

        MySqlDataReader reader = authorizationCommand.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    idResult = reader.GetValue(0).ToString();
                    usernameResult = reader.GetValue(1).ToString();
                    passwordResult = reader[2].ToString();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            reader.Close();

            CloseConnection();
        }

        reader.Close();

        CloseConnection();

        if (usernameResult != "")
        {
            if (passwordResult == password)
            {
                PlayerInfo.Id = int.Parse(idResult);
                PlayerInfo.Password = passwordResult;
                PlayerInfo.Username = usernameResult;

                return "";
            }
            else
            {
                return "Введён неверный пароль";
            }
        }
        else
        {
            return "Такого пользователя не существует";
        }
    }
    public static bool CheckUser()
    {
        bool exist = true;

        OpenConnection();
        string check = $"Select * from user where username = @username";

        MySqlCommand checkCommand = new MySqlCommand(check, _connection);

        checkCommand.Parameters.AddWithValue("@username", PlayerInfo.Username);

        MySqlDataReader reader = checkCommand.ExecuteReader();

        if (reader.HasRows)
            exist = true;
        else
            exist = false;

        CloseConnection();

        return exist;
    }
    public static List<Save> LoadSaves()
    {
        List<Save> saves = new List<Save>();

        OpenConnection();

        string load = $"Select * from save where idUser = @idUser";

        MySqlCommand loadCommand = new MySqlCommand(load, _connection);

        loadCommand.Parameters.AddWithValue("@idUser", PlayerInfo.Id);
        MySqlDataReader reader = loadCommand.ExecuteReader();

        try
        {
            while (reader.Read())
            {
                saves.Add(new Save(int.Parse(reader[0].ToString()), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), reader[7].ToString(), int.Parse(reader[11].ToString())));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            reader.Close();

            CloseConnection();
        }

        reader.Close();

        CloseConnection();

        return saves;
    }
    public static void RewriteSave(Save save)
    {
        OpenConnection();

        string rewrite = $"update save set idLevelPlayer=@idLevelPlayer,experience=@experience,money=@money,food=@food,fluit=@fluit,saveInventory=@saveInventory,score=@score where idSave = @idSave";

        MySqlCommand rewriteCommand = new MySqlCommand(rewrite, _connection);

        rewriteCommand.Parameters.AddWithValue("@idSave", save.Id);
        rewriteCommand.Parameters.AddWithValue("@idLevelPlayer", save.Level);
        rewriteCommand.Parameters.AddWithValue("@experience", save.Experience);
        rewriteCommand.Parameters.AddWithValue("@money", save.Money);
        rewriteCommand.Parameters.AddWithValue("@food", save.Food);
        rewriteCommand.Parameters.AddWithValue("@fluit", save.Fluit);
        rewriteCommand.Parameters.AddWithValue("@saveInventory", save.InventoryString);
        rewriteCommand.Parameters.AddWithValue("@score", save.Score);

        rewriteCommand.ExecuteNonQuery();

        CloseConnection();

        ChangeLeaderboard();
    }
    public static void CreateSave(Save save)
    {
        OpenConnection();

        string rewrite = $"insert into save(idUser,idLevelPlayer,experience,money,food,fluit,saveInventory,score) VALUES(@idUser,@idLevelPlayer,@experience,@money,@food,@fluit,@saveInventory,@score)";

        MySqlCommand rewriteCommand = new MySqlCommand(rewrite, _connection);

        rewriteCommand.Parameters.AddWithValue("@idUser", PlayerInfo.Id);
        rewriteCommand.Parameters.AddWithValue("@idLevelPlayer", save.Level);
        rewriteCommand.Parameters.AddWithValue("@experience", save.Experience);
        rewriteCommand.Parameters.AddWithValue("@money", save.Money);
        rewriteCommand.Parameters.AddWithValue("@food", save.Food);
        rewriteCommand.Parameters.AddWithValue("@fluit", save.Fluit);
        rewriteCommand.Parameters.AddWithValue("@saveInventory", save.InventoryString);
        rewriteCommand.Parameters.AddWithValue("@score", save.Score);

        rewriteCommand.ExecuteNonQuery();

        CloseConnection();

        ChangeLeaderboard();
    }
    public static int SelectUserId()
    {
        OpenConnection();

        int idUser = 0;

        string rewrite = $"select idUser from user where username=@username";

        MySqlCommand rewriteCommand = new MySqlCommand(rewrite, _connection);

        rewriteCommand.Parameters.AddWithValue("@username", PlayerInfo.Username);
        Debug.Log(PlayerInfo.Username);

        MySqlDataReader reader = rewriteCommand.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                idUser = int.Parse(reader[0].ToString());
            }
        }  
        
        reader.Close();

        CloseConnection();
        return idUser;
    }
    public static void ChangeLeaderboard()
    {
        OpenConnection();
        string checkLeader = $"Select score from leaderboard where idUser = @idUser";

        MySqlCommand checkLeaderCommand = new MySqlCommand(checkLeader, _connection);

        checkLeaderCommand.Parameters.AddWithValue("@idUser", PlayerInfo.Id);
        MySqlDataReader reader = checkLeaderCommand.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int score = int.Parse(reader[0].ToString());

                    if (PlayerIndicators.Score > score)
                    {
                        reader.Close();
                        CloseConnection();
                        UpdateLeaderboard(false);
                    }
                }
                reader.Close();

                CloseConnection();  
            }
            else
            {
                reader.Close();
                CloseConnection();
                UpdateLeaderboard(true);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            reader.Close();

            CloseConnection();
        }
    }
    public static void UpdateLeaderboard(bool add)
    {
        OpenConnection();
        if (!add)
        {
            Debug.Log("sa");
            string create = $"update leaderboard set score=@score where idUser = @idUser";

            MySqlCommand createCommand = new MySqlCommand(create, _connection);

            createCommand.Parameters.AddWithValue("@idUser", PlayerInfo.Id);
            createCommand.Parameters.AddWithValue("@score", PlayerIndicators.Score);

            createCommand.ExecuteNonQuery();
        }
        else
        {
            Debug.Log("sa1");
            string create = $"insert into leaderboard(idUser,score) VALUES(@idUser,@score)";

            MySqlCommand createCommand = new MySqlCommand(create, _connection);

            createCommand.Parameters.AddWithValue("@idUser", PlayerInfo.Id);
            createCommand.Parameters.AddWithValue("@score", PlayerIndicators.Score);

            createCommand.ExecuteNonQuery();
        }
        CloseConnection();
    }
    public static List<List<string>> LoadLeaderboard()
    {
        List<List<string>> leaderboard = new List<List<string>>();

        OpenConnection();
       
            string load = $"SELECT u.username, l.score FROM leaderboard l JOIN user u ON l.idUser = u.idUser ORDER BY l.score DESC";

            MySqlCommand loadCommand = new MySqlCommand(load, _connection);

        MySqlDataReader reader = loadCommand.ExecuteReader();

        try
        {
            while (reader.Read())
            {
                leaderboard.Add(new List<string> { reader[0].ToString(), reader[1].ToString() });
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            reader.Close();

            CloseConnection();
        }

        reader.Close();

        CloseConnection();

        return leaderboard;
    }
}
