namespace Uncharterd_Waters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CHANGES BACKGROUND COLOR OF CONSOLES
            Console.BackgroundColor=ConsoleColor.DarkBlue;
            //NEED THE CLEAR SO THAT THE ENTIRE CONSOLE CHANGES COLOR- NOT JUST THE SPACE BEHIND THE TEXT
            Console.Clear();

            //BANNER FOR PROGRAM
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("---------------------------UNCHARTED WATERS---------------------------");
            Console.WriteLine("----------------------------------------------------------------------");
           
            
           //DECLARE ARRAYS AND VARIABLES
            //CREATES TABLE OF DATA FOR SUBS POPULATING OCEAN
            int[,,] arrSubmarines = new int[10, 10, 3];
            //HOLDS DATA TO STORE 3 DIFFERENT VALUES FOR PERCENTS FOR EACH DEPTH LEVEL
            double [] arrPercent = new double[3];
            //HOLDS TEXT DATA FOR DISPLAY PURPOSES OF DIFFERENT OCEAN DEPTHS/LEVELS
            string[] arrOceanLevels={ "Surface", "Under Water", "Deep Water" };
            //HOLDS DATA TO STORE 3 DIFFERENT BOOLEANS FOR EACH DEPTH LEVEL
            bool[] arrAttackOrders= new bool[3];
            //EXITS FOR DISPLAY PURPOSES PRIMARILY 
            int uSaSubs = 0;
            int enemySubs=0;


           //CALL FUNCTION: GetSatelliteData() TO FILL arrSubmarines WITH 0,1,2 TO REPRESENT UNOCCUPIED/OCCUPIED SPACES
           arrSubmarines=GetSatelliteData();
            

           //CALL FUNCTION LevelPercent() TO GET AND RETURN % OF OCEAN POPULATED BY ALL SUBS
           //FOR LOOP CALLS FUNCTION 3 DIFFERENT TIMES TO GET THE 3 DIFFERENT LAYERS
           for (int z = 0;z< arrPercent.Length; z++) {
                arrPercent[z] = LevelPercent(arrSubmarines, z);
           }//end for loop 

           //DISPLAYS TO THE CONSOLE THE OCEAN DEPTH/LEVEL AND THE PERCENTS IN THAT LEVEL
            Console.WriteLine();
            Console.WriteLine("----- Level Density -----\n");
            //USES FOR LOOP TO ITERATE THROUGH ARRAYS TO DISPLAY INFORMATION
            for (int i = 0; i < arrPercent.Length; i++) {
                Console.WriteLine($"{arrOceanLevels[i]}:\t{arrPercent[i]}");
            }//end for loop
            Console.WriteLine();


            //CALLS FUNCTION UsSubToEnemySubRatio TO RETURN THE RATIO OF ENEMY TO US SUBS
            double ratio= UsSubToEnemySubRatio(arrSubmarines,arrOceanLevels);

            //CALLS FUNCTION AttackLevelBool() 3 TIMES TO RETURN A BOOL TO DETERMINE IF SUBS ON LEVEL SHOULD ATTACK FOR EACH LEVEL
            for (int i=0;i<arrAttackOrders.Length;i++) {
                arrAttackOrders[i] = AttackLevelBool(arrSubmarines, i);
            }//end for loop

            //DISPLAYS TO THE CONSOLE THE OCEAN DEPTH/LEVEL AND TRUE/FALSE TO ATTACK
            Console.WriteLine();
            Console.WriteLine("----- ATTACK ORDERS -----\n\n   GO FOR ATTACK:\n");
            //USES FOR LOOP TO ITERATE THROUGH ARRAYS AND DISPLAY INFORMATION
            for (int i = 0; i < arrAttackOrders.Length; i++) {
                Console.WriteLine($"{arrOceanLevels[i]}:\t{arrAttackOrders[i]}");
            }//end for loop 
            OceanDisplay(arrSubmarines,arrOceanLevels);
        }//END MAIN



         //USE Function TO DISPLAY ALL OCEAN SPACES- EACH LEVEL SHOULD BE DISPLAYED IN A DIFFERENT COLOR 
        static void OceanDisplay(int[,,] arrSubmarines,string[] arrOceanLevels) { 
            int uSaSubs = 0;
            int enemySubs = 0;
            for (int z = 0; z < arrSubmarines.GetLength(2); z++) {
                switch (z+1) {
                    case 1:  Console.ForegroundColor = ConsoleColor.White; break;
                    case 2: Console.ForegroundColor = ConsoleColor.Gray; break ;
                    case 3: Console.ForegroundColor = ConsoleColor.DarkGray; break;


                }//end switch

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"    {arrOceanLevels[z]}");

                
                for (int y = 0;y< arrSubmarines.GetLength(1); y++) {
                    Console.WriteLine();

                    for (int x = 0; x < arrSubmarines.GetLength(0); x++) {
                        Console.Write($"{arrSubmarines[x, y, z]} ");
                        if (arrSubmarines[x, y, z] == 1) {
                            uSaSubs++;
                        }//end if
                        if (arrSubmarines[x,y,z] == 2) { 
                            enemySubs++;
                        }//end if
                    }// end for x loop  
                
                }//end y for loop
                        Console.WriteLine($"\nUSA Subs: {uSaSubs}\nEnemy Subs: {enemySubs}");
                uSaSubs = 0;
                enemySubs = 0;
            }//end for z loop

        }//end function








        static bool AttackLevelBool(int[,,] arrData, int z) {

            bool ShouldAttack=false;
            int  uSaSubs = 0;
            int enemySubs = 0;
            
                for (int y = 0; y < arrData.GetLength(1); y++)
                {
                    for (int x = 0; x < arrData.GetLength(0); x++)
                    {
                        //SETS SUB RATIOS- 1==US SUBS 2==ENEMY SUBS
                        if (arrData[x, y, z] == 1)
                        {
                            uSaSubs++;

                        }//end if statement
                        if (arrData[x, y, z] == 2)
                        {

                            enemySubs++;
                        }//end if
                    }//end x for loop
                
                }//end y for loop

            if (uSaSubs > enemySubs) { 

                ShouldAttack = true;
            }//end if
            
                return ShouldAttack;          

        }//end function

        static double UsSubToEnemySubRatio(int[,,] arrData, string[]oceanLevels) {
           double uSaSubs = 0;
           double  enemySubs = 0;

            for (int z = 0; z < arrData.GetLength(2); z++) 
            {
               
               for (int y = 0; y < arrData.GetLength(1); y++)
              {
                  for (int x = 0; x < arrData.GetLength(0); x++)
                  {
                    //SETS SUB RATIOS- 1==US SUBS 2==ENEMY SUBS
                     if (arrData[x,y,z]==1) {
                            uSaSubs++;
                       
                     }//end if statement
                     if (arrData[x,y,z]==2) { 
                        
                        enemySubs++;
                     }//end if
                  }//end x for loop
               }//end y for loop
               
            }//end z for loop

            double ratio=uSaSubs/enemySubs;
            Console.WriteLine("----- US TO ENEMY RATIO -----\n");
            Console.WriteLine($"Ratio: {ratio}");

            return ratio;
        }//end function

        static double LevelPercent(int[,,] arrData, int z) {
            //DECLARE VARIABLE target TO HOLD NUMBER OF TOTAL SUBS PRESENT
            double target = 0;

            //ITERATES THROUGH Y AXIS
            for ( int y = 0; y < arrData.GetLength(1); y++) { 
                //ITERATES THROUGH X AXIS
                for (int x = 0; x < arrData.GetLength(0); x++) {
                    //SETS target TO THE NUMBER OF SUBS FOUND - REP BY 1 & 2'S
                    if (arrData[x,y,z] == 1 || arrData[x, y, z] == 2) {
                        target++ ;
                    }//end if statement
                }//end x for loop
            }//end y for loop

            double percent =(target/(arrData.GetLength(0)*arrData.GetLength(1)));

                   return percent;
        
        }//END LevelPercent FUNCTION

        

        static int[,,] GetSatelliteData() { 
         Random rand = new Random();
            int[,,] data=new int[10, 10, 3];

            for (int z=0; z<data.GetLength(2); z++) { 
                for (int y=0; y<data.GetLength(1);  y++) { 
                    for (int x=0; x<data.GetLength(0); x++) {
                        if (rand.Next(0, 101) < 25)
                        {
                            data[x, y, z] = rand.Next(1, 3);
                        
                        }//end if
                    
                    }//end x for loop
                
                }//end y for loop
            
            }//end z for loop
         return data;
        
        }//end GetSatelliteData FUNCTION

        #region PROMPT FUNCTIONS

        static string Prompt(string dataRequest)
        { //START Prompt FUNCTION
          //VARIABLES
            string userInput = "";
            //REQUEST INFORMATION FROM USER
            Console.Write(dataRequest);
            //RECEIVE RESPONSE
            userInput = Console.ReadLine();
            //OUTPUT
            return userInput;
        }//END Prompt FUNCTION 

        static int PromptInt(string dataRequest)
        { //START PromptInt FUNCTION
          //VARIABLES
            int userInput = 0;
            //REQUEST AND RECEIVE
            userInput = int.Parse(Prompt(dataRequest));
            //OUTPUT
            return userInput;
        }//END PromptInt FUNCTION

        static int PromptTryInt(string dataRequest)
        {
            //VARIABLES
            int userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
            do
            {
                Console.Write(dataRequest);
                isValid = int.TryParse(Console.ReadLine(), out userInput);
            } while (isValid == false);

            return userInput;
        }//END PromptInt FUNCTION

        static double PromptTryDouble(string dataRequest)
        {
            //VARIABLES
            double userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
            do
            {
                Console.Write(dataRequest);
                isValid = double.TryParse(Console.ReadLine(), out userInput);
            } while (isValid == false);

            return userInput;
        }//END PromptInt FUNCTION

        static double PromptDouble(string dataRequest)
        { //START PromptDouble FUNCTION
          //VARIABLES
            double userInput = 0;
            //REQUEST AND RECEIVE
            userInput = double.Parse(Prompt(dataRequest));
            //OUTPUT
            return userInput;
        }//END PromptDouble FUNCTION

        #endregion

    }//END CLASS

}//END NAMESPACE
