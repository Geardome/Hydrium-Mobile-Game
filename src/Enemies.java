/*
 * Enemies.java
 *
 * Creado el 8 de febrero de 2006, 11:03
 *
 * Autor: Nacho S�nchez
 *
 */

public class Enemies
{
    public static DynamicObject[] EnemyArray;
    public static int EnemyCount;
    
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    
    public static void loadAll()
    {
	int count=0;
	for (int i = 0; i<Map.DynamicObjectCount; i++)
	{
	    if (Map.DynamicObjectsArray[i].type == 2)
		count++;
	}
	
	EnemyCount = count;
	
	EnemyArray = new DynamicObject[EnemyCount];
	
	count=0;
	
	for (int i = 0; i<Map.DynamicObjectCount; i++)
	{
	    if (Map.DynamicObjectsArray[i].type == 2)
	    {
		EnemyArray[count] = Map.DynamicObjectsArray[i];
		count++;
	    }
	}
    }
    
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    
    public static void updateAll()
    {
	for (int i = 0; i<EnemyCount; i++)
	{
	    // TODO: Hacer que s�lo se actualizen los enemigos si est�n cerca de la c�mara
	    //	     que no se muevan pero si se incluyan en los sectores???
	    if (!EnemyArray[i].disable)
		EnemyArray[i].update();
	}	
    }    
}
