using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private enum ChunkShpaes
    {
        None,
        One,
        Two,
        Four,
    }
    [Header("Elements")]
    [SerializeField] private Transform world;
    [SerializeField] private int gridSize;
    [SerializeField] private int gridScale;
    [SerializeField] private GameObject player;
    Chunk[,] grid; //2D Array

    private string dataPath;
    private bool shouldSave;

    [Header("Data")]
    private World_Data worldData;

    [Header("ChunkMeshes")]
    [SerializeField] private Mesh[] chunkShapes;

    

    private void Awake()
    {
        Chunk.onUnlocked += ChunkUnlockedCallback;

        
        dataPath = Application.persistentDataPath + "/WorldData.txt";

        Chunk.onPriceChanged += ChunkPriceChangedCallback;

        dataPath = Application.dataPath + "/WorldData.txt";

    }


    private void OnDestroy()
    {
        Chunk.onUnlocked -= ChunkUnlockedCallback;
        Chunk.onPriceChanged -= ChunkPriceChangedCallback;
    }

    private void Start()
    {
       
        LoadWorld();
        Initialize();
        InvokeRepeating("TrySaveGame", 5, 1);
        


    }
    private void Update()
    {
        //fix PLayer fly
        if(player.transform.position.y > 3)
        {
            Vector3 playerFlyingPosition = player.transform.position;
            playerFlyingPosition.y = 0.23f;
            player.transform.position = playerFlyingPosition;
        }
    }

    #region Chunk Method
    private void ChunkUnlockedCallback()
    {
        UpdateChunkWalls();
        UpdateGridRenderer();
        SaveWorld();

    }
    private void ChunkPriceChangedCallback()
    {
        shouldSave = true;


    }
    private void Initialize()
    {
        InitializePriceOfChunk();
        InitializeGrid();
        UpdateChunkWalls();
        UpdateGridRenderer();




    }
    private void UpdateChunkWalls() //‡™Á§«Ë“°”·æß¢ÕßChunk‰Àπª≈¥≈ÁÕ°∫È“ß
    {
        //Loop X axis
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            //Loop Y axis
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Chunk chunk = grid[x, y];
                if (chunk == null)
                    continue;

                Chunk frontChunk = null;
                if (IsValidGridPosition(x, y + 1))
                    frontChunk = grid[x, y + 1];
                Chunk rightChunk = null;
                if (IsValidGridPosition(x + 1, y))
                    rightChunk = grid[x + 1, y];
                Chunk backChunk = null;
                if (IsValidGridPosition(x, y - 1))
                    backChunk = grid[x, y - 1];
                Chunk leftChunk = null;
                if (IsValidGridPosition(x - 1, y))
                    leftChunk = grid[x - 1, y];

                int configuration = 0;

                if (frontChunk != null && frontChunk.IsUnlocked())
                    configuration = configuration + 1; //moveTOleft
                if (rightChunk != null && rightChunk.IsUnlocked())
                    configuration = configuration + 2;
                if (backChunk != null && backChunk.IsUnlocked())
                    configuration = configuration + 4;
                if (leftChunk != null && leftChunk.IsUnlocked())
                    configuration = configuration + 8;
                //‡√“°Á√ŸÈconfiguration¢ÕßChunk·≈È«µËÕ‰ªµÈÕßÕ—ª‡¥µWalls

                chunk.UpdateWalls(configuration);

                SetChunkRenderer(chunk, configuration);

            }

        }
    }

    private void SetChunkRenderer(Chunk chunk, int configuration)
    {

        switch (configuration)
        {
            case 0:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.Four]);
                break;
            case 1:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.Two]);
                break;
            case 2:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.Two], 90);
                break;
            case 3:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.One], 90);
                break;
            case 4:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.Two], 180);
                break;
            case 5:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;
            case 6:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.One], 180);
                break;
            case 7:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;
            case 8:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.Two], 270);
                break;
            case 9:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.One]);
                break;
            case 10:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;
            case 11:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;
            case 12:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.One], 270);
                break;
            case 13:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;
            case 14:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;
            case 15:
                chunk.SetRenderer(chunkShapes[(int)ChunkShpaes.None]);
                break;





        }



    }

    private bool IsValidGridPosition(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
        {
            return false;
        }
        return true;
    }


    private void InitializePriceOfChunk()
    {
        for (int i = 0; i < world.childCount; i++)
        {
            world.GetChild(i).GetComponent<Chunk>().Initialize(worldData.chunckPrices[i]);

        }
    }

    private void InitializeGrid()
    {
        grid = new Chunk[gridSize, gridSize];
        for (int i = 0; i < world.childCount; i++)
        {
            Chunk chunk = world.GetChild(i).GetComponent<Chunk>();

            Vector2Int chunkGridPosition = new Vector2Int((int)chunk.transform.position.x / gridScale,
                (int)chunk.transform.position.z / gridScale);

            chunkGridPosition += new Vector2Int(gridSize / 2, gridSize / 2);

            grid[chunkGridPosition.x, chunkGridPosition.y] = chunk;



        }



    }

    private void UpdateGridRenderer()
    {
        //Loop X axis
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            //Loop Y axis
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Chunk chunk = grid[x, y];
                if (chunk == null)
                    continue;
                if (chunk.IsUnlocked())
                    continue;

                Chunk frontChunk = IsValidGridPosition(x, y + 1) ? grid[x, y + 1] : null;
                Chunk rightChunk = IsValidGridPosition(x + 1, y) ? grid[x + 1, y] : null;
                Chunk backChunk = IsValidGridPosition(x, y - 1) ? grid[x, y - 1] : null;
                Chunk leftChunk = IsValidGridPosition(x - 1, y) ? grid[x - 1, y] : null;

                if (frontChunk != null && frontChunk.IsUnlocked())
                    chunk.DisplayLockedElement();
                else if (rightChunk != null && rightChunk.IsUnlocked())
                    chunk.DisplayLockedElement();
                else if (backChunk != null && backChunk.IsUnlocked())
                    chunk.DisplayLockedElement();
                else if (leftChunk != null && leftChunk.IsUnlocked())
                    chunk.DisplayLockedElement();




            }
        }
    }


   
    private void TrySaveGame()
    {
        if (shouldSave)
        {
            SaveWorld();
            
            shouldSave = false;
        }

    }
    #endregion



    #region LoadAndSaveMethod
    private void LoadWorld()
    {

        string data = "";
        if (!File.Exists(dataPath))
        {
            FileStream fs = new FileStream(dataPath, FileMode.Create);

            worldData = new World_Data();

            for (int i = 0; i < world.childCount; i++)
            {
                int chunkInitialPrice = world.GetChild(i).GetComponent<Chunk>().GetInitialPrice();
                worldData.chunckPrices.Add(chunkInitialPrice);
            }

            string worldDataString = JsonUtility.ToJson(worldData, true);

            //convert byte ‡æ√“– FileStream µÕπWriteµÕπ„™È¢ÈÕ¡Ÿ≈‡ªÁπbyte
            byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);


            fs.Write(worldDataBytes);

            fs.Close(); // FileStreams‡ª‘¥‰ø≈Ï∑‘Èß‰«ÈµÈÕßª‘¥‰ø≈Ï¥È«¬
        }
        else
        {
            data = File.ReadAllText(dataPath);
            worldData = JsonUtility.FromJson<World_Data>(data); // fromJson§◊Õ·ª≈ß‰ø≈Ïjson‡ªÁπGameObj
            if (worldData.chunckPrices.Count < world.childCount)
                UpdateData();

        }


    }

    private void SaveWorld()
    {

        //∂È“‡√“‡æ‘Ë¡Chunk¡“„ÀÈ √È“ßworldData„À¡Ëµ“¡®”π«πworld.childCount
        if (worldData.chunckPrices.Count != world.childCount)
            worldData = new World_Data();

        //‡°Á∫√“§“ª≈¥≈ÁÕ°Chunkª—®®ÿ∫—π
        for (int i = 0; i < world.childCount; i++)
        {
            int chunkInitialPrice = world.GetChild(i).GetComponent<Chunk>().GetCurrentPrice();


            if (worldData.chunckPrices.Count > i)
                worldData.chunckPrices[i] = chunkInitialPrice;
            else
                worldData.chunckPrices.Add(chunkInitialPrice); //À¡“¬§«“¡«Ë“‡ªÁπChunk„À¡Ë∑’Ëª≈¥≈ÁÕ°·≈È«¬—ß‰¡Ë‰¥È‡´ø

        }

        string data = JsonUtility.ToJson(worldData, true);//ToJson§◊Õ‡‡ª≈ß®“°Object‡ªÁπJson

        //WriteAllText §◊Õ‡ª‘¥‰ø≈Ï Write ·≈– ª‘¥‰ø≈Ï
        File.WriteAllText(dataPath, data);
    }
    private void UpdateData()
    {
        // How many chunk missing in data
        int missionData = world.childCount - worldData.chunckPrices.Count;
        for (int i = 0; i < missionData; i++)
        {
            int chunkIndex = world.childCount - missionData; // index∑’Ë‰¡Ë¡’data °Á§◊Õindex≈Ë“ ÿ¥∑’Ë‡√“‡æ‘Ë¡¡“„À¡Ë
            int chunkPrice = world.GetChild(chunkIndex).GetComponent<Chunk>().GetInitialPrice();
            worldData.chunckPrices.Add(chunkPrice);


        }


    }
    #endregion



}
